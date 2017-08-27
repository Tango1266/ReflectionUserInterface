using System;
using System.Collections.Generic;
using System.Reflection;

namespace AutoUIConsole.Components
{
    public class SelectionOption
    {
        public SelectionOption previousOptions { get; set; }
        public List<Type> Classes { get; set; }
        public List<MethodInfo> Methods { get; set; }


        public string Selection { get; set; }

        public bool NoOptionsLeft => Methods == null || Classes.Count == 0;

        public SelectionOption(SelectionOption options, string selection)
        {
            previousOptions = options;
            Selection = previousOptions?.Selection + ".*" + selection;

            Classes = new List<Type>();
            Classes = Helper.GetTypesFromFullName(this);

            Methods = new List<MethodInfo>();

            if (Classes.Count > 0)
            {
                foreach (Type classOption in Classes)
                {
                    Methods.AddRange(Helper.GetMethods(classOption));
                }
            }
            else
            {
                //Methods = Helper.GetMethods(Selection);
            }
        }

        public SelectionOption Undo()
        {
            Classes = previousOptions?.Classes;
            Selection = previousOptions?.Selection;
            Methods = previousOptions?.Methods;
            previousOptions = previousOptions?.previousOptions;
            return this;
        }

    }
}
