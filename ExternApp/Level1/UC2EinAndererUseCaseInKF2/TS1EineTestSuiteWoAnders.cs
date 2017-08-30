using System;
using ExternApp.Level1.Level2a;

namespace ExternApp.Level1.UC2EinAndererUseCaseInKF2
{
    public class TS1EineTestSuiteWoAnders
    {
        public void TC1()
        {
            Console.WriteLine(this.GetType().FullName + " - " + Helpers.ExecutedMethod);
        }

        public void TC2()
        {
            Console.WriteLine(this.GetType().FullName + " - " + Helpers.ExecutedMethod);
        }

        }
}
