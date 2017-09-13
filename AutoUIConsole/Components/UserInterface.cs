using AutoUIConsole.Components.Abstracts;
using AutoUIConsole.Components.Commands;
using AutoUIConsole.Components.DataTypes;
using System;
using System.Linq;

namespace AutoUIConsole.Components
{
    public class UserInterface
    {
        public Menu CurrentMenu { get; set; }
        public Selection currentSelection { get; set; }


        public UserInterface(Selection selection)
        {
            currentSelection = selection;
        }

        public void ShowConsoleMenu()
        {
            CurrentMenu = new Menu(CurrentMenu, currentSelection);
        }

        public void ExecuteSelection(UserInput input)
        {
            if (input.IsEmpty) Helper.InvokeCommand("GoBack");

            foreach (UserInput userInput in input.Arguments)
            {
                if (userInput.IsCommand) Helper.InvokeCommand(userInput.Content);

                else if (userInput.IsNumber) HandleMenuSelection(userInput.Content);

                else HandleCustomeInput(userInput.Content);
            }
        }


        public void HandleCustomeInput(params string[] selection)
        {
            currentSelection = new Selection(currentSelection, selection[0]);

            if (currentSelection.Options.Classes.Count == 0 && currentSelection.Options.Methods.Count == 1)
            {
                Helper.InvokeMethod(currentSelection);
            }
            else if (currentSelection.Options.Methods.Count >= 1)
            {
                ShowConsoleMenu();

                currentSelection.Query = "";
            }
            else
            {
                ShowConsoleMenu();
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
                    Helper.WriteLine(Environment.NewLine + $"Der Wert \"{key + 1}\" stellt keine Option dar.");
                    return;
                }

                var currentItem = CurrentMenu.MenuItems.ElementAt(key);
                currentSelection = new Selection(currentSelection, currentItem);

                if (currentSelection.Options.Classes.Count == 0)
                {
                    Helper.InvokeMethod(currentSelection);
                }
                else
                {
                    ShowConsoleMenu();
                }
            }
        }

        public void DirectStart(UserInput input)
        {
            foreach (UserInput argument in input.Arguments)
            {
                if (argument.IsEmpty) continue;

                currentSelection = new Selection(currentSelection, argument.Content);

                //TODO: Evaluieren ob notwendig
                if (argument.IsCommand) Helper.InvokeCommand(argument.Content);

                Helper.InvokeMethod(currentSelection);

                new GoBack().Execute(false);
            }
        }

        public void StepBack()
        {
            CurrentMenu = CurrentMenu?.PreviousMenu;
            currentSelection = currentSelection?.previousSelection;
        }
    }
}
