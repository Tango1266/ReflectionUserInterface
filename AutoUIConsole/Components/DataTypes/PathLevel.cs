using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutoUIConsole.Components.DataTypes
{
    public class PathLevel
    {
        private readonly string _fullPath;
        private readonly string _targetLevel;

        public string previousLevel { get; set; }
        public string nextLevel { get; set; }
        public string baseLevel { get; set; }

        public bool IsValid { get; private set; }
        public bool IsIncomplete { get; set; }
        public bool IsTop { get; private set; }
        public bool IsLeaf { get; private set; }
        public bool IsLeafOrIncomplete => IsLeaf || IsIncomplete;
        public bool IsValidOrTop => IsValid || IsTop;

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
            if (IsValid) baseLevel = levelsWithBase[0];

            IsTop = levelsUntilBase.Length <= 0 || (IsValid && !IsLeaf && baseLevel.Equals(Config.DirLevel0));
            if (!IsTop) previousLevel = levelsUntilBase[levelsUntilBase.Length - 1];

            IsLeaf = levelsWithBase.Length <= 1;
            if (!IsLeaf) nextLevel = levelsWithBase[1];

            IsIncomplete = IsValid && (!baseLevel.Equals(_targetLevel) && Regex.IsMatch(baseLevel, ".*" + _targetLevel + ".*"));
        }

        #region IDisposable Support
        private bool disposedValue = false; // Dient zur Erkennung redundanter Aufrufe.

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    previousLevel = null;
                    // TODO: verwalteten Zustand (verwaltete Objekte) entsorgen.
                }

                // TODO: nicht verwaltete Ressourcen (nicht verwaltete Objekte) freigeben und Finalizer weiter unten überschreiben.
                // TODO: große Felder auf Null setzen.

                disposedValue = true;
            }
        }

        // TODO: Finalizer nur überschreiben, wenn Dispose(bool disposing) weiter oben Code für die Freigabe nicht verwalteter Ressourcen enthält.
        // ~PathLevel() {
        //   // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(bool disposing) weiter oben ein.
        //   Dispose(false);
        // }

        // Dieser Code wird hinzugefügt, um das Dispose-Muster richtig zu implementieren.
        public void Dispose()
        {
            // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(bool disposing) weiter oben ein.
            Dispose(true);
            // TODO: Auskommentierung der folgenden Zeile aufheben, wenn der Finalizer weiter oben überschrieben wird.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
