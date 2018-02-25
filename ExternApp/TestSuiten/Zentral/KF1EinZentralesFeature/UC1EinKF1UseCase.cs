using ExternApp.Level1.Level2a;
using System;
using System.Threading.Tasks;

namespace ExternApp.TestSuiten.Zentral.KF1EinZentralesFeature
{
    public class UC1EinKF1UseCase
    {

        public void TC1()
        {
            Helpers.WriteMessage(this);
            Task.Delay(Config.DurationExecution);

        }

    }
}
