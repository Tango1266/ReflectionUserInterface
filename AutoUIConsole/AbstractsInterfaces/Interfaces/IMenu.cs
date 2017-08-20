using System.Collections.Generic;

namespace AutoUIConsole
{
    interface IMenu
    {
        void CreateMenu();

        SortedSet<string> GetMenuItems();

        void PrintMenu();

    }
}
