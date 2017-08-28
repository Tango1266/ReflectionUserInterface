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
                StartDirectOrMenu(args);

                HandleUserInput();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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
            var startUpSelectionOption = new SelectionOption(null, Config.DirLevel0);

            UserInterface = new UserInterface(startUpSelectionOption);

            if (args.Length > 0)
            {
                UserInterface.HandleSelection(args);
            }
            else
            {
                UserInterface.ShowMenu();
            }
        }
    }
}
