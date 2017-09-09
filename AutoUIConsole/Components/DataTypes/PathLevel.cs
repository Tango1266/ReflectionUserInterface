using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static AutoUIConsole.Config.RegexPattern;

namespace AutoUIConsole.Components.DataTypes
{
    public class PathLevel
    {
        private readonly string _fullPath;
        private readonly string _targetLevel;

        public List<string> Levels { get; private set; } = new List<string>();

        //public PathLevel previousLevel { get; set; }
        public string previousLevel { get; set; }

        public string nextLevel { get; set; }

        public string baseLevel { get; set; }


        public bool IsTopLevel { get; private set; }
        public bool IsLeafLevel { get; private set; }

        public PathLevel(string fullPath, string targetLevel)
        {
            _fullPath = fullPath;
            _targetLevel = targetLevel;

            InspectLevels();
        }

        public void InspectLevels()
        {
            Levels = _fullPath.Split('.').ToList();

            string pathWithBaseLevel = Regex.Replace(_fullPath, AnyChars + @"?(?=\." + _targetLevel + ")", "");
            string[] levelsWithBase = pathWithBaseLevel.Split('.').Where(x => x.Length > 1).ToArray();

            string[] levelsUntilBase = Levels.Except(levelsWithBase).ToArray();

            if (Levels.Count <= 0 || levelsWithBase.Length <= 0) return;

            previousLevel = levelsUntilBase[levelsUntilBase.Length - 1];
            baseLevel = levelsWithBase[0];
            nextLevel = levelsWithBase[1];
        }
    }
}
