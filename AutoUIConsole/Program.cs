using AutoUIConsole.Components;
using System;

namespace AutoUIConsole
{
    public class Program
    {
        public static UserInterface UserInterface;

        static void Main(string[] args)
        {
            UserInterface = new UserInterface(Config.DirLevel0);

            while (true)
            {
                var selection = Console.ReadLine();

                //TODO: Entferne Whitespaces und Zeichen aus selection
                try
                {
                    UserInterface.ExecuteSelection(selection);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
