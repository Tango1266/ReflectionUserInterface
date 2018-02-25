using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static AutoUIConsole.AppConfig;

namespace AutoUIConsole.Components.DataTypes
{
    public class PathLevel
    {
        private readonly string _fullPath;
        private readonly string _targetLevel;

        public string PreviousLevel { get; set; }
        public string NextLevel { get; set; }
        public string BaseLevel { get; set; }

        public bool IsValid { get; private set; }
        public bool IsIncomplete { get; set; }
        public bool IsTop { get; private set; }
        public bool IsLeaf { get; private set; }

        public static bool LastIsTop { get; set; }

        public PathLevel(string fullPath, string targetLevel)
        {
            _fullPath = fullPath;
            _targetLevel = targetLevel;

            InspectLevel();
        }

        public void InspectLevel()
        {
            var levels = _fullPath.Split('.').ToList();
            string pathWithBaseLevel = Regex.Replace(_fullPath, @".*?(?=\." + _targetLevel + ")", "");
            string[] levelsWithBase = pathWithBaseLevel.Split('.').Where(x => x.Length > 1).ToArray();
            string[] levelsUntilBase = levels.Except(levelsWithBase).ToArray();

            EvaluateLevel(levels, levelsWithBase, levelsUntilBase);
            LastIsTop = IsTop;
        }

        private void EvaluateLevel(List<string> levels, string[] levelsWithBase, string[] levelsUntilBase)
        {
            IsValid = levels.Count > 0;
            if (IsValid) BaseLevel = levelsWithBase[0];

            IsTop = levelsUntilBase.Length <= 0 || (IsValid && !IsLeaf && BaseLevel.Equals(DirLevel0));
            if (!IsTop) PreviousLevel = levelsUntilBase[levelsUntilBase.Length - 1];

            IsLeaf = levelsWithBase.Length <= 1;
            if (!IsLeaf) NextLevel = levelsWithBase[1];

            IsIncomplete = IsValid && (!BaseLevel.Equals(_targetLevel) && Regex.IsMatch(BaseLevel, ".*" + _targetLevel + ".*"));
        }

    }
}
