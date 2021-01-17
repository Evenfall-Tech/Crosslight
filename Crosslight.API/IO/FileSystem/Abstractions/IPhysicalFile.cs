namespace Crosslight.API.IO.FileSystem.Abstractions
{
    public interface IPhysicalFile: IFile
    {
        string Extension { get; }
        byte[] Data { get; }
    }
}
