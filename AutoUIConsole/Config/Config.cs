using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AutoUIConsole
{
    public static class Config
    {
        //public static Assembly AssemblyWhereToLookUp = Helper.GetLookUpAssembly(typeof(TS1EineTestSuite));
        public static Assembly AssemblyWhereToLookUp = LoadAssemblyFromConfig();
        public static string DirLevel0 = DefineDirLevel0();

        public struct Commands
        {
            ////TODO:Get Method Names dynamically
            public static List<string> GoToMainMenu = new List<string> { "main", "home", "m" };
            public static List<string> GoToPreviousnMenu = new List<string> { "back", "b" };
            public static List<string> StartAllMethods = new List<string> { "StartAll", "start", "s" };
            public static List<string> ExitApplication = new List<string> { "exit", "quit", "q" };
            public static List<string> GetHelp = new List<string> { "help", "h" };
            public static List<string> SaveMethodsOfCurrentOptions = new List<string> { "SaveMethodsOfCurrentOptions", "save" };
            public static List<string> ShowManual = new List<string> { "ShowManual", "manual" };
        }

        public struct MenuTexts
        {
            public static string Storage(string filename = "") => Path.Combine(@"Config\MenuText", filename);

            public static string Introduction = File.ReadAllText(Storage("Introduction.txt"));
            public static string InputNotefication = File.ReadAllText(Storage("InputNotefication.txt"));
            public static string Manual = File.ReadAllText(Storage("Manual.txt"));

            //public static string HelpText =
            //    NewLine +
            //    "\t\t === Help ===" +
            //    NewLine +
            //    "  Beschreibung \t\t\t" + "Befehl(e)" +
            //    NewLine +
            //    "Main Menu \t\t\t" + Commands.GoToMainMenu.ToText() +
            //    NewLine +
            //    "Anleitung anzeigen \t\t\t" + Commands.ShowManual.ToText() +
            //    NewLine +
            //    "Previous Menu \t\t\t" + Commands.GoToPreviousnMenu.ToText() +
            //    NewLine +
            //    "Start all Methods on this level \t" + Commands.StartAllMethods.ToText() +
            //    NewLine +
            //    "Exit Apllication \t\t\t" + Commands.ExitApplication.ToText() +
            //    NewLine +
            //    NewLine +
            //    "Beispiel:" + Commands.GoToMainMenu.First() + " <Enter>";
        }

        public static string GetValueFromAppConfig(string key)
        {
            var appSettings = ConfigurationManager.AppSettings;

            if (!appSettings.AllKeys.Contains(key)) throw new KeyNotFoundException($"Das Configurationsitem \"{key}\" wurde nicht in {AppDomain.CurrentDomain.SetupInformation.ConfigurationFile} gefunden");

            return ConfigurationManager.AppSettings[key];
        }

        private static Assembly LoadAssemblyFromConfig()
        {
            var configValue = GetValueFromAppConfig(nameof(AssemblyWhereToLookUp));

            return Assembly.LoadFrom(configValue);
        }

        private static string DefineDirLevel0()
        {
            var dirLevel0 = GetValueFromAppConfig(nameof(DirLevel0));

            if (dirLevel0.Equals(string.Empty)) dirLevel0 = AssemblyWhereToLookUp.GetName().Name;

            return dirLevel0;
        }
    }
}