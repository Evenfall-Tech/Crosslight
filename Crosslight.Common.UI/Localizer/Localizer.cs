using Avalonia;
using Avalonia.Platform;
using Newtonsoft.Json;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Crosslight.Common.UI.Localizer
{
    public class Localizer : ReactiveObject
    {
        private const string IndexerName = "Item";
        private const string IndexerArrayName = "Item[]";
        private Dictionary<string, string> m_Strings = null;

        public Localizer()
        {

        }

        private Dictionary<string, string> StringsFromJson(Dictionary<string, object> value, string start = "")
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            foreach (var kvp in value)
            {
                if (kvp.Value is string s) res.Add(start + kvp.Key, s);
                else if (kvp.Value is Dictionary<string, object> val)
                {
                    var inner = StringsFromJson(val, kvp.Key + "/");
                    foreach (var kvpInner in inner)
                    {
                        res.Add(kvpInner.Key, kvpInner.Value);
                    }
                }
                else res.Add(start + kvp.Key, kvp.Value.ToString());
            }
            return res;
        }

        public bool LoadLanguage(string language)
        {
            language = language.Trim();
            Language = language;
            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();

            Uri uri = new Uri($"avares://Crosslight.Common.UI/Assets/i18n/{language}.json");
            if (assets.Exists(uri))
            {
                using (StreamReader sr = new StreamReader(assets.Open(uri), Encoding.UTF8))
                {
                    m_Strings = StringsFromJson(JsonConvert.DeserializeObject<Dictionary<string, object>>(sr.ReadToEnd()));
                }
                Invalidate();

                return true;
            }
            return false;
        } // LoadLanguage

        public string Language { get; private set; }

        public string this[string key]
        {
            get
            {
                if (m_Strings != null && m_Strings.TryGetValue(key, out string res))
                    return res.Replace("\n", "\n");

                return $"{Language}:{key}";
            }
        }

        public static Localizer Instance { get; set; } = new Localizer();

        public void Invalidate()
        {
            this.RaisePropertyChanged(nameof(IndexerName));
            this.RaisePropertyChanged(nameof(IndexerArrayName));
        }
    }
}
