using AutoUIConsole.Components.Commands;
using AutoUIConsole.Components.DataTypes;
using System;
using System.Linq;

namespace AutoUIConsole.Components
{
    public class UserInterface
    {
        public Menu CurrentMenu { get; set; }
        public Selection CurrentSelection { get; set; }

        public UserInterface(Selection selection)
        {
            CurrentSelection = selection;
        }

        public void ShowConsoleMenu()
        {
            CurrentMenu = new Menu(CurrentMenu, CurrentSelection);
        }

        public void HandleUserInput(UserInput input)
        {
            if (input.IsEmpty) Helper.InvokeCommand("GoBack");

            else if (input.IsCommand && input.IsMultiInput)
            {
                UserInput remainingArgs = input;
                Session.IsDirectStart = true;
                DirectStart(remainingArgs);
                Session.IsDirectStart = false;
            }

            else if (input.IsCommand) Helper.InvokeCommand(input);

            else if (input.IsNumber) HandleMenuSelection(input.Content);

            else HandleCustomeInput(input);
        }


        public void HandleCustomeInput(UserInput userInput)
        {
            foreach (UserInput userInputArgument in userInput)
            {
                if ( !(!userInput.IsMultiInput && userInput.Content.Equals(AppConfig.DirLevel0)))
                {
                    // change CurrentSelection only if it is new Selection different from Dir0
                    CurrentSelection = new Selection(CurrentSelection, userInputArgument.Content);
                }

                if (CurrentSelection.HasJustOneOption) Helper.InvokeMethod(CurrentSelection);

                else if (CurrentSelection.HasOptions)
                {
                    ShowConsoleMenu();
                    CurrentSelection.Query = "";
                }
                else
                {
                    ShowConsoleMenu();
                }
            }
        }

        public void HandleMenuSelection(string selection)
        {
            if (int.TryParse(selection, out int key))
            {
                key = key - 1;
                if (key >= CurrentMenu.MenuItems.Count)
                {
                    ShowConsoleMenu();
                    Helper.Log(Environment.NewLine + $"Der Wert \"{key + 1}\" stellt keine Option dar.");
                    return;
                }

                var currentItem = CurrentMenu.MenuItems.ElementAt(key);
                CurrentSelection = new Selection(CurrentSelection, currentItem);

                if (CurrentSelection.Options.Classes.Count == 0)
                {
                    Helper.InvokeMethod(CurrentSelection);
                }
                else
                {
                    ShowConsoleMenu();
                }
            }
        }

        public void DirectStart(UserInput input, bool showMenu=false)
        {
            foreach (UserInput argument in input)
            {
                if (argument.IsEmpty) continue;
                if (argument.IsCommand) continue;

                CurrentSelection = new Selection(CurrentSelection, argument.Content);
                Helper.InvokeMethod(CurrentSelection);

                new GoBack().Execute(showMenu);
            }
        }

        public void StepBack()
        {
            CurrentMenu = CurrentMenu?.PreviousMenu ?? CurrentMenu;
            CurrentSelection = CurrentSelection?.PreviousSelection ?? CurrentSelection;
        }
    }
}
