using System.Collections.Generic;
using System.Linq;

namespace AutoUIConsole.Components
{
    public partial class Commands
    {
        public static List<string> AvailableCommands { get; } = GenerateAvailableCommands();

        private static List<string> GenerateAvailableCommands()
        {
            var availableCommands = new List<string>();
            typeof(Commands).GetMethods().ToList().ForEach(x => availableCommands.Add(x.Name));

            return availableCommands;
        }
    }
}
