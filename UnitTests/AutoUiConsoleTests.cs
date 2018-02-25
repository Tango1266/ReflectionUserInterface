using AutoUIConsole;
using AutoUIConsole.Components;
using AutoUIConsole.Components.DataTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text.RegularExpressions;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace UnitTests
{
    //TODO: Tests ueberarbeiten, funktionieren nicht mehr
    [TestClass]
    public class MenuTests
    {
        private UserInterface _userInterface;

        [TestInitialize]
        public void Testinit()
        {
            Program.CurrentSession = new Session();
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

            Assert.AreEqual(".*" + level, uiInterface.CurrentSelection.Query);
        }

        [TestMethod]
        public void TestMenuItemsOnStartup()
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
        public void TestMenuItems_SpezialKF1()
        {
            string selection = "Spezial.*KF1";
            string querrySpaceSeperated = "Spezial KF1";

            string[] expTargetLevel = new[]
            {
                "KF1EinSpeziellesFeature",
            };

            TestMenuItemInstances(selection, expTargetLevel);
            TestMenuItemInstances(querrySpaceSeperated, expTargetLevel);
        }

        [TestMethod]
        public void TestMenuItems_SpezialKF1_UCs()
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
        public void TestMenuItems_SpezialKF2UC_TS()
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
        public void TestMenuItems_SpezialKF3UC_TCs()
        {
            string querryWhole = "Spezial.*KF3.*TC";

            string[] expTargetLevel = new[]
            {
                "TC1",
                "TC2",
                "TC3",
            };
            TestMenuItemInstances(querryWhole, expTargetLevel);
        }

        [TestMethod]
        public void TestMenuItems_SpezialKF3UC_TCs_SpaceSeperated()
        {
            string querrySpaceSeperated = "Spezial KF3 TC";

            string[] expTargetLevel = new[]
            {
                "TC1",
                "TC2",
                "TC3",
            };
            TestMenuItemInstances(querrySpaceSeperated, expTargetLevel);
        }



        private void TestMenuItemInstances(string querry, params string[] expTargetLevels)
        {
            _userInterface.HandleCustomeInput(new UserInput(querry));

            foreach (string expTargetLevel in expTargetLevels)
            {
                var currentMenu = _userInterface.CurrentMenu;
                Assert.IsTrue(currentMenu.MenuItems.Any(x => Regex.IsMatch(x, expTargetLevel)), $"Expected level {expTargetLevel} was not found for querry {querry} in menu \n {currentMenu.MenuItems.ToText()}");
            }
        }
    }
}
