using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using static AutoUIConsole.Config.RegexPattern;
namespace AutoUIConsole
{
    internal class Helper
    {
        public static string MakeRegex(string PREFIX) => AnyChars + PREFIX + AnyChars;

        public static List<Type> GetTypesFromNamespace(string seekedLevel)
        {
            var types = Config.AssemblyWhereToLookUp.GetTypes();
            return types.Where(x => x.Namespace != null && Regex.IsMatch(x.Namespace, MakeRegex(seekedLevel))).ToList();
        }

        public static List<Type> GetTypesFromFullName(string seekedLevel)
        {
            return Config.AssemblyWhereToLookUp.GetTypes()
                .Where(x => x.FullName != null && Regex.IsMatch(x.FullName, MakeRegex(seekedLevel))).ToList();
        }

        public static List<Type> GetTypesFromFullNameRegex(string RegexPattern)
        {
            return Config.AssemblyWhereToLookUp.GetTypes()
                .Where(x => x.FullName != null && Regex.IsMatch(x.FullName, RegexPattern)).ToList();
        }


        public static Assembly GetLookUpAssembly(Type AnyTypeOfTheTargetAssembly)
        {
            return Assembly.GetAssembly(AnyTypeOfTheTargetAssembly);
        }

        public static void InvokeCommand(Type type, string selection)
        {
            var method = type.GetMethod(selection, BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);
            method.Invoke(Program.UserInterface.Commands, new object[] { });
        }

        public static SortedSet<string> CreateMenuItems()
        {
            var menuItems = new SortedSet<string>();

            foreach (var type in Program.UserInterface.CurrentSelection.CurrentOptions)
            {
                string menuItem = type.FullName;
                string NameSpaceEndsWithDefineLevel = Program.UserInterface.CurrentSelection.Selection;


                if (Regex.IsMatch(menuItem, NameSpaceEndsWithDefineLevel))
                {
                    menuItems.Add(menuItem);
                }
            }

            return menuItems;
        }

        public static List<MethodInfo> GetMethods(string selection)
        {
            return GetTypesFromFullName(selection)[0].GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public).ToList();
        }
    }
}