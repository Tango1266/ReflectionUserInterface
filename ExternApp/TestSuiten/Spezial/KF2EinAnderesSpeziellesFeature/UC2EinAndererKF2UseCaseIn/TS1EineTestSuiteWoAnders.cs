using System.Threading.Tasks;
using ExternApp.Level1.Level2a;

namespace ExternApp.TestSuiten.Spezial.KF2EinAnderesSpeziellesFeature.UC2EinAndererKF2UseCaseIn
{
    public class TS1EineTestSuiteWoAnders
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
