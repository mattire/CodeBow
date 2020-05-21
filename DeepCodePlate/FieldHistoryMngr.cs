using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingHood
{
    class FieldHistoryMngr
    {
        public static string HistoryFolderName = ".\\History";

        public string CurrentScriptName { get { return CodeBow.Current.CurrentScript; } }

        public const int HistoryLimitCount = 10;

        public List<string> FieldNames {
            get { return CodeBow.Current.Fields.Select(f=>f.Name).ToList(); }
        }

        public Dictionary<string, List<string>> SuggestionMap { get; set; }

        private static FieldHistoryMngr instance;
        public static FieldHistoryMngr Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FieldHistoryMngr();
                }
                return instance;
            }
        }

        public void LoadHistory() {
            if(!string.IsNullOrWhiteSpace(CurrentScriptName))
            {
                //PreviousValues = new Dictionary<string, List<string>>();
                LoadPreviousEntries();
            }
        }

        private string GetOriginalFieldName(CodeBow.Field fieldPlace)
        {
            var bow = CodeBow.Current;
            var ind = bow.Fields.IndexOf(fieldPlace);
            if (ind != -1) {
                return bow.OriginalFields[ind].Name;
            }
            return "";
        }

        public void StoreValues()
        {
            foreach (var fld in CodeBow.Current.Fields)
            {
                var fn = GetOriginalFieldName(fld);
                if (SuggestionMap.ContainsKey(fn)) {
                    var lst = SuggestionMap[fn];
                    lst.Insert(0, fld.Name);
                    if (lst.Count > HistoryLimitCount) {
                        lst.RemoveAt(lst.Count - 1);
                    }
                }
            }
            SaveSuggestionMap();
        }

        private void SaveSuggestionMap()
        {
            foreach (var kvp in SuggestionMap)
            {
                var content = string.Join("\n", kvp.Value);
                //var path = Path.Combine(HistoryFolderName, kvp.Key);
                var path = Path.Combine(mFolderPath, kvp.Key);
                
                File.WriteAllText(path, content);
            }
        }

        object loadLock = new object();
        private void LoadPreviousEntries()
        {
            lock (loadLock)
            {
                EnsurePaths();
                var fls = Directory.GetFileSystemEntries(mFolderPath);
                SuggestionMap = fls.ToDictionary(
                    (fpath) => Path.GetFileName(fpath),
                    (fpath) => File.ReadAllLines(fpath).ToList()
                );
            }
        }

        string mFolderPath;
        public void EnsurePaths()
        { 
            mFolderPath = Path.Combine(HistoryFolderName, CurrentScriptName);
            if (!Directory.Exists(mFolderPath)) {
                Directory.CreateDirectory(mFolderPath);
            }

            foreach (var fn in FieldNames)
            {
                var filePath = Path.Combine(mFolderPath, fn);
                if (!File.Exists(filePath)) {
                    using (var file = File.Create(filePath)) { }
                }
            }
        }

    }
}
