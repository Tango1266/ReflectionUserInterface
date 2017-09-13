using AutoUIConsole.Components;
using AutoUIConsole.Components.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace AutoUIConsole
{
    public static class Helper
    {
        internal static List<Type> GetTypesFromFullName(Selection selection)
        {
            List<Type> typeList = selection?.previousSelection?.Options?.Classes ?? GetTypeFromAssembly(selection);

            return typeList
                .Where(x => x.FullName != null && Regex.IsMatch(x.FullName, ".*" + selection?.Query + ".*")).ToList();
        }

        internal static List<MethodInfo> GetMethods(params Type[] classes)
        {
            List<MethodInfo> methods = new List<MethodInfo>();
            foreach (Type option in classes)
                methods.AddRange(option.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public));

            return methods;
        }

        internal static List<MethodInfo> GetMethodsFiltered(string selection, params Type[] classes)
        {
            return GetMethods(classes).Where(x => Regex.IsMatch(x.DeclaringType + "." + x.Name, selection)).ToList();
        }

        public static void InvokeMethod(Selection selection)
        {
            List<MethodInfo> methods = selection.previousSelection.Options.Methods
                .Where(x => Regex.IsMatch(x.DeclaringType?.FullName + "." + x.Name, selection.Query)).ToList();
            foreach (MethodInfo method in methods)
            {
                Type classType = method.DeclaringType;
                if (classType is null) return;

                object classInstance = Activator.CreateInstance(classType);
                method.Invoke(Convert.ChangeType(classInstance, classInstance.GetType()), new object[] { });
            }
        }

        //public static void InvokeCommand2(Type type, string selection)
        //{
        //    MethodInfo method = type.GetMethod(selection,
        //        BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);
        //    method.Invoke(Session.UserInterface.Commands, new object[] { });
        //}

        public static void InvokeCommand(string selection)
        {
            var filteredCommandTypes = SuperCommand.CommandTypeMethods.Keys.Where(x => x.GetType().Name.Equals(selection)).ToList();
            if (filteredCommandTypes.Count < 1)
            {
                foreach (ICommand command in SuperCommand.CommandTypeMethods.Keys)
                {
                    var isMatch = SuperCommand.CommandTypeMethods[command].Any(x => x.Name.Equals(selection));

                    if (isMatch)
                    {
                        ICommand com = (ICommand)Activator.CreateInstance(command.GetType(), new object[] { });
                        com.Execute(null);
                    }
                }
            }
            else filteredCommandTypes.First().Execute(null);
        }

        private static List<Type> GetTypeFromAssembly(Selection selection)
        {
            return Config.AssemblyWhereToLookUp.GetTypes()
                .Where(x => Regex.IsMatch(x.FullName, selection?.Query + ".*")).ToList();
        }

        public static string ToText(this object[] array)
        {
            string res = "";

            foreach (object userInput in array)
            {
                res = res + userInput + " ";
            }

            return res.TrimEnd();
        }

        public static string ToText(this List<string> list)
        {
            string res = "";
            list.ForEach(x => res += x + ", ");
            return res.Substring(0, res.Length - 2) + "";
        }

        public static void WriteLine(string message, Exception ex = null)
        {
            Write(message, true, ex);
        }

        public static void Write(string message, bool newLine = false, Exception ex = null)
        {

            if (newLine) Console.WriteLine(message);
            else Console.Write(message);

            if (!(ex is null))
            {
                Console.WriteLine
                    (
                    Environment.NewLine +
                    "Fehlerinhalt:" + Environment.NewLine +
                    nameof(ex.Message) + ": " + Environment.NewLine +
                    "\t" + ex.Message + Environment.NewLine +
                    nameof(ex.InnerException) + ": " + Environment.NewLine +
                    "\t" + ex.InnerException + Environment.NewLine +
                    nameof(ex.StackTrace) + ": " + Environment.NewLine +
                    "\t" + ex.StackTrace
                    + Environment.NewLine
                    );
            }
        }
    }
}