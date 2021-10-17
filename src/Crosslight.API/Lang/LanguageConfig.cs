using System;
using System.Collections.Generic;

namespace Crosslight.API.Lang
{
    public class LanguageConfig
    {
        protected Dictionary<string, object> options;

        public LanguageConfig()
        {
            options = new Dictionary<string, object>();
        }

        public void Add(string option, object value = null) => options.Add(option, value);
        public object Get(string option) => options[option];
        public T Get<T>(string option)
        {
            try
            {
                return (T)options[option];
            }
            catch (Exception)
            {
                // TODO: log exception.
                return default;
            }
        }
        public bool Contains(string option)
        {
            return options.ContainsKey(option);
        }
    }
}
