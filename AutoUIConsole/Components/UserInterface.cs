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
            CurrentMenu = new Menu(null, CurrentSelection);
        }

        public void ExecuteSelection(string selection)
        {
            if (Commands.AvailableCommands.Contains(selection))
            {
                Helper.InvokeCommand(typeof(Commands), selection);
            }

            else if (SelectionIsNumber(selection))
            {
                HandleDigitSelection(selection);
            }
            else
            {
                HandleSelection(selection);
            }
        }

        private static bool SelectionIsNumber(string selection)
        {
            return Regex.IsMatch(selection, Config.RegexPattern.ConsistOnlyOfDigits);
        }

        public void HandleSelection(params string[] selection)
        {
            CurrentSelection = new SelectionOption(CurrentSelection, selection[0]);

            if (CurrentSelection.Classes.Count == 1)
            {
                //TODO: Methoden sollen aufgelistet werden bzw ausgefuehrt, wenn eine Methode eingegeben wird
                //hier ist es wohl angebracht, dass der Klassenname vorher angegeben wird
            }

            CurrentMenu = new Menu(CurrentMenu, CurrentSelection);
        }

        public void HandleDigitSelection(string selection)
        {
            if (int.TryParse(selection, out int key))
            {
                key = key - 1;
                if (key >= CurrentMenu.MenuItems.Count)
                    throw new ArgumentException("Der Wert stellt keine Option dar.");

                var currentItem = CurrentMenu.MenuItems.ElementAt(key);
                CurrentSelection = new SelectionOption(CurrentSelection, currentItem);

                if (CurrentSelection.Classes.Count == 0)
                {
                    Helper.InvokeMethod(CurrentSelection);
                }
                else
                {
                    CurrentMenu = new Menu(CurrentMenu, CurrentSelection);
                }
            }
        }
    }
}
