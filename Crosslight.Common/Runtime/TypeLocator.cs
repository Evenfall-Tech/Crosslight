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

        public static InputLanguage[] LoadInputLanguageFromAssembly(string filePath)
        {
            return LoadTypeInstanceFromAssembly<InputLanguage>(filePath);
        }

        public static OutputLanguage[] LoadOutputLanguageFromAssembly(string filePath)
        {
            return LoadTypeInstanceFromAssembly<OutputLanguage>(filePath);
        }

        public static T[] LoadTypeInstanceFromAssembly<T>(string filePath) where T : class
        {
            var assembly = LoadAssembly(filePath);
            return CreateTypeInstance<T>(assembly);
        }

        public static T[] CreateTypeInstance<T>(Assembly assembly) where T : class
        {
            Type[] match = FindTypesInAssembly<T>(assembly);
            if (match == null || match.Length == 0) return null;
            return match.Select(x => Activator.CreateInstance(x) as T).ToArray();
        }

        public static T CreateTypeInstance<T>(Type type) where T : class
        {
            return (T)Activator.CreateInstance(type);
        }

        public static Type[] FindTypesInAssembly<T>(Assembly assembly) where T : class
        {
            var type = typeof(T);
           return assembly.GetTypes().Where(p => type.IsAssignableFrom(p)).ToArray();
        }
        public static Assembly LoadAssembly(string filePath) => Assembly.LoadFrom(filePath);
    }
}
