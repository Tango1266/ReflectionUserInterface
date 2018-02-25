namespace AutoUIConsole.Components.Commands
{
    public class GoBack : SuperCommand
    {
        public void b() => Execute();

        public void back() => Execute();

        public override void Execute(object parameter = null)
        {
            var userInterface = Session.UserInterface;

            userInterface.StepBack();

            if(Session.IsDirectStart)return;

            userInterface.ShowConsoleMenu();
        }
    }
}
