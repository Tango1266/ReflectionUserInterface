using System;
using System.Reflection;

namespace AutoUIConsole.Components
{
    public partial class Commands
    {
        public void StartAll()
        {
            Console.WriteLine("Wechsle zu Main Menu");
            var options = Program.UserInterface.CurrentSelection.CurrentOptions;

            foreach (Type option in options)
            {
                foreach (MethodInfo methodInfo in option.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                {

                    try
                    {
                        var classInstance = Activator.CreateInstance(option);
                        methodInfo.Invoke(Convert.ChangeType(classInstance, classInstance.GetType()), new object[] { });
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(option.FullName +
                                        "\n" + methodInfo.Name + " " +
                                          "\n" + e.Message);
                    }
                }
            }

        }

        public void s() => StartAll();

        public void start() => StartAll();
    }
}
