using AutoUIConsole.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class TableTest
    {
        [TestMethod]
        public void SpalteHinzufuegen()
        {
            Table tabelle = new Table();

            tabelle.AddColumn("Spalte1");

            Assert.AreEqual("Spalte1", tabelle.Columns.First());
        }
    }
}
