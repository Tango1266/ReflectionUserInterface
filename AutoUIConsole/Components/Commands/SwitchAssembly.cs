using AutoUIConsole.Components.DataTypes;
using System;
using System.IO;
using System.Reflection;
using AutoUIConsole.Utils;

namespace AutoUIConsole.Components.Commands
{
    public class SwitchAssembly : Command
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
                Helper.Log("Die Assembly wurde nicht unter dem Pfad gefunden. Pfad:" + location);
                return;
            }

            AutoUIConsole.AppConfig.AssemblyWhereToLookUp = Assembly.LoadFrom(location);
            AutoUIConsole.AppConfig.DirLevel0 = AutoUIConsole.AppConfig.AssemblyWhereToLookUp.GetName().Name;

            new GoToMainMenu().Execute();
            Helper.Log(Environment.NewLine + $"Die Assembly {AutoUIConsole.AppConfig.DirLevel0} wurde eingebund");
        }
    }
}
