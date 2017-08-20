using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AutoUIConsole._Archive
{
    /// <summary>
    /// Structure of the Directory
    /// </summary>
    public class DirStructure
    {
        public static string DeepestClassPath;
        public int PrefixLength;

        public string DirLevel0;
        public string DirLevel1;
        public string DirLevel2;
        public string DirLevel3;
        public string DirLevel4;
        public string DirLevel5;
        public string DirLevel6;
        public string DirLevel7;

        public bool IsLeaf { get; private set; }

        public DirStructure(string path)
        {
            PrefixLength = Config.PrefixDirLevel.MainLevel.Length;
            //GetDeepestClassPath();
            InitializeDirStructure("", path);
        }

        public DirStructure(string level, string path)
        {
            PrefixLength = Config.PrefixDirLevel.MainLevel.Length;
            //GetDeepestClassPath();
            InitializeDirStructure(level, path);
        }

        private void InitializeDirStructure(string level, string path)
        {

            //string baseLevel = DirLevel0;
            var levels = path.Split('.');

            if (level.Length > 0)
            {
                string pathWithoutBaseLevel = Regex.Replace(path, Config.RegexPattern.AnyChars + level + Config.RegexPattern.dot, "");
                levels = pathWithoutBaseLevel.Split('.');
            }

            //var levelsPrefixes = new List<string>();
            //levels.ToList().ForEach(x => levelsPrefixes.Add(x.Substring(0, PrefixLength)));
            DirLevel0 = levels[0];

            var fields = this.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

            for (var i = 1; i < levels.Length; i++)
            {
                var valueOfPreviousDirLevel = fields.Where(x => x.Name.Equals("DirLevel" + (i - 1))).ToArray()[0].GetValue(this);

                fields.Where(x => x.Name.Equals("DirLevel" + i)).ToArray()[0].SetValue(this, valueOfPreviousDirLevel + "." + levels[i]);
            }

        }


        private void GetDeepestClassPath()
        {
            List<string> typeNamesFilteredDirLevel = new List<string>();
            var typesFilteredDireLevel0 = Config.AssemblyWhereToLookUp.GetTypes()
                .Where(x => x.FullName != null && x.FullName.Contains(DirLevel0)).ToList();

            //Alle Fullnames in einer Liste
            typesFilteredDireLevel0.ForEach(x => typeNamesFilteredDirLevel.Add(x.FullName));

            //Alle Fullnames als key in Dictionarry:
            //  - teile Fullnames nach "."
            //  - zähle die Anzahl der elemente im result-Dictionarry und setze es als Value
            //  - sortiere die anzahl der elemente absteigend
            var DirLevels = new Dictionary<string, int>();
            typeNamesFilteredDirLevel.ForEach(x => DirLevels.Add(x, x.Split('.').Length));

            //Die höchste Hirarchiestufe ist das erste key im Dictionarry
            DeepestClassPath = DirLevels.OrderByDescending(x => x.Value).First().Key;
        }
    }
}

