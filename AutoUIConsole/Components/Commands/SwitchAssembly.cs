using AutoUIConsole.Components.DataTypes;
using System;
using System.IO;
using System.Reflection;

namespace AutoUIConsole.Components.Commands
{
    public class SwitchAssembly : SuperCommand
    {
        public void b() => Execute();

        public void back() => Execute();

        public override void Execute(object parameter = null)
        {
            var userArguments = (UserInput[])parameter;
            if (userArguments is null || userArguments.Length < 1) return;
            string location = userArguments[0].Content;

            var fileExists = File.Exists(location);

            if (!fileExists)
            {
                Helper.WriteLine("Die Assembly wurde nicht unter dem Pfad gefunden. Pfad:" + location);
                return;
            }

            Config.AssemblyWhereToLookUp = Assembly.LoadFrom(location);
            Config.DirLevel0 = Config.AssemblyWhereToLookUp.GetName().Name;

            new GoToMainMenu().Execute();
            Helper.WriteLine(Environment.NewLine + $"Die Assembly {Config.DirLevel0} wurde eingebund");
        }
    }
}
