using AutoUIConsole.Components.Abstracts;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AutoUIConsole.Components.Menus
{
    public class MainMenu : Menu
    {
        public override SortedSet<string> GetMenuItems()
        {
            //return new SortedSet<string> { "EinMenuItem" };

            var menuItems = new SortedSet<string>();

            foreach (var type in SelectionOptions.CurrentOptions)
            {
                string menuItem = type.FullName;

                Directory = new DirStructure(SelectionOptions.Selection, menuItem);


                if (Regex.IsMatch(menuItem, Directory.DirLevels[0]))
                {
                    var nextLevel = Directory.DirLevels[0];
                    menuItems.Add(nextLevel);
                }
            }

            return menuItems;
        }


        public MainMenu(SelectionOption selection) : base(null, selection)
        {

        }

    }
}