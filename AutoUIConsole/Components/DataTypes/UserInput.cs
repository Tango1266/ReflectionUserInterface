using AutoUIConsole.Components.Commands;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AutoUIConsole.Components.DataTypes
{
    public class UserInput
    {
        public List<UserInput> Arguments { get; set; }

        public string Content { get; }

        public bool IsEmpty { get; private set; }
        public bool IsMultiArgument { get; private set; }
        public bool IsCommand { get; private set; }
        public bool IsNumber { get; private set; }


        public UserInput(params string[] arguements)
        {
            ExtractArguments(arguements);

            Content = arguements.ToText();

            EvaluateArguments();
        }

        private void EvaluateArguments()
        {
            IsEmpty = Content.Equals(string.Empty) || Arguments.Count == 0;
            IsMultiArgument = Arguments.Count > 1;

            if (IsMultiArgument) return;

            IsCommand = CheckIsCommand(Content);

            if (!IsCommand) IsNumber = Regex.IsMatch(Content, @"\b\d+$");
        }

        private void ExtractArguments(string[] userInput)
        {
            Arguments = new List<UserInput>();

            if (userInput.Length == 1)
            {
                var arguements = userInput[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (arguements.Length == 1)
                {
                    Arguments.Add(this);
                    return;
                }


                foreach (string arguement in arguements)
                {
                    Arguments.Add(new UserInput(arguement));
                }
                return;
            }

            foreach (string argument in userInput)
            {
                Arguments.Add(new UserInput(argument));
            }
        }


        private bool CheckIsCommand(string content)
        {
            return SuperCommand.AvailableCommands.Contains(content);
        }
    }
}
