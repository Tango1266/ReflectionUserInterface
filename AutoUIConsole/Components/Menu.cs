using AutoUIConsole.Components.DataTypes;
using System;
using System.Collections.Generic;
using System.Reflection;
using static AutoUIConsole.Helper;


namespace AutoUIConsole.Components.Abstracts
{
    public class Menu
    {

        public Menu PreviousMenu { get; set; }
        public SortedSet<string> MenuItems { get; set; }

        public bool IsMain => PathLevel.LastIsTop;

        public Menu(Menu previousMenu, Selection selection)
        {
            PreviousMenu = previousMenu;
            MenuItems = CreateMenuItems(selection.Options, selection.Content);
            PrintMenu();
        }

        public static SortedSet<string> CreateMenuItems(Options options, string selectionContent)
        {
            if (options?.Methods == null) return null;

            SortedSet<string> menuItems = new SortedSet<string>();

            foreach (MethodInfo methodInfo in options.Methods)
            {
                string fullPathMethodName = methodInfo.DeclaringType?.FullName + "." + methodInfo.Name;
                PathLevel pathLevel = new PathLevel(fullPathMethodName, selectionContent);

                if (pathLevel.IsLeafOrIncomplete) menuItems.Add(pathLevel.baseLevel);

                else if (pathLevel.IsValidOrTop) menuItems.Add(pathLevel.nextLevel);
            }

            return menuItems;
        }

        public void PrintMenu()
        {
            if (MenuItems == null) return;
            Console.Clear();

            if (MenuItems.Count == 0)
            {
                WriteLine(Environment.NewLine + $"Deine Auswahl erzielte keine Treffer. " +
                                  Environment.NewLine + $"Gebe {Config.Commands.GoToMainMenu.ToText()} ein und bestätige mit <Enter> um in das Hauptmenu zu gelangen." +
                                  Environment.NewLine + $"Oder bestätige jetzt mit <Enter> um zurück zu gelanden");
                return;
            }

            WriteLine(Environment.NewLine + $"{ "Startet alle untergeordnete Methoden mit: \t" + Config.Commands.StartAllMethods.ToText()}");

            int pos = 1;
            foreach (var item in MenuItems)
            {
                WriteLine($"  {pos++} \t : " + item);
            }

            WriteLine(Config.MenuTexts.InputNotefication);
        }

    }
}
