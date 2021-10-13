using Crosslight.API.IO.FileSystem.Abstractions;
using System.Collections.Generic;

namespace Crosslight.API.IO.FileSystem.Implementations
{
    public class FileSystemCollection : IDirectory
    {
        public string Name { get; private set; }

        public IDirectory Parent { get; private set; }

        public IList<IFileSystemItem> Items { get; private set; }

        public FileSystemCollection(string name, IDirectory parent = null)
        {
            Name = name;
            Parent = parent;
            Items = new List<IFileSystemItem>();
        }
    }
}
