namespace AutoUIConsole.Components.Commands
{
    public class ShowManual : SuperCommand
    {
        public void manual() => Execute();
        public override void Execute(object parameter = null)
        {
            Helper.Log(Config.MenuTexts.Manual);
        }
    }
}