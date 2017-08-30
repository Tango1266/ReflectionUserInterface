using AutoUIConsole.Components;
using System;

namespace AutoUIConsole
{
    public class Program
    {
        public static UserInterface UserInterface;

        static void Main(string[] args)
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
                HandleUserInput();
            }
        }

        private static void HandleUserInput()
        {
            while (true)
            {
                var selection = Console.ReadLine();
                //TODO: Entferne Whitespaces und Zeichen aus selection
                UserInterface.ExecuteSelection(selection);
            }
        }

        private static void StartDirectOrMenu(string[] args)
        {
            if (args.Length > 0)
            {
                UserInterface.HandleSelection(args);
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
