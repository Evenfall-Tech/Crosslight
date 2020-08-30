using Crosslight.API.Lang;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Crosslight.Common.Runtime
{
    public static class TypeLocator
    {
        public static IEnumerable<string> LocateCrosslightAssembliesInDirectory(string path)
        {
            return Directory.GetFiles(path, "Crosslight.*.dll");
        }

        public static InputLanguage LoadInputLanguageFromAssembly(string filePath)
        {
            return LoadTypeInstanceFromAssembly<InputLanguage>(filePath);
        }

        public static OutputLanguage LoadOutputLanguageFromAssembly(string filePath)
        {
            return LoadTypeInstanceFromAssembly<OutputLanguage>(filePath);
        }

        private static T LoadTypeInstanceFromAssembly<T>(string filePath) where T : class
        {
            var assembly = Assembly.LoadFrom(filePath);
            var type = typeof(T);
            Type match = assembly
                .GetTypes()
                .Where(p => type.IsAssignableFrom(p))
                .FirstOrDefault();
            if (match == null) return null;
            return (T)Activator.CreateInstance(match);
        }
    }
}
