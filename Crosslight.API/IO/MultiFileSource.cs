using System.Collections.Generic;

namespace Crosslight.API.IO
{
    public class MultiFileSource : Source
    {
        private List<string> files;

        public IEnumerable<string> Files { get => files; }
        public override int Count => files == null ? 0 : files.Count;

        public MultiFileSource()
        {
            files = new List<string>();
        }
        public MultiFileSource WithFilePath(string path)
        {
            files.Add(path);
            return this;
        }
        public MultiFileSource WithFilePaths(IEnumerable<string> paths)
        {
            files.AddRange(paths);
            return this;
        }
    }
}
