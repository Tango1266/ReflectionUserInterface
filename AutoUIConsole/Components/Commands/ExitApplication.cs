using System;

namespace AutoUIConsole.Components
{
    /// <summary>
    /// This is a command
    /// </summary>
    public partial class Commands
    {
        /// <summary>
        /// Anwendung wird verlassen
        /// </summary>
        public void ExitApplication()
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// <see cref="ExitApplication"/>
        /// </summary>
        public void q() => ExitApplication();

        public void quit() => ExitApplication();
        public void exit() => ExitApplication();
    }
}
