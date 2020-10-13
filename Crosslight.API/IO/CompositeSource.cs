using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.API.IO
{
    public class CompositeSource : Source
    {
        private List<Source> sources;

        public IEnumerable<Source> Sources { get => sources; }
        public override int Count => sources == null ? 0 : sources.Count;

        public CompositeSource()
        {
            sources = new List<Source>();
        }
        public CompositeSource WithSource(Source src)
        {
            sources.Add(src);
            return this;
        }
        public CompositeSource WithSources(IEnumerable<Source> srcs)
        {
            sources.AddRange(srcs);
            return this;
        }
    }
}
