using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutoUIConsole.Components.Commands
{
    public class SaveMethodsOfCurrentOptions : Command
    {
        private Task _savingTask;
        private CancellationTokenSource _cts;

        //TODO: Filename durch UserInput bestimmen

        public void save() => Execute();

        private void SaveInNewTask(CSVFile csvFile)
        {
            if (!_savingTask?.IsCompleted ?? false)
            {
                _cts.Cancel();
                while (!_savingTask.IsCanceled || !_savingTask.IsCanceled)
                {
                }
            }

            _cts = new CancellationTokenSource();
            lock (csvFile)
            {
                _savingTask = Task.Factory.StartNew(csvFile.Save, _cts.Token);

                Helper.LogInLine(Environment.NewLine + "Es wird gespeichert");
                while (!_savingTask.IsCompleted)
                {
                    Helper.LogInLine(". ");
                    Task.Delay(1000).Wait();
                }
                Helper.LogInLine(Environment.NewLine);
            }
        }

        public override void Execute(object parameter = null)
        {
            CSVFile csvFile = new CSVFile(Session.UserInterface.CurrentSelection, "AvailableMethods.csv");
            csvFile.CreateHeaderLine("Menu Item", "Method Name", "Namespace");
            SaveInNewTask(csvFile);
        }
    }
}