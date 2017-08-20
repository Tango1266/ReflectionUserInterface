using System;
using ExternApp.Level1.Level2a;

namespace ExternApp.TestSuiten.Zentral.KF1EinZentralesFeature
{
    public class UC1EinKF1UseCase
    {

        public void TC1()
        {
            Console.WriteLine(this.GetType().FullName + " - " + Helpers.ExecutedMethod);
        }

        public void TC2()
        {
            Console.WriteLine(this.GetType().FullName + " - " + Helpers.ExecutedMethod);
        }

        public void TC3()
        {
            Console.WriteLine(this.GetType().FullName + " - " + Helpers.ExecutedMethod);
        }
    }
}
