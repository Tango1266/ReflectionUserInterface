using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Input;

#pragma warning disable 1591

namespace AutoUIConsole.Components.Commands
{
    public abstract class SuperCommand : ICommand
    {
        public static List<ICommand> ExecutedCommands { get; set; } = new List<ICommand>();
        public static Dictionary<ICommand, List<MethodInfo>> CommandTypeMethods { get; set; } = new Dictionary<ICommand, List<MethodInfo>>();
        public static List<string> AvailableCommands { get; set; } = new List<string>();

        public event EventHandler CanExecuteChanged;


        public SuperCommand()
        {
            if (!ExecutedCommands.Contains(this)) ExecutedCommands.Add(this);

        }

        public static void Init()
        {
            var commandTypes = Assembly.GetExecutingAssembly().GetTypes().Where(x =>
                Regex.IsMatch(x.Namespace, ".*Commands") && typeof(SuperCommand).IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface);

            foreach (Type commandType in commandTypes)
            {
                ICommand instance = (ICommand)Activator.CreateInstance(commandType);
                CommandTypeMethods.Add(instance, Helper.GetMethods(commandType));
                AvailableCommands.Add(commandType.Name);

                foreach (var methodInfo in CommandTypeMethods[instance])
                {
                    if (methodInfo.Name.Equals("Execute")) continue;

                    AvailableCommands.Add(methodInfo.Name);
                }
            }
        }

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public abstract void Execute(object parameter = null);
    }
}
