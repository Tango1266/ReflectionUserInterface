namespace AutoUIConsole.Components
{
    public partial class Commands
    {
        public void GoBack()
        {
            var userInterface = Session.UserInterface;
            userInterface.StepBack();
            userInterface.ShowConsoleMenu();
        }

        public void b() => GoBack();

        public void back() => GoBack();
    }
}
