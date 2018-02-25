using ExternApp.Level1.Level2a;
using System;
using System.Threading.Tasks;

namespace ExternApp.TestSuiten.Spezial.KF1EinSpeziellesFeature
{
    public class UC2EinAndererUseCase
    {
        public void TC1()
        {
            Helpers.WriteMessage(this);
            Task.Delay(Config.DurationExecution);

        }

    }
}
