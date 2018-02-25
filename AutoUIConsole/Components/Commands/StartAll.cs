using System;
using System.Reflection;
using AutoUIConsole.Utils;

namespace AutoUIConsole.Components.Commands
{
    public partial class StartAll : Command
    {
        public void s() => Execute();

        public void start() => Execute();
        public override void Execute(object parameter = null)
        {
            Helper.Log("Wechsle zu Main Menu");
            var methodInfos = Helper.GetMethods(Session.UserInterface.CurrentSelection.Options.Classes.ToArray());

            foreach (MethodInfo methodInfo in methodInfos)
            {

                try
                {
                    var classInstance = Activator.CreateInstance(methodInfo?.DeclaringType);
                    methodInfo?.Invoke(Convert.ChangeType(classInstance, classInstance.GetType()), new object[] { });
                }
                catch (Exception e)
                {
                    Helper.Log(methodInfo.DeclaringType.FullName + Environment.NewLine +
                                     methodInfo.Name + " " + Environment.NewLine +
                                     e.Message + Environment.NewLine +
                                     e.StackTrace);
                }
            }
        }
    }
}
