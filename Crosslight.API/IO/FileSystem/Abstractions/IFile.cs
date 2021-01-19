namespace Crosslight.API.IO.FileSystem.Abstractions
{
    public interface IFile : IFileSystemItem
    {
        object Content { get; }
        IDirectory Parent { get; }
    }
}
