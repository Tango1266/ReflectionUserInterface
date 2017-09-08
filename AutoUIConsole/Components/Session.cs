using AutoUIConsole.Components;
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
                InitializeStartUpConfiguration();
                StartDirectOrMenu(args);
                HandleUserInput();
            }
            catch (Exception ae)
            {
                Console.WriteLine(ae);
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
                var selection = Console.ReadLine();
                //TODO: Evtl: Entferne Whitespaces und Zeichen aus selection
                UserInterface.ExecuteSelection(selection);
            }
        }

        public void StartDirectOrMenu(string[] args)
        {
            if (args.Length > 0)
            {
                UserInterface.DirectStart(args);
                Environment.Exit(0);
            }
            else
            {
                UserInterface.ShowConsoleMenu();
            }
        }
    }
}