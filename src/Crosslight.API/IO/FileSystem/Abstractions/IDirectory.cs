using System.Collections.Generic;

namespace Crosslight.API.IO.FileSystem.Abstractions
{
    public interface IDirectory : IFileSystemItem
    {
        IDirectory Parent { get; }
        IList<IFileSystemItem> Items { get; }
    }
}
