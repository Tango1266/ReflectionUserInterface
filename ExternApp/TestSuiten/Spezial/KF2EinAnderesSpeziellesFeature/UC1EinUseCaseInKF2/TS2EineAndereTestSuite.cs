using ExternApp.Level1.Level2a;
using System;

namespace ExternApp.TestSuiten.Spezial.KF2EinAnderesSpeziellesFeature.UC1EinUseCaseInKF2
{
    public class TS2EineAndereTestSuite
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
