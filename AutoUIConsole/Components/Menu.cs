using System;
using System.Collections.Generic;


namespace AutoUIConsole.Components.Abstracts
{
    public class Menu
    {

        public Menu PreviousMenu { get; set; }
        public SortedSet<string> MenuItems { get; set; }
        public Options optionses { get; set; }

        public Menu(Menu previousMenu, Options options)
        {
            PreviousMenu = previousMenu;
            optionses = options;
            CreateMenu();
        }

        public void CreateMenu()
        {
            MenuItems = GetMenuItems();
            PrintMenu();
        }

        public SortedSet<string> GetMenuItems()
        {
            return Helper.CreateMenuItems(optionses);
        }

        public void PrintMenu()
        {
            if (MenuItems == null) return;
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
