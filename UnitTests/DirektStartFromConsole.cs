using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using AutoUIConsole;
using AutoUIConsole.Components;
using AutoUIConsole.Components.DataTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Helper = AutoUIConsole.Helper;

namespace UnitTests
{
    [TestClass]
    public class DirektStartFromConsole:DirektStart
    {
        private UserInterface UserInterface { get; set; }

        protected void AssertOutputContainesOnly(params string[] content)
        {
            var consoleOut = ConsoleOut.ToList();

            Assert.IsNotNull(consoleOut, "ConsoleOutput was not initialized");
            Assert.AreNotEqual(0, consoleOut.Count, "ConsoleOutput is empty but shouldnt");
            Assert.AreEqual(content.Length, consoleOut.Count, "console content dimension was not as expected");

            foreach (var s in content)
            {
                Assert.IsTrue(consoleOut.Contains(s), $"{s} was not present in consoleOut: \n {Helper.ToText(consoleOut)}");
            }
        }

        protected void AssertTestCaseExecutionWithOneArgument(string querry)
        {
            UserInterface.HandleUserInput(new UserInput("s "+querry));
            AssertOutputContainesOnly(AlleTcs.Where(x => Regex.IsMatch(x, querry)));
        }

        protected void AssertOutputContainesOnly(IEnumerable<string> collection)
        {
            AssertOutputContainesOnly(collection.ToArray());
        }

        [ClassInitialize]
        public static void SetUpClass(TestContext context)
        {
            DirektStart.SetUpClass(context);
        }

        [TestInitialize]
        public void SetUpMethod()
        {
            Program.CurrentSession = new Session();
            Program.CurrentSession.InitializeStartUpConfiguration();
            UserInterface = Session.UserInterface;
            StringWriter = new StringWriter();
            Console.SetOut(StringWriter);
        }

        [TestMethod]
        public void StartKF3TC3()
        {
            UserInterface.HandleUserInput(new UserInput("s KF3.*TC3"));
            AssertOutputContainesOnly(SpezKF3Tc3);
        }

        [TestMethod]
        public void StartKF3TC3_TC2_TC1()
        {
            var querry = "s KF3.*TC3 KF3.*TC2 KF3.*TC1";
            UserInterface.HandleUserInput(new UserInput(querry));
            AssertOutputContainesOnly(SpezKF3Tc3, SpezKF3Tc2, SpezKF3Tc1);
        }

        [TestMethod]
        public void StarteAlleKFs()
        {
            var querry = "s KF";
            UserInterface.HandleUserInput(new UserInput(querry));
            AssertOutputContainesOnly(AlleTcs);
        }

        [TestMethod]
        public void StarteAlleKF1()
        {
            var querry = "KF1";
            AssertTestCaseExecutionWithOneArgument(querry);
        }

        [TestMethod]
        public void StarteAlleKF2()
        {
            var querry = "KF2";
            AssertTestCaseExecutionWithOneArgument(querry);
        }

        [TestMethod]
        public void StarteAlleKF3()
        {
            var querry = "KF3";
            AssertTestCaseExecutionWithOneArgument(querry);
        }

        [TestMethod]
        public void StarteUC1vonKF1()
        {
            var querry = "KF1.*UC1";
            AssertTestCaseExecutionWithOneArgument(querry);

     
        }

        [TestMethod]
        public void StarteTS2vonKF2()
        {
            var querry = "KF2.*TS2";
            AssertTestCaseExecutionWithOneArgument(querry);

        }

        [TestMethod]
        public void StarteMethode()
        {
            var querry = "TCDasSollEineEindeutigDefinierteMethodeSein";
            AssertTestCaseExecutionWithOneArgument(querry);
        }
    }


}
