using System;
using AutoUIConsole.Components;

namespace AutoUIConsole
{
    public class Program
    {
        //TODO: Feature1: merhre Befehle/Methoden uber commandline ausfuhren (eingabe von mehreren Argumenten mit leerzeichen getrennt)
        //TODO: Feature3: Dynamische Erstellung und initialisierung von Properties in den Command Klassen mittels Reflection und der AppConfig
        public static Session CurrentSession { get; set; }

        public static void Main(string[] args)
        {
           // var xml = new XMLDocComment(typeof(ExitApplication), "q");
            try
            {
                CurrentSession = new Session(args);
                CurrentSession.Start();
            }
            catch (Exception ex)
            {
                Helper.Log("AutoUIConsole ran into some issues", ex);
            }
        }
    }
}
