using AutoUIConsole.Components.DataTypes;

namespace AutoUIConsole.Components
{
    public class Selection
    {
        public Selection previousSelection { get; set; }
        public Options Options { get; set; }

        public string Content { get; set; }
        public string Query { get; set; }

        public bool HasJustOneOption => (Options.Classes.Count == 0 && Options.Methods.Count == 1);
        public bool HasOptions => (Options.Classes.Count >= 0 && Options.Methods.Count >= 1);

        public Selection(Selection selection, string input)
        {
            previousSelection = selection;
            Content = input;
            Query = previousSelection?.Query + ".*" + input;
            Options = new Options(this);
        }

        public Selection Clone()
        {
            var cloneSelection = new Selection(null, "");
            cloneSelection.previousSelection = this.previousSelection;
            cloneSelection.Options = this.Options;
            cloneSelection.Content = this.Content;
            cloneSelection.Query = this.Query;

            return cloneSelection;
        }
    }
}
