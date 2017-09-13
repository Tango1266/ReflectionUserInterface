namespace AutoUIConsole.Components.Commands
{
    public class GoBack : SuperCommand
    {
        public void b() => Execute();

        public void back() => Execute();

        public override void Execute(object parameter = null)
        {
            var showMenu = parameter is null ? true : (bool)parameter;

            var userInterface = Session.UserInterface;

            if (userInterface.CurrentMenu?.IsMain ?? false)
            {
                userInterface.CurrentMenu.PrintMenu();
                return;
            }

            userInterface.StepBack();

            if (showMenu) userInterface.ShowConsoleMenu();
        }
    }
}
