namespace AutoUIConsole.Components
{
    public partial class Commands
    {
        public void GoBack(bool showMenu = true)
        {
            var userInterface = Session.UserInterface;

            if (userInterface.CurrentMenu?.IsMain ?? false)
            {
                userInterface.CurrentMenu.PrintMenu();
                return;
            }

            userInterface.StepBack();

            if (showMenu) userInterface.ShowConsoleMenu();
        }

        public void b() => GoBack();

        public void back() => GoBack();
    }


}
