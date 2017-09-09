using ExternApp.TestSuiten.Spezial.KF2EinAnderesSpeziellesFeature.UC1EinKF2UseCase;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AutoUIConsole
{
    public static class Config
    {
        public static Assembly AssemblyWhereToLookUp = Helper.GetLookUpAssembly(typeof(TS1EineTestSuite));
        public static string DirLevel0 = "TestSuiten";

        public struct Commands
        {
            //TODO:Get Method Names dynamically
            public static List<string> GoToMainMenu = new List<string> { "main", "home", "m" };
            public static List<string> GoToPreviousnMenu = new List<string> { "back", "b" };
            public static List<string> StartAllDisplayedTests = new List<string> { "StartAll", "start", "s" };
            public static List<string> ExitApplication = new List<string> { "exit", "quit", "q" };
            public static List<string> GetHelp = new List<string> { "help", "h" };
        }

        public struct MenuTexts
        {
            public static string Introduction = "";
            public static string InputNotefication =
                "\n Triff eine Auswahl und bestätige mit {Enter}. " +
                $"\n Gebe \"{Commands.GetHelp.ToText()}\" ein um weitere Befehle ausgeben zu lassen";

            public static string HelpText =
                "\n\t\t === Help ===\n " +
                "Main Menu \t\t\t" + Commands.GoToMainMenu.ToText() + "\n" +
                "Previous Menu \t\t\t" + Commands.GoToPreviousnMenu.ToText() + "\n" +
                "Start all Methods on this level \t" + Commands.StartAllDisplayedTests.ToText() + "\n" +
                "Exit Apllication \t\t\t" + Commands.ExitApplication.ToText() + "\n" +
                "\n\n " +
                "Beispiel:" + Commands.GoToMainMenu.First() + " <Enter>";
        }

        public struct PrefixDirLevel
        {
            //Ein KeyFeature kann sich aus mehreren UseCases zusammensetzen
            //Ein KeyFeature oder ein UseCase kann eine TestSuit
            //Eine TestSuit hat mindestens ein TestCase
            //Eine TestSuit ist eine Klasse mit TestCases
            //Ein TestCase ist immer eine Methode
            //KeyFeature und UseCases können Ordner sein oder bilden eine Klasse an Stelle einer TestSuit.
            public static string MainLevel = "KF";
            public static string SubLevel2 = "UC";
            public static string SubLevel3 = "TS";
            public static string SubLevel4 = "TC";
        }
    }
}