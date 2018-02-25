using AutoUIConsole.Utils;

namespace AutoUIConsole.Components.Commands
{
    public class ShowManual : Command
    {
        public void manual() => Execute();
        public override void Execute(object parameter = null)
        {
            Helper.Log(AutoUIConsole.AppConfig.MenuTexts.Manual);
        }
    }
}