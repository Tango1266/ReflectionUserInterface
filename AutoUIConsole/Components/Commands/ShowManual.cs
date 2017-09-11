namespace AutoUIConsole.Components
{
    public partial class Commands
    {
        public void ShowManual()
        {
            Helper.WriteLine(Config.MenuTexts.Manual);
        }

        public void manual() => ShowManual();
    }
}