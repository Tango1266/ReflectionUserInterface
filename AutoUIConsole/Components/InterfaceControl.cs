using AutoUIConsole.Components;
using System;

namespace AutoUIConsole
{
    public class InterfaceControl
    {
        public static UserInterface UserInterface { get; set; }


        public static void HandleUserInput()
        {
            while (true)
            {
                var selection = Console.ReadLine();
                //TODO: Evtl: Entferne Whitespaces und Zeichen aus selection
                UserInterface.ExecuteSelection(selection);
            }
        }

        public static void StartDirectOrMenu(string[] args)
        {
            if (args.Length > 0)
            {
                UserInterface.DirectStart(args);
                Environment.Exit(0);
            }
            else
            {
                UserInterface.ShowMenu();
            }
        }

        public static void InitializeStartUpConfiguration()
        {
            var startUpSelectionOption = new SelectionOption(null, Config.DirLevel0);

            UserInterface = new UserInterface(startUpSelectionOption);
        }

    }
}