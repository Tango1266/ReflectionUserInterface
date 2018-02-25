using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using AutoUIConsole;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Helper = AutoUIConsole.Helper;

namespace UnitTests
{
    [TestClass]
    public class DirektStart
    {

        private const string SpezKF3Tc1 =
            "ExternApp.TestSuiten.Spezial.KF3EinWeiteresSpeziellesFeature.KF3EinWeiteresSpeziellesFeature - TC1";

        private const string SpezKF3Tc2 =
            "ExternApp.TestSuiten.Spezial.KF3EinWeiteresSpeziellesFeature.KF3EinWeiteresSpeziellesFeature - TC2";

        private const string SpezKF3Tc3 =
            "ExternApp.TestSuiten.Spezial.KF3EinWeiteresSpeziellesFeature.KF3EinWeiteresSpeziellesFeature - TC3";

        private const string SpezKF3TcTCdefiniert =
            "ExternApp.TestSuiten.Spezial.KF3EinWeiteresSpeziellesFeature.KF3EinWeiteresSpeziellesFeature - TCDasSollEineEindeutigDefinierteMethodeSein";

        public StringWriter StringWriter { get; private set; }

        public string[] ConsoleOut => StringWriter?.ToString()
            .Split(new[] {StringWriter.NewLine}, StringSplitOptions.RemoveEmptyEntries);

        private void AssertOutputContainesOnly(IEnumerable<string> collection)
        {
            AssertOutputContainesOnly(collection.ToArray());
        }

        private void AssertOutputContainesOnly(params string[] content)
        {
            var consoleOut = ConsoleOut.ToList();

            Assert.IsNotNull(consoleOut, "ConsoleOutput was not initialized");
            Assert.AreNotEqual(0, consoleOut.Count, "ConsoleOutput is empty but shouldnt");
            Assert.IsTrue(consoleOut.Contains("Direct Start reached its end."), "Direct start did not reach end");
            Assert.AreEqual(content.Length, consoleOut.Count - 1, "console content dimension was not as expected");

            foreach (var s in content)
            {
                Assert.IsTrue(consoleOut.Contains(s), $"{s} was not present in consoleOut: \n {Helper.ToText(consoleOut)}");
            }
        }

        private void AssertTestCaseExecutionWithOneArgument(string querry)
        {
            Program.Main(new[] {querry});
            AssertOutputContainesOnly(AlleTcs.Where(x => Regex.IsMatch(x, querry)));
        }

        [TestInitialize]
        public void SetUP()
        {
            StringWriter = new StringWriter();
            Console.SetOut(StringWriter);
            var spezialMethoden = Helper.GetMethodsFiltered(".*Spezial.*");
            var zentralMethoden = Helper.GetMethodsFiltered(".*Zentral.*");
            var alleMethoden = Helper.GetMethodsFiltered(".*TestSuiten.*");

            SpezialTcs = new List<string>();
            spezialMethoden.ForEach(x => SpezialTcs.Add(x.DeclaringType?.FullName + " - " + x.Name));

            ZentraleTcs = new List<string>();
            zentralMethoden.ForEach(x => ZentraleTcs.Add(x.DeclaringType?.FullName + " - " + x.Name));

            AlleTcs = new List<string>();
            alleMethoden.ForEach(x => AlleTcs.Add(x.DeclaringType?.FullName + " - " + x.Name));
        }

        public List<string> AlleTcs { get; set; }

        public List<string> ZentraleTcs { get; set; }

        public List<string> SpezialTcs { get; set; }

        [TestMethod]
        public void StartKF3TC3()
        {
            Program.Main(new[] {"KF3.*TC3"});

            AssertOutputContainesOnly(SpezKF3Tc3);
        }

        [TestMethod]
        public void StartKF3TC3_TC2_TC1()
        {
            Program.Main(new[] {"KF3.*TC3 KF3.*TC2 KF3.*TC1"});

            AssertOutputContainesOnly(SpezKF3Tc3, SpezKF3Tc2, SpezKF3Tc1);
        }

        [TestMethod]
        public void StarteAlleKFs()
        {
            Program.Main(new[] {"KF"});
            AssertOutputContainesOnly(AlleTcs);
        }

        [TestMethod]
        public void StarteAlleKF1()
        {
            AssertTestCaseExecutionWithOneArgument("KF1");
        }

        [TestMethod]
        public void StarteAlleKF2()
        {
            AssertTestCaseExecutionWithOneArgument("KF2");
        }

        [TestMethod]
        public void StarteAlleKF3()
        {
            AssertTestCaseExecutionWithOneArgument("KF3");
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
            AssertTestCaseExecutionWithOneArgument("KF2.*TS2");
        }

        [TestMethod]
        public void StarteMethode()
        {
            AssertTestCaseExecutionWithOneArgument("TCDasSollEineEindeutigDefinierteMethodeSein");
        }

        [TestMethod]
        public void MultiplyArguments()
        {
            var arg1 = "Spezial.*KF1.*UC1.*TC1";
            var arg2 = "KF2.*UC2.*TS2.*TC2";
            Program.Main(new[] {arg1, arg2});

            var expectedOutput = ConcatQuerry(arg1, arg2);

            AssertOutputContainesOnly(expectedOutput);
        }

        private List<string> ConcatQuerry(params string[] args)
        {
            var res = new List<string>();

            foreach (var arg in args)
            {
                res.AddRange(AlleTcs.Where(x => Regex.IsMatch(x, arg)).ToList());
            }
            return res;
        }

        [TestMethod]
        public void MultiplyArgumentsDevidedBySpace()
        {
            var arg1 = "Spezial.*KF1.*UC1.*TC1";
            var arg3 = "KF2.*UC2.*TS2.*TC2";
            Program.Main(new[] { arg1, arg3 });
            var expectedOutput = ConcatQuerry(arg1,  arg3);
            AssertOutputContainesOnly(expectedOutput);
        }
    }


}
