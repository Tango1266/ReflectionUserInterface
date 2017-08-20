using System;
using System.Collections.Generic;


namespace AutoUIConsole.Components.Abstracts
{
    public abstract class Menu : IMenu
    {
        public Menu PreviousMenu { get; set; }
        public SortedSet<string> MenuItems { get; set; }
        public SelectionOption SelectionOptions { get; set; }
        public DirStructure Directory { get; set; }

        public Menu(Menu previousMenu, SelectionOption selectionOption)
        {
            PreviousMenu = previousMenu;
            SelectionOptions = selectionOption;
            CreateMenu();
        }

        public void CreateMenu()
        {
            MenuItems = GetMenuItems();
            PrintMenu();
        }

        public abstract SortedSet<string> GetMenuItems();

        public void PrintMenu()
        {
            Console.Clear();

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
