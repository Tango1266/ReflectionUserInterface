using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Input;
using AutoUIConsole.Components;
using AutoUIConsole.Components.Commands;
using AutoUIConsole.Components.DataTypes;
using AutoUIConsole.Utils;

namespace AutoUIConsole
{
    public static class Helper
    {
        internal static List<Type> GetTypesFromFullName(Selection selection)
        {
            List<Type> typeList = selection?.PreviousSelection?.Options?.Classes ?? GetTypeFromAssembly(selection);

            return typeList.Where(x 
                => x.FullName != null 
                   && Regex.IsMatch(x.FullName, ".*"
                                                + 
                                                selection?.Query + ".*"))
                .ToList();
        }

        internal static List<MethodInfo> GetMethods(params Type[] classes)
        {
            List<MethodInfo> methods = new List<MethodInfo>();
            foreach (Type option in classes)
                methods.AddRange(option.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public));

            return methods;
        }

        public static List<MethodInfo> GetMethodsFiltered(string querry)
        {
            var classes = GetTypeFromAssembly(new Selection(null, querry));
            return GetMethods(classes.ToArray());
        }

        internal static List<MethodInfo> GetMethodsFiltered(string selection, params Type[] classes)
        {
            return GetMethods(classes).Where(
                x => 
                    Regex.IsMatch(
                        x.DeclaringType + "." + x.Name,
                        selection
                        )).ToList();
        }

        public static void InvokeMethod(Selection selection)
        {
            //TODO:Evaluieren ob benoetig, da bereits schon in GetMethodsFiltered
            List<MethodInfo> methods = selection.PreviousSelection.Options.Methods.Where(x => Regex.IsMatch(x.DeclaringType?.FullName + "." + x.Name, selection.Query)).ToList();
            foreach (MethodInfo method in methods)
            {
                Type classType = method.DeclaringType;
                if (classType is null) return;

                object classInstance = Activator.CreateInstance(classType);
                method.Invoke(Convert.ChangeType(classInstance, classInstance.GetType()), new object[] { });
            }
        }
        public static void InvokeCommand(string requestedCommand, UserInput[] parameter = null)
        {
            var filteredCommandTypes = Command.CommandTypeMethods.Keys.Where(x => x.GetType().Name.Equals(requestedCommand)).ToList();
            if (filteredCommandTypes.Count < 1)
            {
                foreach (ICommand command in Command.CommandTypeMethods.Keys)
                {
                    var isMatch = Command.CommandTypeMethods[command].Any(x => x.Name.Equals(requestedCommand));

                    if (isMatch)
                    {
                        ICommand com = (ICommand)Activator.CreateInstance(command.GetType(), new object[] { });
                        com.Execute(parameter);
                    }
                }
            }
            else filteredCommandTypes.First().Execute(parameter);
        }

        public static void InvokeCommand(UserInput userInput)
        {
            InvokeCommand(userInput.Content, userInput.ToArray());
        }

        private static List<Type> GetTypeFromAssembly(Selection selection)
        {
            return AppConfig.AssemblyWhereToLookUp.GetTypes()
                .Where(x => Regex.IsMatch(x.FullName, selection?.Query + ".*")).ToList();
        }

        public static string ToText(this IEnumerable<object> array, string[] seperator=null)
        {
            string res = "";
            var sep = seperator ?? new[] {" "};
            foreach (object userInput in array)
            {
                res = res + userInput + sep[0];
            }

            return res.TrimEnd(char.Parse(sep[0]));
        }

        public static string ToText(this List<string> list)
        {
            string res = "";
            list.ForEach(x => res += x + ", ");
            return res.Substring(0, res.Length - 2) + "";
        }

        public static void Log(string message, Exception ex = null)
        {
            LogInLine(message, true, ex);
        }

        public static void LogInLine(string message, bool newLine = false, Exception ex = null)
        {

            if (newLine)
            {
                Console.WriteLine(message);
                Debug.WriteLine(message);
            }
            else
            {
                Console.Write(message);
                Debug.Write(message);

            }

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

        public static bool IsNumber(this String stringToValidate)
        {
            Assert.IsNotNull(stringToValidate,$"{stringToValidate} was null");
            return Regex.IsMatch(stringToValidate, @"\b\d+$");
        }
    }
}