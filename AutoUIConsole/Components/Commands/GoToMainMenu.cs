﻿namespace AutoUIConsole.Components
{
    public partial class Commands
    {
        public void GoToMainMenu()
        {
            Session.UserInterface = new UserInterface(new Selection(null, Config.DirLevel0));

            Helper.WriteLine("Wechsle zu Main Menu");

            Session.UserInterface.ShowConsoleMenu();
        }

        public void main() => GoToMainMenu();

        public void home() => GoToMainMenu();

        public void m() => GoToMainMenu();
    }
}
