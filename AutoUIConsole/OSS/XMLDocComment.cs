using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace AutoUIConsole.OSS
{
    /// <summary>
    /// https://social.msdn.microsoft.com/Forums/vstudio/en-US/40393acc-3b36-4191-9148-947eb8272119/reading-methodparameterproperties-comments-from-cs-file-or-executables?forum=csharpgeneral
    /// </summary>
    class XMLDocComment
    {
        private Type _type;
        private MethodInfo _method;

        private string _typeName;
        private string _methodName;
        public string ClassDoc { get; set; }
        public string MethodDoc { get; set; }
        public XDocument Documentation { get; set; }

        public XMLDocComment(Type @class, string methodName)
        {
            Init(@class, methodName);

            Documentation = XDocument.Load(Path.ChangeExtension(_type.Assembly.CodeBase, "xml"));
            var test = Documentation.Root.Element("members").Elements("member").Attributes();

            ClassSummary();

            MethodSummary();
        }

        private void MethodSummary()
        {
            if (_methodName == null || _methodName.Equals(string.Empty)) return;

            XElement methodDocElement = Documentation.Root.Element("members").Elements("member")
                .FirstOrDefault(e => Regex.IsMatch(e.Attribute("name").Value + ".*", _methodName));
            if (methodDocElement != null)
                MethodDoc = methodDocElement.Element("summary").Value.Trim('\n', ' ');
        }

        private void Init(Type @class, string methodName)
        {
            _type = @class;
            _method = Helper.GetMethodsFiltered(methodName, @class)?.FirstOrDefault();

            _typeName = "T:" + _type.FullName;

            if (_method == null) return;

            _methodName = "M:" + _type.FullName + "." + _method.Name + "(" +
                     string.Join(",", _method.GetParameters().Select(p => p.ParameterType.FullName).ToArray()) + ")";
        }

        private void ClassSummary()
        {
            XElement typeDocElement = Documentation.Root.Element("members").Elements("member")
                .FirstOrDefault(e => e.Attribute("name").Value == _typeName);
            if (typeDocElement != null)
                ClassDoc = typeDocElement.Element("summary").Value.Trim('\n', ' ');
        }
    }
}
