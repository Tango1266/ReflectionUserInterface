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
        private UserInterface _userInterface;

        [TestInitialize]
        public void Testinit()
        {
            Program.CurrentSession.InitializeStartUpConfiguration();

            _userInterface = Session.UserInterface;
        }

        [TestMethod]
        public void TestMenuClassInstance()
        {
            string level = "TestSuiten";

            var uiInterface = new UserInterface(new Selection(null, level));
            uiInterface.ShowConsoleMenu();

            Assert.IsNull(uiInterface.CurrentMenu.PreviousMenu);

            Assert.AreEqual(".*" + level, uiInterface.currentSelection.Query);
        }

        [TestMethod]
        public void TestMenuItemsInstance()
        {
            string dirLevel0 = "TestSuiten";
            string[] expTargetLevel = new[]
            {
                "Spezial",
                "Zentral"
            };
            TestMenuItemInstances(dirLevel0, expTargetLevel);
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
            var selection = "TestSuiten";
            var selectionOption = new Selection(null, selection);
            var path = "ExternApp.TestSuiten.Spezial.KF2EinAnderesSpeziellesFeature.UC1EinUseCaseInKF2.TS1EineTestSuite";

            var dir = Helper.GetDirStructure(selectionOption, path);


            Assert.AreEqual(selection, dir[0]);
            Assert.AreEqual("Spezial", dir[1]);
        }


        [TestMethod]
        public void TestMenuItemsSpezialKF1()
        {
            string selection = "Spezial.*KF1";
            string[] expTargetLevel = new[]
            {
                "KF1EinSpeziellesFeature",
            };

            TestMenuItemInstances(selection, expTargetLevel);
        }

        [TestMethod]
        public void TestMenuItemsSpezialKF1_UCs()
        {
            string selection = "Spezial.*KF1.*UC";
            string[] expTargetLevel = new[]
            {
                "UC1EinUseCase",
                "UC2EinAndererUseCase",
            };

            TestMenuItemInstances(selection, expTargetLevel);
        }

        [TestMethod]
        public void TestMenuItemsSpezialKF2UC_TS()
        {
            string selection = "Spezial.*KF2.*UC.*TS";
            string[] expTargetLevel = new[]
            {
                "TS1EineTestSuite",
                "TS2EineAndereTestSuite",
            };

            TestMenuItemInstances(selection, expTargetLevel);
        }

        [TestMethod]
        public void TestMenuItemsSpezialKF3UC_TCs()
        {
            string selection = "Spezial.*KF3.*TC";
            string[] expTargetLevel = new[]
            {
                "TC1",
                "TC2",
                "TC3",
            };

            TestMenuItemInstances(selection, expTargetLevel);
        }

        private void TestMenuItemInstances(string selection, params string[] expTargetLevels)
        {
            _userInterface.HandleCustomeInput(selection);

            foreach (string expTargetLevel in expTargetLevels)
            {
                Assert.IsTrue(_userInterface.CurrentMenu.MenuItems.Any(x => Regex.IsMatch(x, expTargetLevel)));
            }

        }
    }
}
