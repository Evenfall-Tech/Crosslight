using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Crosslight.API.IO
{
    public class Source
    {
        private IEnumerable<string> files;
        private IEnumerable<string> filedata;
        private Source()
        {
            files = null;
            filedata = null;
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
            return FromFiles(filenames);
        }

        public static Source FromFiles(IEnumerable<string> filenames)
        {
            return new Source()
            {
                files = filenames,
                filedata = null,
            };
        }

        public static Source FromString(string source)
        {
            return new Source()
            {
                files = null,
                filedata = new string[] { source },
            };
        }
    }
}
