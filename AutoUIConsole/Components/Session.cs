using AutoUIConsole.Components;
using AutoUIConsole.Components.DataTypes;
using System;

namespace AutoUIConsole
{
    public class Session
    {
        public static UserInterface UserInterface { get; set; }

        public Session(params string[] args)
        {
            try
            {
                var userInput = new UserInput(args);

                InitializeStartUpConfiguration();
                StartDirectOrMenu(userInput);
                HandleUserInput();
            }
            catch (Exception ae)
            {
                Console.WriteLine(ae);
                HandleUserInput();
            }
        }
        public void InitializeStartUpConfiguration()
        {
            var startUpSelectionOption = new Options(null, Config.DirLevel0);

            UserInterface = new UserInterface(startUpSelectionOption);
        }

        public void HandleUserInput()
        {
            while (true)
            {
                var userInput = new UserInput(Console.ReadLine());
                UserInterface.ExecuteSelection(userInput);
            }
        }

        public void StartDirectOrMenu(UserInput userInput)
        {
            if (!userInput.IsEmpty)
            {
                UserInterface.DirectStart(userInput);
                Environment.Exit(0);
            }
            else
            {
                UserInterface.ShowConsoleMenu();
            }
        }
    }
}