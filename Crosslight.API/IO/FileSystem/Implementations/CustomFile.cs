using Crosslight.API.IO.FileSystem.Abstractions;

namespace Crosslight.API.IO.FileSystem.Implementations
{
    public class CustomFile : IFile
    {
        public string Name { get; private set; }

        public object Content { get; private set; }

        public IDirectory Parent { get; private set; }

        public CustomFile(string name, object content, IDirectory parent = null)
        {
            Name = name;
            Content = content;
            Parent = parent;
        }
    }
}
