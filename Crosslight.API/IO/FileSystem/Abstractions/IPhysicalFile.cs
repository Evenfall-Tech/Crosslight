namespace Crosslight.API.IO.FileSystem.Abstractions
{
    public interface IPhysicalFile : IFile
    {
        string Extension { get; }
        string Path { get; }
        byte[] Data { get; }
    }
}
