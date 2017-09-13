namespace AutoUIConsole.Components.Commands
{
    public partial class ShowManual : SuperCommand
    {
        public void manual() => Execute();
        public override void Execute(object parameter = null)
        {
            Helper.WriteLine(Config.MenuTexts.Manual);

        }
    }
}