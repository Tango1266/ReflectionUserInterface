using AutoUIConsole.Components.Menus;

namespace AutoUIConsole.Components
{
    public partial class Commands
    {
        public void GoBack()
        {
            var menu = Program.UserInterface.CurrentMenu;
            Program.UserInterface.CurrentMenu = new SubMenu(menu.PreviousMenu, Program.UserInterface.CurrentSelection.Undo());
        }

        public void b() => GoBack();

        public void back() => GoBack();
    }
}
