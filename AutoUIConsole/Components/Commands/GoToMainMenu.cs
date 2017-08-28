using System;

namespace AutoUIConsole.Components
{
    public partial class Commands
    {
        public void GoToMainMenu()
        {
            //UserInterface.CurrentSelection = new SelectionOption(null, Menu.MainMenu.SelectionOptions.Selection);
            //UserInterface.CurrentMenu = new MainMenu(UserInterface.CurrentSelection);

            Program.UserInterface = new UserInterface(new SelectionOption(null, Config.DirLevel0));

            Console.WriteLine("Wechsle zu Main Menu");

            Program.UserInterface.ShowMenu();
        }

        public void main() => GoToMainMenu();

        public void home() => GoToMainMenu();

        public void m() => GoToMainMenu();
    }
}
