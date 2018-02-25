using System.Threading.Tasks;
using ExternApp.Level1.Level2a;

namespace ExternApp.TestSuiten.Spezial.KF2EinAnderesSpeziellesFeature.UC1EinKF2UseCase
{
    public class TS1EineTestSuite
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
