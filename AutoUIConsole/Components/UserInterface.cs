using AutoUIConsole.Components.Abstracts;
using AutoUIConsole.Components.DataTypes;
using System;
using System.Linq;

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

        public void ExecuteSelection(UserInput input)
        {
            if (input.IsEmpty) Helper.InvokeCommand(typeof(Commands), Config.Commands.GoToPreviousnMenu.First());

            foreach (UserInput userInput in input.Arguments)
            {
                if (userInput.IsCommand) Helper.InvokeCommand(typeof(Commands), userInput.Content);

                else if (userInput.IsNumber) HandleMenuSelection(userInput.Content);

                else HandleCustomeInput(userInput.Content);
            }
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

        public void HandleMenuSelection(string selection)
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

        public void DirectStart(UserInput input)
        {
            foreach (UserInput argument in input.Arguments)
            {

                if (argument.IsEmpty) continue;

                if (argument.IsCommand) Helper.InvokeCommand(typeof(Commands), argument.Content);

                CurrentOptions = new Options(CurrentOptions, input.Content);
                Helper.InvokeMethod(CurrentOptions);
            }
        }

        public void StepBack()
        {
            CurrentMenu = CurrentMenu?.PreviousMenu;
            CurrentOptions = CurrentOptions?.previousOptions;
        }
    }
}
