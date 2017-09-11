using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace AutoUIConsole.Components
{
    public partial class Commands
    {
        public void help()
        {
            Console.Clear();
            Helper.WriteLine(Config.MenuTexts.HelpText);


            GenerateTable();
        }

        //TODO: Just Proof of work, need massive refactoring
        private void GenerateTable()
        {
            XmlDocument helpDoc = new XmlDocument();
            helpDoc.Load(Config.MenuTexts.Storage("help.xml"));

            var tableRows = new List<List<string>>();
            var headerItems = RowItems(helpDoc, "header", "Name");
            var headerLineItems = new[] { headerItems[0][0], headerItems[1][0] };

            tableRows.AddRange(RowItems(helpDoc, "listItems", headerLineItems));

            var lines = new List<string>();
            lines.Add(GenerateLine(headerLineItems));

            foreach (List<string> tableRow in tableRows)
            {
                var firstColumn = ((List<string>)typeof(Config.Commands).GetField(tableRow.First()).GetValue(null)).ToText();
                var secondColumn = tableRow[1];
                lines.Add(GenerateLine(firstColumn, secondColumn));
            }

            lines.ForEach(x => Helper.WriteLine(x));
        }

        private List<List<string>> RowItems(XmlDocument helpDoc, string xmlNodeName, params string[] attributNames)
        {
            var items = new List<List<string>>();

            var childNodes = helpDoc?.DocumentElement?.GetElementsByTagName(xmlNodeName).Item(0)?.ChildNodes;

            foreach (XmlNode child in childNodes)
            {
                var row = new List<string>();

                foreach (string attributName in attributNames)
                {
                    row.Add(child.Attributes?[attributName].Value);
                }

                items.Add(row);
            }
            return items;
        }

        public void h() => help();

        private string GenerateLine(params string[] columnns)
        {
            var line = string.Empty;
            foreach (string columnn in columnns)
            {
                line += columnn + "\t";
            }
            return line.Length < 1 ? string.Empty : line.Substring(0, line.Length - 1);
        }
    }
}