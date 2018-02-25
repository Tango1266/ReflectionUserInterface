using AutoUIConsole.Components.Commands;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AutoUIConsole.Components.DataTypes
{
    public class UserInput:IEnumerable<UserInput>
    {
        public LinkedList<UserInput> Arguments { get; set; }

        public UserInput RootArgument { get; set; }

        public string Content { get; set; }

        public bool IsEmpty { get; private set; }

        public bool IsMultiInput { get; private set; }

        public bool IsCommand { get; private set; }

        public bool IsNumber { get; private set; }

        public string OriginQuerry { get; set; }

        public UserInput(params string[] input)
        {
            input = SplitInput(input);
            Content = input.Length > 0 ? input[0] : string.Empty;
            OriginQuerry = input.ToText();
            InitUserInput(this, input);
        }


        public UserInput(UserInput rootArgument, params string[] input)
        {
            input = SplitInput(input);
            Content = input.Length > 0 ? input[0] : string.Empty;

            InitUserInput(rootArgument, input);
        }

        private void InitUserInput(UserInput rootArgument, string[] input)
        {
            RootArgument = rootArgument;

            Arguments = ExtractArguments(rootArgument, input);

            EvaluateInput();
        }

        private string[] SplitInput(string[] input)
        {
            if (input.Length == 1) return input[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            return input;
        }

        private LinkedList<UserInput> ExtractArguments(UserInput parent, string[] userInput)
        {
            if (userInput.Length <= 1)
            {
                return new LinkedList<UserInput>();
            }

            string[] subArray = new string[userInput.Length - 1];
            Array.Copy(userInput, 1, subArray, 0, subArray.Length);

            var userIn = new UserInput(parent,subArray);
            Arguments = ExtractArguments(parent, subArray);

            Arguments.AddFirst(userIn);
            return Arguments;
        }

        private void EvaluateInput()
        {
            IsEmpty = Content.Equals(string.Empty);
            IsMultiInput = Arguments.Count > 0;

            // Only RootArgument is allowed to be a command
            if (this != RootArgument) return;

            IsCommand = Command.IsCommand(Content);

            if (!IsCommand) IsNumber = Content.IsNumber();
        }

        #region Ovverrides
        public override string ToString()
        {
            return $"{RootArgument.OriginQuerry}: {Content}";
        }

        public override bool Equals(object obj)
        {
            return ToString().Equals(ToString());
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public IEnumerator<UserInput> GetEnumerator()
        {
            Arguments.AddFirst(RootArgument);
            return Arguments.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
