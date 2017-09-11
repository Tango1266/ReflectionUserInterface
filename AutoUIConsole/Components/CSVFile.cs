using AutoUIConsole.Components.Abstracts;
using System;
using System.IO;
using System.Text;

namespace AutoUIConsole.Components
{
    public class CSVFile
    {
        public char Seperator { get; set; } = ';';

        public string Directory => Environment.CurrentDirectory;
        public string Name { get; private set; }
        public string FullPathName => Path.Combine(Directory, Name);
        public string HeaderLine { get; private set; }

        private Selection _selection;
        private readonly StringBuilder _csvFile;

        public CSVFile(Selection currentSelection, string fileName)
        {
            _selection = currentSelection.Clone();
            _csvFile = new StringBuilder();
            Name = fileName;
        }

        public void Save()
        {
            Helper.WriteLine(Environment.NewLine + $"Die Datei {Name} wird erstellt in {Directory}");

            generateCsv();

            try
            {
                File.WriteAllText(FullPathName, _csvFile.ToString());
            }
            catch (IOException)
            {
                Helper.WriteLine(Environment.NewLine + "(Fehler) Die zu speichernde Datei ist möglicherweise geöffnet. Bitte schliesse die Datei und versuche es erneut.");
                return;
            }

            Helper.WriteLine("Datei wurde erfolgreich gespeichert.");
        }

        public void CreateHeaderLine(params string[] titles)
        {
            HeaderLine = GenerateLine(titles);
        }

        private void generateCsv()
        {
            AddLine(HeaderLine);

            var menuItems = Menu.CreateMenuItems(_selection.Options, _selection.Content);
            foreach (string menuItem in menuItems)
            {
                _selection = new Selection(_selection, menuItem);

                foreach (var methodInfo in _selection.Options.Methods)
                {
                    AddLine(menuItem, methodInfo.Name, methodInfo.DeclaringType?.FullName);
                }

                _selection = _selection.previousSelection;
            }
        }

        private void AddLine(params string[] columnns)
        {
            var row = GenerateLine(columnns);
            if (row != string.Empty) _csvFile.AppendLine(row);
        }

        private string GenerateLine(string[] columnns)
        {
            var line = string.Empty;
            foreach (string columnn in columnns)
            {
                line += columnn + Seperator;
            }
            return line.Length < 1 ? string.Empty : line.Substring(0, line.Length - 1);
        }
    }
}