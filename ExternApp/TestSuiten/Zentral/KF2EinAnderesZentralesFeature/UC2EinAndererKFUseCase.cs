using ExternApp.Level1.Level2a;
using System;
using System.Threading.Tasks;

namespace ExternApp.TestSuiten.Zentral.KF2EinAnderesZentralesFeature
{
    public class UC2EinAndererKFUseCase
    {
        public void TC1()
        {
            Helpers.WriteMessage(this);
            Task.Delay(Config.DurationExecution);

        }

        public void TC2()
        {
            Helpers.WriteMessage(this);
            Task.Delay(Config.DurationExecution);

        }


    }
}
