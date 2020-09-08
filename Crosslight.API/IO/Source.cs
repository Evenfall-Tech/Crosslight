using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Crosslight.API.IO
{
    public class Source
    {
        public bool HasFiles => Files != null && Files.Count() > 0;
        public bool HasData => Data != null && Data.Count() > 0;
        public IEnumerable<string> Files { get; protected set; }
        public IEnumerable<string> Data { get; protected set; }

        private Source()
        {
            Files = null;
            Data = null;
        }
        public static Source FromFile(string filename)
        {
            return FromFiles(new string[] { filename });
        }

        public static Source FromStream(StreamReader stream)
        {
            return FromString(stream.ReadToEnd());
        }

        public static Source FromDirectory(string path)
        {
            var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            return FromFiles(files);
        }

        public static Source FromFiles(params string[] filenames)
        {
            return FromFiles((IEnumerable<string>)filenames);
        }

        public static Source FromFiles(IEnumerable<string> filenames)
        {
            return new Source()
            {
                Files = filenames,
                Data = null,
            };
        }

        public static Source FromString(string source)
        {
            return new Source()
            {
                Files = null,
                Data = new string[] { source },
            };
        }
    }
}
