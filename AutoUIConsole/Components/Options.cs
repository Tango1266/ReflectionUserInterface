using System;
using System.Collections.Generic;
using System.Reflection;

namespace AutoUIConsole.Components
{
    public class Options
    {
        public Options previousOptions { get; set; }
        public List<Type> Classes { get; set; }
        public List<MethodInfo> Methods { get; set; }


        public string Selection { get; set; }

        public Options(Options options, string selection)
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
                Methods = Helper.GetMethodsFiltered(Selection, previousOptions?.Classes.ToArray());
            }
        }
    }
}
