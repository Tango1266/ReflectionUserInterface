using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AutoUIConsole._Archive
{
    sealed class Archive
    {
        public static List<Type> mainMenu;
        public static List<Type> currentSelection;
        public static List<Type> previousSelection;
        public static List<MethodInfo> testCaseSelection;


        public static SortedSet<string> mainMenuItems;
        public static SortedSet<string> currentMenuItems;
        public static SortedSet<string> previousMenuItems;
        private static bool IsStartUp;

        public static void createMenu(Type testClass)
        {
            testCaseSelection = testClass.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public).ToList();
            PrintMenu(GetTestCases());
        }


        public static void createMenu(string seekedLevel)
        {
            currentMenuItems = GetMenuItems(seekedLevel);
            PrintMenu(currentMenuItems);
        }


        public static SortedSet<string> GetTestCases()
        {
            var menuItems = new SortedSet<string>();
            testCaseSelection.ForEach(x => menuItems.Add(x.Name));
            return menuItems;
        }

        public static SortedSet<string> GetMenuItems(string seekedLevel)
        {
            SortedSet<string> menuItems = CreateMenuItemSet(seekedLevel);



            return menuItems;
        }



        public static SortedSet<string> CreateMenuItemSet(string seekedLevel)
        {
            var menuItems = new SortedSet<string>();

            foreach (var type in currentSelection)
            {
                string menuItem = "";
                string NameSpaceEndsWithDefineLevel = "";
                if (IsStartUp)
                {
                    menuItem = type.Namespace.Substring(type.Namespace.LastIndexOf(".", StringComparison.Ordinal) + 1);
                    NameSpaceEndsWithDefineLevel = seekedLevel + Config.RegexPattern.endsWithTwoDigitsFollowingByWordChars;
                }
                else
                {
                    menuItem = type.FullName.Substring(type.FullName.LastIndexOf(".", StringComparison.Ordinal) + 1);
                    NameSpaceEndsWithDefineLevel = ".*";
                }


                if (Regex.IsMatch(menuItem, NameSpaceEndsWithDefineLevel))
                {
                    menuItems.Add(menuItem);
                }
            }

            return menuItems;
        }


        public static void PrintMenu(SortedSet<string> menuItems)
        {

        }

        public static void TestSubstring()
        {
            string x = "TestSuiten.KF1.UC1";
            Console.WriteLine(x.Substring(0, x.LastIndexOf(".", StringComparison.Ordinal)));
        }
    }
}
