using Crosslight.API.IO.FileSystem.Abstractions;

namespace Crosslight.API.IO.FileSystem.Implementations
{
    public class StringFile : IStringFile
    {
        public string Name { get; private set; }

        public object Content => Text;

        public IDirectory Parent { get; private set; }

        public string Text { get; private set; }

        public StringFile(string name, string text, IDirectory parent = null)
        {
            Name = name;
            Text = text;
            Parent = parent;
        }
    }
}
