using System;
using System.Diagnostics;

namespace ExternApp.Level1.Level2a
{
    internal static class Helpers
    {
        public static string ExecutedMethod(int stackPos) => new StackTrace().GetFrame(stackPos).GetMethod().Name;

        public static void WriteMessage(Object _object)
        {
            Console.WriteLine(_object.GetType().FullName + " - " + Helpers.ExecutedMethod(2));
            Debug.WriteLine(_object.GetType().FullName + " - " + Helpers.ExecutedMethod(2));

        }
    }
}