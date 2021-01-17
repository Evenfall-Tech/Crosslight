using Crosslight.API.IO.FileSystem.Abstractions;
using Crosslight.API.IO.FileSystem.Implementations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Crosslight.API.IO.FileSystem
{
    public static class FileSystem
    {
        public static IDirectory FromDirectory(string path, bool includeSubdirectories = true)
        {
            var sourceDirectory = new DirectoryInfo(path);
            if (!sourceDirectory.Exists)
                throw new ArgumentException($"A directory with the given path does not exist: {path}", nameof(path));
            var resultingDirectory = new Implementations.Directory(sourceDirectory.FullName);
            var files = System.IO.Directory
                .GetFiles(path)
                .Select(filePath => new FileInfo(filePath))
                .Where(fileInfo => fileInfo.Exists)
                .Select(fileInfo => new PhysicalFile(
                    Path.ChangeExtension(fileInfo.Name, null),
                    fileInfo.Extension,
                    File.ReadAllBytes(fileInfo.FullName),
                    resultingDirectory
                ));
            foreach (var file in files)
            {
                resultingDirectory.Items.Add(file);
            }

            if (includeSubdirectories)
            {
                var directories = System.IO.Directory.GetDirectories(path);
                foreach (var dir in directories)
                {
                    IFileSystemItem subdirectory = FromDirectory(dir, true);
                    if (subdirectory != null)
                    {
                        resultingDirectory.Items.Add(subdirectory);
                    }
                }
            }

            return resultingDirectory;
        }

        public static IPhysicalFile FromFile(string filepath)
        {
            var fileInfo = new FileInfo(filepath);
            if (!fileInfo.Exists)
                throw new ArgumentException($"A file with the given path does not exist: {filepath}", nameof(filepath));
            return new PhysicalFile(
                Path.ChangeExtension(fileInfo.FullName, null),
                fileInfo.Extension,
                File.ReadAllBytes(filepath)
            );
        }

        public static IDirectory FromFiles(params string[] filenames)
        {
            return FromFiles((IEnumerable<string>)filenames);
        }

        public static IDirectory FromFiles(IEnumerable<string> filenames)
        {
            var resultingCollection = new FileSystemCollection("File System Collection");
            var files = filenames
                .Select(filePath => new FileInfo(filePath))
                .Where(fileInfo => fileInfo.Exists)
                .Select(fileInfo => new PhysicalFile(
                    Path.ChangeExtension(fileInfo.FullName, null),
                    fileInfo.Extension,
                    File.ReadAllBytes(fileInfo.FullName),
                    resultingCollection
                ));
            foreach (var file in files)
            {
                resultingCollection.Items.Add(file);
            }

            return resultingCollection;
        }

        public static IStringFile FromStream(StreamReader stream, string name = null)
        {
            return FromString(stream.ReadToEnd(), name);
        }

        public static IStringFile FromString(string source, string name = null)
        {
            return new StringFile(name ?? "String File", source);
        }

        public static IDirectory FromStrings(params string[] sources)
        {
            return FromStrings((IEnumerable<string>)sources);
        }

        public static IDirectory FromStrings(IEnumerable<string> sources)
        {
            var resultingCollection = new FileSystemCollection("File System Collection");
            var files = sources
                .Where(source => !string.IsNullOrWhiteSpace(source))
                .Select(source => new StringFile(
                    "String File",
                    source,
                    resultingCollection
                ));
            foreach (var file in files)
            {
                resultingCollection.Items.Add(file);
            }

            return resultingCollection;
        }
    }
}
