using System;
using AutoUIConsole.Components.Commands;
using AutoUIConsole.Components.DataTypes;

namespace AutoUIConsole.Components
{
    public class Session
    {
        private UserInput _userInput;
        public static UserInterface UserInterface { get; set; }
        public static bool IsConsoleSession { get; set; }
        public static bool IsDirectStart { get; set; }

        public Session(params string[] args)
        {
            try
            {
                _userInput = new UserInput(args);
                InitializeStartUpConfiguration();
            }
            catch (Exception ae)
            {
                Helper.Log(ae.ToString());
                if (IsConsoleSession) HandleUserInput();
            }
        }

        public void Start()
        {
            try
            {
                StartDirectOrMenu(_userInput);
                if (IsConsoleSession) HandleUserInput();
            }
            catch (ExitApplicationException exit)
            {
                Helper.Log(exit.Message);
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
                IsDirectStart = true;
                UserInterface.DirectStart(userInput);
                throw new ExitApplicationException("Direct Start reached its end.");
            }
        
            Helper.Log(Config.MenuTexts.Introduction);
            Console.Read();
            IsConsoleSession = true;
            UserInterface.ShowConsoleMenu();
        }
    }

    public class ExitApplicationException : Exception
    {
        public ExitApplicationException(string message) : base(message)
        {
        }
    }
}