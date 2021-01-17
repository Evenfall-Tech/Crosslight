namespace Crosslight.API.IO.FileSystem.Abstractions
{
    public interface IFile : IFileSystemItem
    {
        string Name { get; }
        object Content { get; }
        IDirectory Parent { get; }
    }
}
