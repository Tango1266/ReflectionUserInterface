using AutoUIConsole.Components.DataTypes;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoUIConsole.Components
{
    public partial class Commands
    {
        private Task _savingTask;
        private CancellationTokenSource _cts;

        public void SaveMethodsOfCurrentOptions()
        {
            CSVFile csvFile = new CSVFile(Session.UserInterface.currentSelection);

            var filePath = Path.Combine(Environment.CurrentDirectory, "AvailableMethods.csv");

            if (!_savingTask?.IsCompleted ?? false)
            {
                _cts.Cancel();

                while (!_savingTask.IsCanceled || !_savingTask.IsCanceled) { }
            }
            _cts = new CancellationTokenSource();
            _savingTask = Task.Factory.StartNew(() => csvFile.Save(filePath), _cts.Token);

            Console.WriteLine(Environment.NewLine + $"Die Datei {filePath} wird erstellt.");
            Console.Write(Environment.NewLine + "Es wird gespeichert");

            while (!_savingTask.IsCompleted)
            {
                Console.Write(". ");
                Task.Delay(250).Wait();
            }

            Console.WriteLine("Datei wurde erfolgreich gespeichert.");
        }

        public void save() => SaveMethodsOfCurrentOptions();
    }

    public class CSVFile
    {
        private readonly Assembly assembly;
        private readonly string dirLevel0;
        private Selection selection;
        private StringBuilder csvFile;
        public char Seperator { get; set; } = ';';

        public CSVFile(Assembly assembly, string dirLevel0)
        {
            this.assembly = assembly;
            this.dirLevel0 = dirLevel0;
            this.selection = new Selection(selection, dirLevel0);
        }

        public CSVFile(Selection currentSelection)
        {
            selection = currentSelection;
            csvFile = new StringBuilder();
        }

        public void Save(string fileName)
        {
            AddLine("MenuItem", "Method", "Full Path");

            foreach (var methodInfo in selection.Options.Methods)
            {
                var fullName = methodInfo.DeclaringType?.FullName + methodInfo.Name;

                //TODO: MenuItems als SelectionContent
                PathLevel pathLevel = new PathLevel(fullName, selection.Content);

                AddLine(pathLevel.nextLevel, methodInfo.Name, methodInfo.DeclaringType?.FullName);
            }

            File.WriteAllText(fileName, csvFile.ToString());
        }

        private void AddLine(params string[] columnns)
        {
            var row = string.Empty;
            foreach (string columnn in columnns)
            {
                row += columnn + Seperator;
            }

            csvFile.AppendLine(row.Substring(0, row.Length - 1));
        }
    }
}