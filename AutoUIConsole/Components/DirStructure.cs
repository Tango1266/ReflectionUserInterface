using System.Collections.Generic;
using System.Text.RegularExpressions;
using static AutoUIConsole.Config.RegexPattern;
namespace AutoUIConsole.Components
{
    /// <summary>
    /// Structure of the Directory
    /// </summary>
    public class DirStructure
    {
        public int PrefixLength;

        public List<string> DirLevels;

        public bool IsLeaf => DirLevels?.Count == 1 || DirLevels?.Count == 0;

        public DirStructure(string level, string path)
        {
            PrefixLength = Config.PrefixDirLevel.MainLevel.Length;
            InitializeDirStructure(level, path);
        }

        private void InitializeDirStructure(string level, string path)
        {
            DirLevels = new List<string>();

            var levels = path.Split('.');

            if (level.Length > 0)
            {
                //string pathWithoutBaseLevel = Regex.Replace(path, AnyChars + level + dot, "");

                string pathWithoutBaseLevel = Regex.Replace(path, AnyChars + level + @"(\.|$)", "");
                levels = pathWithoutBaseLevel.Split('.');
            }

            DirLevels.Add(levels[0]);

            for (var i = 1; i < levels.Length; i++)
            {
                DirLevels.Add(levels[i - 1] + "." + levels[i]);
            }

        }

    }
}

