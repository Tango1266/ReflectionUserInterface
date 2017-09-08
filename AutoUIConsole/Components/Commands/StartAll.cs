using System;
using System.Reflection;

namespace AutoUIConsole.Components
{
    public partial class Commands
    {
        public void StartAll()
        {
            Console.WriteLine("Wechsle zu Main Menu");
            var methodInfos = Helper.GetMethods(Session.UserInterface.CurrentOptions.Classes.ToArray());

            foreach (MethodInfo methodInfo in methodInfos)
            {

                try
                {
                    var classInstance = Activator.CreateInstance(methodInfo?.DeclaringType);
                    methodInfo?.Invoke(Convert.ChangeType(classInstance, classInstance.GetType()), new object[] { });
                }
                catch (Exception e)
                {
                    Console.WriteLine(methodInfo.DeclaringType.FullName +
                                    "\n" + methodInfo.Name + " " +
                                      "\n" + e.Message +
                                      "\n" + e.StackTrace);
                }
            }
        }

        public void s() => StartAll();

        public void start() => StartAll();
    }
}
