using System.Diagnostics;

namespace ExternApp.Level1.Level2a
{
    internal static class Helpers
    {
        public static string ExecutedMethod => new StackTrace().GetFrame(1).GetMethod().Name;


    }
}