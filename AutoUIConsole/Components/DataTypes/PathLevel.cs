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

        public bool IsTop { get; private set; }
        public bool IsLeaf { get; private set; }
        public bool IsValid { get; private set; }
        public bool IsIncomplete { get; set; }

        public bool IsLeafOrIncomplete => IsLeaf || IsIncomplete;
        public bool IsValidOrTop => IsValid || IsTop;

        public PathLevel(string fullPath, string targetLevel)
        {
            _fullPath = fullPath;
            _targetLevel = targetLevel;

            InspectLevel();
        }

        public void InspectLevel()
        {
            Levels = _fullPath.Split('.').ToList();
            string pathWithBaseLevel = Regex.Replace(_fullPath, AnyChars + @"?(?=\." + _targetLevel + ")", "");
            string[] levelsWithBase = pathWithBaseLevel.Split('.').Where(x => x.Length > 1).ToArray();
            string[] levelsUntilBase = Levels.Except(levelsWithBase).ToArray();

            IsValid = Levels.Count > 0;
            if (IsValid) baseLevel = levelsWithBase[0];

            IsTop = levelsUntilBase.Length <= 0 || (IsValid && !IsLeaf && baseLevel.Equals(Config.DirLevel0));
            if (!IsTop) previousLevel = levelsUntilBase[levelsUntilBase.Length - 1];

            IsLeaf = levelsWithBase.Length <= 1;
            if (!IsLeaf) nextLevel = levelsWithBase[1];

            IsIncomplete = IsValid && (!baseLevel.Equals(_targetLevel) && Regex.IsMatch(baseLevel, ".*" + _targetLevel + ".*"));
        }

    }
}
