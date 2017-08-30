using System.Collections.Generic;
using System.Reflection;
using ExternApp.TestSuiten.Spezial.KF2EinAnderesSpeziellesFeature.UC1EinKF2UseCase;

namespace AutoUIConsole
{
    public static class Config
    {
        public static Assembly AssemblyWhereToLookUp = Helper.GetLookUpAssembly(typeof(TS1EineTestSuite));
        public static string DirLevel0 = "TestSuiten";

        public static string ToText(this List<string> list)
        {
            string res = "";

            list.ForEach(x => res += x + " | ");

            return res;
        }

        public struct Commands
        {
            public static List<string> GoToMainMenu = new List<string> { "main", "home", "m" };
            public static List<string> GoToPreviousnMenu = new List<string> { "back", "b" };
            public static List<string> StartAllDisplayedTests = new List<string> { "s", "start", "StartAll" };
            public const string GetHelp = "help";

        }

        public struct MenuTexts
        {
            public static string HauptMenuCommand = "";
            public static string Introduction = "";
            public static string InputNotefication =
                "\n Triff eine Auswahl und bestätige mit {Enter}. " +
                $"\n Gebe \"{Config.Commands.GetHelp}\" ein um weitere Befehle ausgeben zu lassen";

            public static string HelpText =
                "Main Menu \t" + Config.Commands.GoToMainMenu + "\n" +
                "Previous Menu \t" + Config.Commands.GoToPreviousnMenu + "\n";
        }

        public struct RegexPattern
        {
            public const string AnyChars = ".*";
            public const string dot = @"\.";
            public const string endsWithTwoDigitsFollowingByWordChars = @"d{0,2}\w?$";
            public const string ConsistOnlyOfDigits = @"\b\d+$";
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