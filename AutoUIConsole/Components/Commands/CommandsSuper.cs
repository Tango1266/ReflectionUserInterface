using System.Collections.Generic;
using System.Linq;

namespace AutoUIConsole.Components
{
    public partial class Commands
    {
        public static List<string> AvailableCommands { get; private set; }

        public Commands()
        {
            AvailableCommands = new List<string>();
            GetType().GetMethods().ToList().ForEach(x => AvailableCommands.Add(x.Name));
        }
    }
}
