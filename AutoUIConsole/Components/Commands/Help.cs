using System;

namespace AutoUIConsole.Components
{
    public partial class Commands
    {
        public void help()
        {
            Console.Clear();
            Helper.WriteLine(Config.MenuTexts.HelpText);
        }

        public void h() => help();
    }
}