using System;

namespace AutoUIConsole.Components.Commands
{
    /// <summary>
    /// This is a command
    /// </summary>
    public class ExitApplication : Command
    {
        /// <summary>
        /// Anwendung wird verlassen
        /// </summary>
        public override void Execute(object parameter = null)
        {
            Environment.Exit(0);
        }

        public void q() => Execute();
        public void quit() => Execute();
        public void exit() => Execute();
    }
}
