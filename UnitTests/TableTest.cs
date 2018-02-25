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
            Assert.AreEqual(row.ToText(), tabelle.Rows.ToText(), $"Die Zeile {row.ToText()} war nicht enthalten in {tabelle.Rows.ToText()}");
        }

   
        [TestMethod]
        public void ZweiZeilenHinzufuegen()
        {
            Table tabelle = new Table();
            var row = new[] { "Zeile11", "Zeile12" };
            tabelle.AddRow(row);
            Assert.AreEqual("Zeile11;Zeile12", tabelle.Rows.ToText(), $"Die Zeile {row.ToText()} war nicht enthalten in {tabelle.Rows.ToText()}");
        }

        [TestMethod]
        public void ZweiSymbolSeperierteZeilenHinzufuegen()
        {
            Table tabelle = new Table();
            var seperator = tabelle.Seperator[0];

            var row =  "Zeile11;Zeile12" ;
            tabelle.AddRow(row);
            Assert.AreEqual("Zeile11"+ seperator+"Zeile12", tabelle.Rows.ToText(), $"Die Zeile {row} war nicht enthalten in {tabelle.Rows.ToText()}");
        }

        [TestMethod]
        public void ZweiSymbolMitCustomSeperierteZeilenHinzufuegen()
        {
            Table tabelle = new Table();
            tabelle.Seperator = new[] {","};

            var seperator = tabelle.Seperator[0];

            var row = "Zeile11,Zeile12";
            tabelle.AddRow(row);
            Assert.AreEqual("Zeile11" + seperator + "Zeile12", tabelle.Rows.ToText(), $"Die Zeile {row} war nicht enthalten in {tabelle.Rows.ToText()}");
        }

        [TestMethod]
        public void BefuellenEinerZeileMitSpalten()
        {
            var tabelle = CreateTable_NameAlterHaarfarbe();

            var seperator = tabelle.Seperator[0];
            Assert.AreEqual(3,tabelle.Columns.Count,"anzahl spalten stimmt nicht");
            Assert.AreEqual(1,tabelle.Rows.Count, "anzahl zeilen stimmt nicht");

            var expRow = "Hans" + seperator + "22" + seperator +"braun";
            Assert.AreEqual(expRow, tabelle.Rows.ToText(), $"Die Zeile {expRow} war nicht enthalten in {tabelle.Rows.ToText()}");
            var expCol = "Name Alter Haarfarbe";
            Assert.AreEqual(expCol, tabelle.Columns.ToText(), $"Die Zeile {expCol} war nicht enthalten in {tabelle.Columns.ToText()}");
        }

        [TestMethod]
        public void ZeichneEineTabelle()
        {
            var tabelle = CreateTable_NameAlterHaarfarbe();
            tabelle.AddRow("Maria;21;blond");
            tabelle.Draw();
        }

        [TestMethod]
        public void FuegeEineNeueZeileEinerBestehendenTabeleHinzu()
        {
            var tabelle = CreateTable_NameAlterHaarfarbe();
            tabelle.AddRow("Maria;21;blond");
            var seperator = tabelle.Seperator[0];

            Assert.AreEqual(3, tabelle.Columns.Count, "anzahl spalten stimmt nicht");
            Assert.AreEqual(2, tabelle.Rows.Count, "anzahl zeilen stimmt nicht");

            var expRow1 = "Hans" + seperator + "22" + seperator + "braun";
            var expRow2 = "Maria" + seperator + "21" + seperator + "blond";
            Assert.AreEqual(expRow1, tabelle.Rows[0].ToString(), $"Die Zeile {expRow1} war nicht enthalten in {tabelle.Rows.ToText()}");
            Assert.AreEqual(expRow2, tabelle.Rows[1].ToString(), $"Die Zeile {expRow2} war nicht enthalten in {tabelle.Rows.ToText()}");

            var expCol = "Name" + seperator + "Alter" + seperator + "Haarfarbe";
            Assert.AreEqual(expCol, tabelle.Columns.ToText(tabelle.Seperator), $"Die Zeile {expCol} war nicht enthalten in {tabelle.Columns.ToText()}");
        }

        private static Table CreateTable_NameAlterHaarfarbe()
        {
            Table tabelle = new Table();
            tabelle.AddColumn("Name", "Alter", "Haarfarbe");

            Column name = new Column(tabelle, "Name");
            Column alter = new Column(tabelle, "Alter");
            Column haarfarbe = new Column(tabelle, "Haarfarbe");

            Row zeile1 = new Row(tabelle);
            tabelle.AddItem(name, zeile1, "Hans");
            tabelle.AddItem(alter, zeile1, "22");
            tabelle.AddItem(haarfarbe, zeile1, "braun");
            return tabelle;
        }
    }
}
