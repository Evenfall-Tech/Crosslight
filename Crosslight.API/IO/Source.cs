using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Crosslight.API.IO
{
    public abstract class Source
    {
        public abstract int Count { get; }

        public static Source FromDirectory(string path)
        {
            var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            return FromFiles(files);
        }

        public static Source FromFile(string filename)
        {
            return new MultiFileSource().WithFilePath(filename);
        }

        public static Source FromFiles(params string[] filenames)
        {
            return FromFiles((IEnumerable<string>)filenames);
        }

        public static Source FromFiles(IEnumerable<string> filenames)
        {
            return new MultiFileSource().WithFilePaths(filenames);
        }

        public static Source FromStream(StreamReader stream)
        {
            return FromString(stream.ReadToEnd());
        }

        public static Source FromString(string source)
        {
            return new MultiStringSource().WithString(source);
        }

        public static Source FromStrings(params string[] sources)
        {
            return FromStrings((IEnumerable<string>)sources);
        }

        public static Source FromStrings(IEnumerable<string> sources)
        {
            return new MultiStringSource().WithStrings(sources);
        }

        public static Source FromSource(Source src)
        {
            return new CompositeSource().WithSource(src);
        }

        public static Source FromSources(params Source[] srcs)
        {
            return new CompositeSource().WithSources(srcs);
        }

        public static Source FromSources(IEnumerable<Source> srcs)
        {
            return new CompositeSource().WithSources(srcs);
        }
    }
}
