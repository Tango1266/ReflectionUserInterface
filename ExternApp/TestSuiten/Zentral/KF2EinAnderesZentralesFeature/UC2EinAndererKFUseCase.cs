using ExternApp.Level1.Level2a;
using System;

namespace ExternApp.TestSuiten.Zentral.KF2EinAnderesZentralesFeature
{
    public class UC2EinAndererKFUseCase
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
