using System;
using System.Collections.Generic;
using System.Reflection;

namespace AutoUIConsole.Components
{
    public class SelectionOption
    {
        public SelectionOption previousSelectionOptions { get; set; }
        public List<Type> CurrentOptions { get; set; }
        public List<MethodInfo> CurrentMethodOptions { get; set; }

        public string Selection { get; set; }

        public bool NoOptionsLeft => CurrentMethodOptions != null || CurrentOptions.Count == 0;

        public SelectionOption(SelectionOption selectionOptions, string selection)
        {
            previousSelectionOptions = selectionOptions;
            Selection = selection;

            if (Program.UserInterface != null
                && Program.UserInterface.CurrentMenu.Directory != null
                && Program.UserInterface.CurrentMenu.Directory.IsLeaf)
            {
                CurrentOptions = Helper.GetTypesFromFullNameRegex("\\." + Selection + "$");
                return;
            }

            CurrentOptions = Helper.GetTypesFromFullName(Selection);
        }

        public SelectionOption Undo()
        {
            CurrentOptions = previousSelectionOptions?.CurrentOptions;
            Selection = previousSelectionOptions?.Selection;
            CurrentMethodOptions = previousSelectionOptions?.CurrentMethodOptions;
            previousSelectionOptions = previousSelectionOptions?.previousSelectionOptions;
            return this;
        }

        public SelectionOption SelectMethods()
        {
            //CurrentOptions = null;
            CurrentMethodOptions = Helper.GetMethods(Selection);
            return this;
        }

    }
}
