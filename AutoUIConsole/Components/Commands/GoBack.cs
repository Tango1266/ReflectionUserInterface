using AutoUIConsole.Components.Abstracts;

namespace AutoUIConsole.Components
{
    public partial class Commands
    {
        public void GoBack()
        {
            var menu = InterfaceControl.UserInterface.CurrentMenu;
            InterfaceControl.UserInterface.CurrentMenu = new Menu(menu.PreviousMenu, InterfaceControl.UserInterface.CurrentSelection.Undo());
        }

        public void b() => GoBack();

        public void back() => GoBack();
    }
}
