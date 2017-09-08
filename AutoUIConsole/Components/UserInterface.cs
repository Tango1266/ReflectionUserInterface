using AutoUIConsole.Components.Abstracts;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutoUIConsole.Components
{
    public class UserInterface
    {
        public Commands Commands { get; set; }
        public Menu CurrentMenu { get; set; }
        public Options CurrentOptions { get; set; }


        public UserInterface(Options options)
        {
            Commands = new Commands();
            CurrentOptions = options;
        }

        public void ShowConsoleMenu()
        {
            CurrentMenu = new Menu(CurrentMenu, CurrentOptions);
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
            CurrentOptions = new Options(CurrentOptions, selection[0]);

            if (CurrentOptions.Classes.Count == 0 && CurrentOptions.Methods.Count == 1)
            {
                Helper.InvokeMethod(CurrentOptions);
            }
            else if (CurrentOptions.Methods.Count > 1)
            {
                ShowConsoleMenu();

                CurrentOptions.Selection = "";
            }
            else
            {
                ShowConsoleMenu();
            }
        }

        public void HandleDigitSelection(string selection)
        {
            if (int.TryParse(selection, out int key))
            {
                key = key - 1;
                if (key >= CurrentMenu.MenuItems.Count)
                {
                    ShowConsoleMenu();
                    Console.WriteLine(($"\n Der Wert \"{key + 1}\" stellt keine Option dar."));
                    return;
                }

                var currentItem = CurrentMenu.MenuItems.ElementAt(key);
                CurrentOptions = new Options(CurrentOptions, currentItem);

                if (CurrentOptions.Classes.Count == 0)
                {
                    Helper.InvokeMethod(CurrentOptions);
                }
                else
                {
                    ShowConsoleMenu();
                }
            }
        }

        public void DirectStart(string[] args)
        {
            CurrentOptions = new Options(CurrentOptions, args[0]);
            Helper.InvokeMethod(CurrentOptions);
        }

        public void StepBack()
        {
            CurrentMenu = CurrentMenu?.PreviousMenu;
            CurrentOptions = CurrentOptions?.previousOptions;
        }
    }
}
