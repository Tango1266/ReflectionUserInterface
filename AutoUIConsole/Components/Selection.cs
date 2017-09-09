using AutoUIConsole.Components.DataTypes;

namespace AutoUIConsole.Components
{
    public class Selection
    {
        public Selection previousSelection { get; set; }
        public Options Options { get; set; }

        public string Content { get; set; }
        public string Query { get; set; }

        public Selection(Selection selection, string input)
        {
            previousSelection = selection;

            Content = input;

            Query = previousSelection?.Query + ".*" + input;

            Options = new Options(this);
        }
    }
}
