using AutoUIConsole.Components.Abstracts;
using System.Collections.Generic;

namespace AutoUIConsole.Components.Menus
{
    public class LeafMenu : Menu
    {
        public override SortedSet<string> GetMenuItems()
        {
            var menuItems = new SortedSet<string>();

            foreach (var method in Program.UserInterface.CurrentMethodSelection.CurrentMethodOptions)
            {
                menuItems.Add(method.Name);
            }

            return menuItems;
        }

        public LeafMenu(Menu previousMenu, SelectionOption selectionOption) : base(previousMenu, selectionOption)
        {
        }
    }
}