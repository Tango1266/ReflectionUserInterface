using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutoUIConsole.Components.DataTypes
{
    public class PathLevel
    {
        private readonly string _fullPath;
        private readonly string _targetLevel;

        public string PreviousLevel { get; set; }
        public string NextLevel { get; set; }
        public string TargetLevel { get; set; }

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
            //var targetLevel = Regex.Replace(_fullPath, @".*(?=." + _targetLevel + ")", "");
            var targetLevel = Regex.Replace(_fullPath, @"(?<=.*" + _targetLevel + @"\d*\w*)\..*", "");

            //string pathWithBaseLevel = Regex.Replace(_fullPath, @".*?(?=." + _targetLevel + ")", "");
            string pathWithBaseLevel = targetLevel;

            string[] levelsWithTarget = pathWithBaseLevel.Split('.').Where(x => x.Length > 1).ToArray();
            string[] remainingLevels = levels.Except(levelsWithTarget).ToArray();

            EvaluateLevel(levels, levelsWithTarget, remainingLevels);
            LastIsTop = IsTop;
        }

        private void EvaluateLevel(List<string> levels, string[] levelsWithTarget, string[] RemainingLevels)
        {
            IsValid = levels.Count > 0;
            if (IsValid) TargetLevel = levelsWithTarget.Last();

            IsTop = levelsWithTarget.Length <= 1 || (IsValid && !IsLeaf && TargetLevel.Equals(AppConfig.DirLevel0));
            if (!IsTop) PreviousLevel = levelsWithTarget[levelsWithTarget.Length - 2];

            IsLeaf = RemainingLevels.Length <= 0;
            if (!IsLeaf) NextLevel = RemainingLevels[0];

            IsIncomplete = IsValid && !(TargetLevel.Equals(_targetLevel) && Regex.IsMatch(TargetLevel, ".*" + _targetLevel + ".*"));
        }

    }
}
