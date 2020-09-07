using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.API.Lang
{
    public class LanguageOptions
    {
        protected Dictionary<string, object> options;

        public LanguageOptions()
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
            catch (Exception e)
            {
                // TODO: log exception.
                return default;
            }
        }
    }
}
