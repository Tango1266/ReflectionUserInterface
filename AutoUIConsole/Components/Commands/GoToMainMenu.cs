namespace AutoUIConsole.Components.Commands
{
    public class GoToMainMenu : SuperCommand
    {

        public void main() => Execute();

        public void home() => Execute();

        public void m() => Execute();
        public override void Execute(object parameter = null)
        {
            Session.UserInterface = new UserInterface(new Selection(null, Config.DirLevel0));

            Helper.Log("Wechsle zu Main Menu");

            Session.UserInterface.ShowConsoleMenu();
        }
    }
}
