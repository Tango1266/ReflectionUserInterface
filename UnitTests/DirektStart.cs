using AutoUIConsole;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class DirektStart
    {
        [TestMethod]
        public void InitialStart()
        {
            Program.Main(new[] { "KF3.*TC3" });
        }

        [TestMethod]
        public void StarteAlleKFs()
        {
            Program.Main(new[] { "KF" });
        }

        [TestMethod]
        public void StarteAlleKF1()
        {
            Program.Main(new[] { "KF1" });
        }

        [TestMethod]
        public void StarteAlleKF2()
        {
            Program.Main(new[] { "KF2" });
        }

        [TestMethod]
        public void StarteAlleKF3()
        {
            Program.Main(new[] { "KF3" });
        }

        [TestMethod]
        public void StarteUC1vonKF1()
        {
            Program.Main(new[] { "KF1.*UC1" });
        }

        [TestMethod]
        public void StarteTS2vonKF2()
        {
            Program.Main(new[] { "KF2.*TS2" });
        }

        [TestMethod]
        public void StarteMethode()
        {
            Program.Main(new[] { "TCDasSollEineEindeutigDefinierteMethodeSein" });
        }

        [TestMethod]
        public void MultiplyArguments()
        {
            Program.Main(new[] { "KF3 KF1.*UC1" });
        }
    }
}
