using System;
using System.Reflection;

namespace AutoUIConsole.Components
{
    public partial class Commands
    {
        public void StartAll()
        {
            Console.WriteLine("Wechsle zu Main Menu");
            var methodInfos = Helper.GetMethods(Session.UserInterface.currentSelection.Options.Classes.ToArray());

            foreach (MethodInfo methodInfo in methodInfos)
            {

                try
                {
                    var classInstance = Activator.CreateInstance(methodInfo?.DeclaringType);
                    methodInfo?.Invoke(Convert.ChangeType(classInstance, classInstance.GetType()), new object[] { });
                }
                catch (Exception e)
                {
                    Console.WriteLine(methodInfo.DeclaringType.FullName + Environment.NewLine +
                                         methodInfo.Name + " " + Environment.NewLine +
                                         e.Message + Environment.NewLine +
                                         e.StackTrace);
                }
            }
        }

        public void s() => StartAll();

        public void start() => StartAll();
    }
}
