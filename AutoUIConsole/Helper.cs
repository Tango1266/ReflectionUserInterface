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
        public static string MakeRegex(string PREFIX) => AnyChars + PREFIX + AnyChars;

        public static List<Type> GetTypesFromNamespace(string seekedLevel)
        {
            var types = Config.AssemblyWhereToLookUp.GetTypes();
            return types.Where(x => x.Namespace != null && Regex.IsMatch(x.Namespace, MakeRegex(seekedLevel))).ToList();
        }


        internal static List<Type> GetTypesFromFullName(SelectionOption options)
        {

            var typeList = options?.previousOptions?.Classes ?? Config.AssemblyWhereToLookUp.GetTypes().Where(x => Regex.IsMatch(x.FullName, options?.Selection + ".*")).ToList();

            return typeList
                .Where(x => x.FullName != null && Regex.IsMatch(x.FullName, MakeRegex(options?.Selection))).ToList();
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


        public static SortedSet<string> CreateMenuItems(SelectionOption selectionOptions)
        {
            if (selectionOptions?.Methods == null) return null;
            var menuItems = new SortedSet<string>();

            foreach (var type in selectionOptions.Methods)
            {
                string menuItem = type.DeclaringType?.FullName + "." + type.Name;

                var dirLevels = GetDirStructure(selectionOptions, menuItem);

                if (Regex.IsMatch(menuItem, dirLevels[1]))
                {
                    var nextLevel = dirLevels[1];
                    menuItems.Add(nextLevel);
                }
            }

            return menuItems;
        }

        internal static List<MethodInfo> GetMethods(string selection, params Type[] classes)
        {
            return GetMethods(classes).Where(x => Regex.IsMatch(x.DeclaringType + @"\." + x.Name, selection)).ToList();
        }

        internal static List<MethodInfo> GetMethods(params Type[] classes)
        {
            var methods = new List<MethodInfo>();
            foreach (Type option in classes)
            {
                methods.AddRange(option.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public));
            }

            return methods;
        }

        /// <param name="baseLevel">e.g. "level2"</param>
        /// <param name="path">e.g. "levl0.level1.level2.level3.level4"</param>
        public static List<string> GetDirStructure(string baseLevel, string path)
        {
            var dirLevels = new List<string>();

            // e.g. DirLevels= {level2}

            /*e.g. levels=
             * 0    : level0
             *     ...
             * 4    : level4
            */
            var levels = path.Split('.');

            if (baseLevel.Length > 0)
            {
                string pathWithoutBaseLevel = Regex.Replace(path, AnyChars + @"(?=" + baseLevel + @").*(\.|$)", "");
                /*e.g. levels=
                 * 0    : level3
                 * 1    : level4
                */
                levels = pathWithoutBaseLevel.Split('.');
            }


            /* e.g. DirLevels= {level2}
             * 0    : level2
             * 1    : level3
             * 2    : level4
            */
            dirLevels.Add(baseLevel);

            foreach (string level in levels)
            {
                dirLevels.Add(level);
            }

            return dirLevels;
        }

        /// <param name="baseLevel">e.g. "level2"</param>
        /// <param name="path">e.g. "levl0.level1.level2.level3.level4"</param>
        public static List<string> GetDirStructure(SelectionOption options, string path)
        {
            var dirLevels = new List<string>();
            var targetLevel = options.Selection.Split('*').Last();

            var levels = path.Split('.');


            if (levels.Length > 0)
            {
                string pathWithoutBaseLevel = Regex.Replace(path, AnyChars + options.Selection + @"(\.|$)", "");


                if (path.Equals(pathWithoutBaseLevel))
                {

                    //ueberarbeiten
                    pathWithoutBaseLevel = Regex.Replace(path, @".*?(?=\." + targetLevel + @")", "");
                }

                levels = pathWithoutBaseLevel.Split('.').Where(x => x.Length > 1).ToArray();
            }

            dirLevels.Add(targetLevel);
            for (var i = 0; i < levels.Length; i++)
            {
                dirLevels.Add(levels[i]);
            }

            return dirLevels;
        }

        public static void InvokeMethod(SelectionOption options)
        {
            var methods = options.previousOptions.Methods.Where(x => Regex.IsMatch(x.DeclaringType?.FullName + "." + x.Name, options.Selection)).ToList();
            foreach (var method in methods)
            {
                var classType = method.DeclaringType;

                if (classType != null)
                {
                    var classInstance = Activator.CreateInstance(classType);

                    method.Invoke(Convert.ChangeType(classInstance, classInstance.GetType()), new object[] { });
                }
            }
        }
    }
}