using System.Collections.Generic;

namespace Crosslight.API.IO.FileSystem.Abstractions
{
    public interface IDirectory : IFileSystemItem
    {
        string Name { get; }
        IDirectory Parent { get; }
        IEnumerable<IFileSystemItem> Items { get; }
    }
}
