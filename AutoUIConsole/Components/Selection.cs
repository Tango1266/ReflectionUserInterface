namespace AutoUIConsole.Components
{
    public class Selection
    {
        public Selection PreviousSelection { get; set; }
        public Options Options { get; set; }

        public string Content { get; set; }
        public string Query { get; set; }

        public bool HasJustOneOption => (Options.Classes.Count == 0 && Options.Methods.Count == 1);
        public bool HasOptions => (Options.Classes.Count >= 0 && Options.Methods.Count >= 1);

        public Selection(Selection selection, string input)
        {
            PreviousSelection = selection;
            Content = input;
            Query = PreviousSelection?.Query + ".*" + input;
            Options = new Options(this);
        }

        public Selection Clone()
        {
            var cloneSelection = new Selection(null, "");
            cloneSelection.PreviousSelection = PreviousSelection;
            cloneSelection.Options = Options;
            cloneSelection.Content = Content;
            cloneSelection.Query = Query;

            return cloneSelection;
        }
    }
}
