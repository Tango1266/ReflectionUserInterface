using AutoUIConsole.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using static AutoUIConsole.Config.RegexPattern;

namespace AutoUIConsole
{
    public class Helper
    {
        public static List<string> GetDirStructure(Options optionses, string path)
        {
            List<string> dirLevels = new List<string>();
            string targetLevel = optionses.Selection.Split('*').Last();

            string[] levels = path.Split('.');


            if (levels.Length > 0)
            {
                string pathWithoutBaseLevel = Regex.Replace(path, AnyChars + optionses.Selection + @"(\.|$)", "");


                if (path.Equals(pathWithoutBaseLevel))
                    pathWithoutBaseLevel = Regex.Replace(path, @".*?(?=\." + targetLevel + @")", "");

                levels = pathWithoutBaseLevel.Split('.').Where(x => x.Length > 1).ToArray();
            }

            dirLevels.Add(targetLevel);
            for (int i = 0; i < levels.Length; i++)
                dirLevels.Add(levels[i]);

            return dirLevels;
        }
        //TODO: Bug: Wenn per Nummer zu einer Methode navigiert und ausgefuert wird und darauffolgend eine weitere oder die gleiche ausgewaehlt wird, dann gibt es eine Exception
        public static SortedSet<string> CreateMenuItems(Options optionses)
        {
            if (optionses?.Methods == null) return null;
            SortedSet<string> menuItems = new SortedSet<string>();

            foreach (MethodInfo type in optionses.Methods)
            {
                string menuItem = type.DeclaringType?.FullName + "." + type.Name;

                if (Regex.IsMatch(menuItem, optionses.Selection))
                {
                    List<string> dirLevels = GetDirStructure(optionses, menuItem);

                    string nextLevel = "";

                    if (dirLevels.Count > 1)
                        nextLevel = dirLevels[1];
                    else if (dirLevels.Count == 1)
                        nextLevel = menuItem;

                    if (Regex.IsMatch(menuItem, nextLevel)) menuItems.Add(nextLevel);

                }
            }

            return menuItems;
        }

        public static Assembly GetLookUpAssembly(Type AnyTypeOfTheTargetAssembly)
        {
            return Assembly.GetAssembly(AnyTypeOfTheTargetAssembly);
        }
        internal static List<Type> GetTypesFromFullName(Options optionses)
        {
            List<Type> typeList = optionses?.previousOptions?.Classes ?? GetTypeFromAssembly(optionses);

            return typeList
                .Where(x => x.FullName != null && Regex.IsMatch(x.FullName, ".*" + optionses?.Selection + ".*")).ToList();
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

        public static void InvokeMethod(Options optionses)
        {
            List<MethodInfo> methods = optionses.previousOptions.Methods
                .Where(x => Regex.IsMatch(x.DeclaringType?.FullName + "." + x.Name, optionses.Selection)).ToList();
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

        private static List<Type> GetTypeFromAssembly(Options optionses)
        {
            return Config.AssemblyWhereToLookUp.GetTypes()
                .Where(x => Regex.IsMatch(x.FullName, optionses?.Selection + ".*")).ToList();
        }

        public static bool CheckIsNumber(string selection)
        {
            return Regex.IsMatch(selection, Config.RegexPattern.ConsistOnlyOfDigits);
        }
    }
}