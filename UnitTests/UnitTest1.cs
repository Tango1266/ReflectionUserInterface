using AutoUIConsole;
using AutoUIConsole.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text.RegularExpressions;

namespace UnitTests
{
    [TestClass]
    public class AutoUiConsoleTest
    {
        [TestMethod]
        public void TestMenuClassInstance()
        {
            string level = "TestSuiten";

            new UserInterface(new SelectionOption(null, level));

            Assert.IsNull(Program.UserInterface.CurrentMenu.PreviousMenu);

            Assert.AreEqual(level, Program.UserInterface.CurrentSelection.Selection);
        }

        [TestMethod]
        public void TestMenuItemsInstance()
        {
            string level = "TestSuiten";

            new UserInterface(new SelectionOption(null, level));

            Program.UserInterface.CurrentMenu.PrintMenu();


            Assert.IsTrue(Program.UserInterface.CurrentMenu.MenuItems.Any(x => Regex.IsMatch(x, ".*TestSuiten.*")));
        }


        [TestMethod]
        public void TestAvailableCommandsMainMenu()
        {
            var expected = "GoToMainMenu";

            new Commands();

            Assert.IsTrue(Commands.AvailableCommands.Contains(expected));
        }

        [TestMethod]
        public void TestAvailableCommandsStartAll()
        {
            var expected = "StartAll";

            new Commands();

            Assert.IsTrue(Commands.AvailableCommands.Contains(expected));
        }


        [TestMethod]
        public void TestDirStructureGetsPath()
        {
            var selection = "ExternApp.TestSuiten.Spezial.KF2EinAnderesSpeziellesFeature.UC1EinUseCaseInKF2";
            var expected = "ExternApp.TestSuiten.Spezial.KF2EinAnderesSpeziellesFeature.UC1EinUseCaseInKF2.TS1EineTestSuite";

            var dir = Helper.GetDirStructure("", selection);


            Assert.AreEqual(dir[0], selection);
            Assert.AreEqual(dir[1], expected);

        }

        [TestMethod]
        public void TestDirStructureGetsPathInit()
        {
            var selection = "ExternApp.TestSuiten.Spezial";
            var Level1expected = "ExternApp.TestSuiten.Spezial.KF";
            var Level2expected = "ExternApp.TestSuiten.Spezial.KF.*.UC";
            var expected = "ExternApp.TestSuiten.Spezial.KF2EinAnderesSpeziellesFeature.UC1EinUseCaseInKF2.TS1EineTestSuite";

            var dir = Helper.GetDirStructure("", selection);


            Assert.AreEqual(dir[1], selection);

            Assert.AreEqual(dir[2], Level1expected);
            Assert.AreEqual(dir[3], Level2expected);
        }

    }
}
