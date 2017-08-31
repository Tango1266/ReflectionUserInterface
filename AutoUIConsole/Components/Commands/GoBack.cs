namespace AutoUIConsole.Components
{
    public partial class Commands
    {
        public void GoBack()
        {
            var userInterface = InterfaceControl.UserInterface;
            userInterface.StepBack();
            userInterface.ShowMenu();
        }

        public void b() => GoBack();

        public void back() => GoBack();
    }
}
