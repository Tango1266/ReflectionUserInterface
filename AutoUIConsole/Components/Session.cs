using System;
using AutoUIConsole.Components.Commands;
using AutoUIConsole.Components.DataTypes;

namespace AutoUIConsole.Components
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
                Helper.WriteLine(ae.ToString());
                HandleUserInput();
            }
        }
        public void InitializeStartUpConfiguration()
        {
            var startUpSelectionOption = new Selection(null, Config.DirLevel0);

            SuperCommand.Init();
            UserInterface = new UserInterface(startUpSelectionOption);
        }

        public void HandleUserInput()
        {
            while (true)
            {
                var userInput = new UserInput(Console.ReadLine());
                UserInterface.HandleUserInput(userInput);
            }
        }

        public void StartDirectOrMenu(UserInput userInput)
        {
            if (!userInput.IsEmpty)
            {
                UserInterface.DirectStart(userInput);
                new ExitApplication().Execute();
            }
            else
            {
                Helper.WriteLine(Config.MenuTexts.Introduction);
                Console.Read();

                UserInterface.ShowConsoleMenu();
            }
        }
    }
}