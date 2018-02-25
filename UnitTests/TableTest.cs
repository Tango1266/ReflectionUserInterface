using AutoUIConsole.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using AutoUIConsole;

namespace UnitTests
{
    [TestClass]
    public class TableTest
    {
        [TestMethod]
        public void SpalteHinzufuegen()
        {
            Table tabelle = new Table();

            var column = "Spalte1";
            tabelle.AddColumn(column);

            Assert.AreEqual(column, tabelle.Columns[0].ToString(),$"Die Spalte {column} war nicht enthalten in {tabelle.Columns.ToText()}");
        }

        [TestMethod]
        public void ZweiSpalteHinzufuegen()
        {
            Table tabelle = new Table();

            var column = new[] { "Spalte1", "Spalte2" };
            tabelle.AddColumn(column);
            Assert.AreEqual(column.ToText(), tabelle.Columns.ToText(), $"Die Spalte {column.ToText()} war nicht enthalten in {tabelle.Columns.ToText()}");
        }

        [TestMethod]
        public void ZeileHinzufuegen()
        {
            Table tabelle = new Table();
            var row = new[] { "Zeile1"};
            tabelle.AddRow(row);
            Assert.AreEqual(row.ToText(), tabelle.Rows.ToText(), $"Die Spalte {row.ToText()} war nicht enthalten in {tabelle.Rows.ToText()}");
        }

   
        [TestMethod]
        public void ZweiZeilenHinzufuegen()
        {
            Table tabelle = new Table();
            var row = new[] { "Zeile1", "Zeile1" };
            tabelle.AddRow(row);
            Assert.AreEqual(row.ToText(), tabelle.Rows.ToText(), $"Die Spalte {row.ToText()} war nicht enthalten in {tabelle.Rows.ToText()}");
        }

        [TestMethod]
        public void ZweiSymbolSeperierteZeilenHinzufuegen()
        {
            Table tabelle = new Table();
            var seperator = tabelle.Seperator;

            var row = new[] { "Zeile1", "Zeile1" };
            tabelle.AddRow(row);
            Assert.AreEqual(row.ToText(), tabelle.Rows.ToText(), $"Die Spalte {row.ToText()} war nicht enthalten in {tabelle.Rows.ToText()}");
        }
    }
}
