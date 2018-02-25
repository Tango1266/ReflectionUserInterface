using System.Threading.Tasks;
using ExternApp.Level1.Level2a;

namespace ExternApp.TestSuiten.Spezial.KF3EinWeiteresSpeziellesFeature
{
    public class KF3EinWeiteresSpeziellesFeature
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

        public void TC3()
        {
            Helpers.WriteMessage(this);
            Task.Delay(Config.DurationExecution);


        }

        public void TCDasSollEineEindeutigDefinierteMethodeSein()
        {
            Helpers.WriteMessage(this);
            Task.Delay(Config.DurationExecution);


        }
    }
}
