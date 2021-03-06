﻿using AutoUIConsole.Components;
using System;

namespace AutoUIConsole
{
    public class Program
    {
        //TODO: Feature1: merhre Befehle/Methoden uber commandline ausfuhren (eingabe von mehreren Argumenten mit leerzeichen getrennt)
        //TODO: Feature2: Direktstart funktionalitaet implementieren ohne Menu
        //TODO: Feature3: Dynamische Erstellung und initialisierung von Properties in den Command Klassen mittels Reflection und der Config


        public static void Main(string[] args)
        {
            try
            {
                InterfaceControl.InitializeStartUpConfiguration();
                InterfaceControl.StartDirectOrMenu(args);
                InterfaceControl.HandleUserInput();
            }
            catch (Exception ae)
            {
                Console.WriteLine(ae);
                InterfaceControl.HandleUserInput();
            }
        }
    }
}
