using AutoUIConsole.Components.Abstracts;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutoUIConsole.Components
{
    public class UserInterface
    {
        public Commands Commands;
        public Menu CurrentMenu;
        public SelectionOption CurrentSelection;

        public UserInterface(SelectionOption selectionOption)
        {
            Commands = new Commands();
            CurrentSelection = selectionOption;
        }

        public void ShowMenu()
        {
            CurrentMenu = new Menu(CurrentMenu, CurrentSelection);
        }

        public void ExecuteSelection(string selection)
        {
            if (Commands.AvailableCommands.Contains(selection))
            {
                Helper.InvokeCommand(typeof(Commands), selection);
            }
            else if (selection.Equals(string.Empty))
            {
                Helper.InvokeCommand(typeof(Commands), Config.Commands.GoToPreviousnMenu.First());
            }

            else if (SelectionIsNumber(selection))
            {
                HandleDigitSelection(selection);
            }
            else
            {
                HandleCustomeInput(selection);
            }
        }

        private static bool SelectionIsNumber(string selection)
        {
            return Regex.IsMatch(selection, Config.RegexPattern.ConsistOnlyOfDigits);
        }

        public void HandleCustomeInput(params string[] selection)
        {
            CurrentSelection = new SelectionOption(CurrentSelection, selection[0]);

            if (CurrentSelection.Classes.Count == 0 && CurrentSelection.Methods.Count == 1)
            {
                Helper.InvokeMethod(CurrentSelection);
            }
            else if (CurrentSelection.Methods.Count > 1)
            {
                ShowMenu();

                CurrentSelection.Selection = "";
            }
            else
            {
                ShowMenu();
            }
        }

        public void HandleDigitSelection(string selection)
        {
            if (int.TryParse(selection, out int key))
            {
                key = key - 1;
                if (key >= CurrentMenu.MenuItems.Count)
                {
                    ShowMenu();
                    Console.WriteLine(($"\n Der Wert \"{key + 1}\" stellt keine Option dar."));
                    return;
                }

                var currentItem = CurrentMenu.MenuItems.ElementAt(key);
                CurrentSelection = new SelectionOption(CurrentSelection, currentItem);

                if (CurrentSelection.Classes.Count == 0)
                {
                    Helper.InvokeMethod(CurrentSelection);
                }
                else
                {
                    ShowMenu();
                }
            }
        }

        public void DirectStart(string[] args)
        {
            CurrentSelection = new SelectionOption(CurrentSelection, args[0]);
            Helper.InvokeMethod(CurrentSelection);
        }
    }
}
