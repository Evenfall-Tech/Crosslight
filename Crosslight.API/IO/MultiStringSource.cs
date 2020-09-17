using System.Collections.Generic;

namespace Crosslight.API.IO
{
    public class MultiStringSource : Source
    {
        private List<string> strings;

        public IEnumerable<string> Strings { get => strings; }
        public override int Count => strings == null ? 0 : strings.Count;

        public MultiStringSource()
        {
            strings = new List<string>();
        }
        public MultiStringSource WithString(string str)
        {
            strings.Add(str);
            return this;
        }
        public MultiStringSource WithStrings(IEnumerable<string> strings)
        {
            this.strings.AddRange(strings);
            return this;
        }
    }
}
