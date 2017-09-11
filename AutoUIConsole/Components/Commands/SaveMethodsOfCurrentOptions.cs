using System;
using System.Threading;
using System.Threading.Tasks;
using static AutoUIConsole.Helper;

namespace AutoUIConsole.Components
{
    public partial class Commands
    {
        private Task _savingTask;
        private CancellationTokenSource _cts;

        //TODO: Filename durch UserInput bestimmen
        public void SaveMethodsOfCurrentOptions()
        {
            CSVFile csvFile = new CSVFile(Session.UserInterface.currentSelection, "AvailableMethods.csv");
            csvFile.CreateHeaderLine("Menu Item", "Method Name", "Namespace");
            SaveInNewTask(csvFile);
        }

        public void save() => SaveMethodsOfCurrentOptions();

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
            _savingTask = Task.Factory.StartNew(csvFile.Save, _cts.Token);

            Write(Environment.NewLine + "Es wird gespeichert");
            while (!_savingTask.IsCompleted)
            {
                Write(". ");
                Task.Delay(1000).Wait();
            }
            Write(Environment.NewLine);
        }
    }
}