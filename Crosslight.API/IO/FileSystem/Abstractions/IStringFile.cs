namespace Crosslight.API.IO.FileSystem.Abstractions
{
    public interface IStringFile : IFile
    {
        string Text { get; }
    }
}
