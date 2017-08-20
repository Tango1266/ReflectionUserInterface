using AutoUIConsole.Components.Abstracts;
using AutoUIConsole.Components.Menus;
using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using static AutoUIConsole.Config.RegexPattern;

namespace AutoUIConsole.Components
{
    public class UserInterface
    {
        public Commands Commands;
        public Menu CurrentMenu;
        public SelectionOption CurrentSelection;
        public SelectionOption CurrentMethodSelection;

        public DirStructure CurrentDirStructure { get; set; }

        public UserInterface(string selection)
        {
            Commands = new Commands();
            CurrentSelection = new SelectionOption(null, selection);
            CurrentMenu = new MainMenu(CurrentSelection);
        }

        public void ExecuteSelection(string selection)
        {
            if (Commands.AvailableCommands.Contains(selection))
            {
                Helper.InvokeCommand(typeof(Commands), selection);
            }

            else if (SelectionIsNumber(selection))
            {
                HandleDigitSelection(selection);
            }
            else
            {
                HandleSelection(selection);
            }
        }

        private static bool SelectionIsNumber(string selection)
        {
            return Regex.IsMatch(selection, ConsistOnlyOfDigits);
        }

        public void HandleSelection(string selection)
        {

            CurrentMenu.Directory = new DirStructure(selection, Helper.GetTypesFromFullNameRegex(@".*" + selection + @"(\.|$)")[0].FullName);
            CurrentSelection = new SelectionOption(CurrentSelection, selection);

            if (CurrentSelection.CurrentOptions.Count == 1)
            {
                //TODO: Methoden sollen aufgelistet werden bzw ausgefuehrt, wenn eine Methode eingegeben wird
                //hier ist es wohl angebracht, dass der Klassenname vorher angegeben wird
            }

            CurrentMenu = new SubMenu(CurrentMenu, CurrentSelection);
        }

        public void HandleDigitSelection(string selection)
        {
            if (int.TryParse(selection, out int key))
            {
                key = key - 1;
                if (key >= CurrentMenu.MenuItems.Count)
                    throw new ArgumentException("Der Wert stellt keine Option dar.");

                var currentItem = CurrentMenu.MenuItems.ElementAt(key);
                CurrentSelection = new SelectionOption(CurrentSelection, currentItem);

                if (CurrentSelection.previousSelectionOptions != null && CurrentSelection.previousSelectionOptions.NoOptionsLeft)
                {
                    InvokeMethod(key);
                }
                else if (CurrentMenu.Directory.IsLeaf)
                {
                    CurrentMethodSelection = CurrentSelection.SelectMethods();
                    CurrentMenu = new LeafMenu(CurrentMenu, CurrentMethodSelection);
                }
                else
                {
                    CurrentMenu = new SubMenu(CurrentMenu, CurrentSelection);
                }
            }

        }

        private void InvokeMethod(int key)
        {
            string className = CurrentMethodSelection.Selection;
            var classTypes = Helper.GetTypesFromFullNameRegex("\\." + className + "$");

            if (classTypes.Count <= 0)
                throw new AggregateException("Fehler - Die Klasse zur Methode wurde nicht gefunden.");

            Type classType = Helper.GetTypesFromFullNameRegex("\\." + className + "$")[0];

            var classInstance = Activator.CreateInstance(classType);

            classType.GetMethod(CurrentMethodSelection.CurrentMethodOptions[key].Name,
                    BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
                .Invoke(Convert.ChangeType(classInstance, classInstance.GetType()), new object[] { });
        }
    }
}
