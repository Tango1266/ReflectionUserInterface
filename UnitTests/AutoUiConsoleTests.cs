﻿using AutoUIConsole;
using AutoUIConsole.Components;
using AutoUIConsole.Components.DataTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text.RegularExpressions;

namespace UnitTests
{
    //TODO: Tests ueberarbeiten, funktionieren nicht mehr
    [TestClass]
    public class AutoUiConsoleTest
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


        }

        [TestMethod]
        public void TestAvailableCommandsStartAll()
        {
            var expected = "StartAll";


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
            _userInterface.HandleCustomeInput(new UserInput(selection));

            foreach (string expTargetLevel in expTargetLevels)
            {
                Assert.IsTrue(_userInterface.CurrentMenu.MenuItems.Any(x => Regex.IsMatch(x, expTargetLevel)));
            }

        }
    }
}
