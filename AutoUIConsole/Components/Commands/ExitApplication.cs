using System;

namespace AutoUIConsole.Components
{
    public partial class Commands
    {
        public void ExitApplication()
        {
            Environment.Exit(0);
        }

        public void q() => ExitApplication();

        public void quit() => ExitApplication();
        public void exit() => ExitApplication();
    }
}
