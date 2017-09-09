using AutoUIConsole.Components.DataTypes;
using System;
using System.Collections.Generic;
using System.Reflection;


namespace AutoUIConsole.Components.Abstracts
{
    public class Menu
    {

        public Menu PreviousMenu { get; set; }
        public SortedSet<string> MenuItems { get; set; }

        public bool IsMain { get; private set; }

        public Menu(Menu previousMenu, Selection selection)
        {
            PreviousMenu = previousMenu;
            MenuItems = CreateMenuItems(selection.Options, selection.Content);
            PrintMenu();
        }

        public SortedSet<string> CreateMenuItems(Options options, string selectionContent)
        {
            if (options?.Methods == null) return null;

            SortedSet<string> menuItems = new SortedSet<string>();

            foreach (MethodInfo methodInfo in options.Methods)
            {
                string fullPathMethodName = methodInfo.DeclaringType?.FullName + "." + methodInfo.Name;
                PathLevel pathLevel = new PathLevel(fullPathMethodName, selectionContent);

                if (pathLevel.IsLeafOrIncomplete) menuItems.Add(pathLevel.baseLevel);

                else if (pathLevel.IsValidOrTop) menuItems.Add(pathLevel.nextLevel);

                IsMain = pathLevel.IsTop;
            }

            return menuItems;
        }

        public void PrintMenu()
        {
            if (MenuItems == null) return;
            Console.Clear();

            if (MenuItems.Count == 0)
            {
                Console.WriteLine($"\n Deine Auswahl erzielte keine Treffer. " +
                                  $"\n Gebe {Config.Commands.GoToMainMenu.ToText()} ein und bestätige mit <Enter> um in das Hauptmenu zu gelangen." +
                                  $"\n Oder bestätige jetzt mit <Enter> um zurück zu gelanden");
                return;
            }

            Console.WriteLine($"\n { "Startet alle untergeordnete Methoden mit: \t" + Config.Commands.StartAllDisplayedTests.ToText()}");

            int pos = 1;
            foreach (var item in MenuItems)
            {
                Console.WriteLine($"  {pos++} \t : " + item);
            }

            Console.WriteLine(Config.MenuTexts.InputNotefication);
        }

    }
}
