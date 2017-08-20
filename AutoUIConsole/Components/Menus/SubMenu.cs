using AutoUIConsole.Components.Abstracts;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AutoUIConsole.Components.Menus
{
    public class SubMenu : Menu
    {
        public override SortedSet<string> GetMenuItems()
        {
            var menuItems = new SortedSet<string>();

            foreach (var type in Program.UserInterface.CurrentSelection.CurrentOptions)
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

        public SubMenu(Menu previousMenu, SelectionOption selectionOption) : base(previousMenu, selectionOption)
        {
        }
    }
}