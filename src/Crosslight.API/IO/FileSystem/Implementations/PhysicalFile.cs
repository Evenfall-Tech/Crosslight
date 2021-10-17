using Crosslight.API.IO.FileSystem.Abstractions;

namespace Crosslight.API.IO.FileSystem.Implementations
{
    public class PhysicalFile : IPhysicalFile
    {
        public string Extension { get; private set; }

        public string Name { get; private set; }

        public string Path => Name + Extension;

        public object Content => Data;

        public IDirectory Parent { get; private set; }

        public byte[] Data { get; private set; }

        public PhysicalFile(string name, string extension, byte[] data, IDirectory parent = null)
        {
            Name = name;
            Extension = extension;
            Parent = parent;
            Data = data;
        }
    }
}
