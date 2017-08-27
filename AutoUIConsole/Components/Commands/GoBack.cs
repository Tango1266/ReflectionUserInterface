using AutoUIConsole.Components.Abstracts;

namespace AutoUIConsole.Components
{
    public partial class Commands
    {
        public void GoBack()
        {
            var menu = Program.UserInterface.CurrentMenu;
            Program.UserInterface.CurrentMenu = new Menu(menu.PreviousMenu, Program.UserInterface.CurrentSelection.Undo());
        }

        public void b() => GoBack();

        public void back() => GoBack();
    }
}
