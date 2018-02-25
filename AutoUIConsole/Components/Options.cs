using System;
using System.Collections.Generic;
using System.Reflection;

namespace AutoUIConsole.Components
{
    public class Options
    {
        public Selection Selection { get; set; }

        public List<Type> Classes { get; set; }
        public List<MethodInfo> Methods { get; set; }

        public bool IsEmpty { get; set; }

        public Options(Selection selection)
        {
            Selection = selection;

            GenerateOptions(selection);
        }

        private void GenerateOptions(Selection selection)
        {
            Classes = GetClasses(selection);
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
                Methods = Helper.GetMethodsFiltered(selection.Query, selection.PreviousSelection?.Options?.Classes.ToArray());
            }

            IsEmpty = Classes.Count == 0 && Methods.Count == 0;
        }

        private List<Type> GetClasses(Selection selection)
        {
            Classes = new List<Type>();
            return Helper.GetTypesFromFullName(selection);
        }
    }
}
