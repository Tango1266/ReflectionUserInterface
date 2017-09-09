using AutoUIConsole.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AutoUIConsole
{
    public static class Helper
    {
        public static Assembly GetLookUpAssembly(Type anyTypeOfTheTargetAssembly)
        {
            return Assembly.GetAssembly(anyTypeOfTheTargetAssembly);
        }
        internal static List<Type> GetTypesFromFullName(Selection optionses)
        {
            List<Type> typeList = optionses?.previousSelection?.Options?.Classes ?? GetTypeFromAssembly(optionses);

            return typeList
                .Where(x => x.FullName != null && Regex.IsMatch(x.FullName, ".*" + optionses?.Query + ".*")).ToList();
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

        public static void InvokeMethod(Selection optionses)
        {
            List<MethodInfo> methods = optionses.previousSelection.Options.Methods
                .Where(x => Regex.IsMatch(x.DeclaringType?.FullName + "." + x.Name, optionses.Query)).ToList();
            foreach (MethodInfo method in methods)
            {
                Type classType = method.DeclaringType;

                if (classType != null)
                {
                    object classInstance = Activator.CreateInstance(classType);

                    method.Invoke(Convert.ChangeType(classInstance, classInstance.GetType()), new object[] { });
                }
            }
        }
        public static void InvokeCommand(Type type, string selection)
        {
            MethodInfo method = type.GetMethod(selection,
                BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);
            method.Invoke(Session.UserInterface.Commands, new object[] { });
        }

        private static List<Type> GetTypeFromAssembly(Selection optionses)
        {
            return Config.AssemblyWhereToLookUp.GetTypes()
                .Where(x => Regex.IsMatch(x.FullName, optionses?.Query + ".*")).ToList();
        }

        public static string ToText(this object[] array)
        {
            string res = "";

            foreach (object userInput in array)
            {
                res = userInput + " ";
            }

            return res.TrimEnd();
        }

        public static string ToText(this List<string> list)
        {
            string res = "{";
            list.ForEach(x => res += x + ", ");
            return res.Substring(0, res.Length - 2) + "}";
        }
    }
}