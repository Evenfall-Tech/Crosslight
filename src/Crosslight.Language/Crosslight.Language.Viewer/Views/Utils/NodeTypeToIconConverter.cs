using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Crosslight.API.Nodes.Implementations.Componentization;
using Crosslight.API.Nodes.Implementations.Entities;
using Crosslight.API.Nodes.Implementations.Entities.Types;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crosslight.Language.Viewer.Views.Utils
{
    public class NodeTypeToIconConverter : IBindingTypeConverter
    {
        private static readonly Dictionary<string, string> nodes = new Dictionary<string, string>()
        {
            { nameof(ProjectNode), "Assembly_16x.png" },
            { nameof(ModuleNode), "Module_16x.png" },
            { nameof(NamespaceDeclarationNode), "Namespace_16x.png" },
            { nameof(FunctionEntityDeclarationNode), "Delegate_16x.png" },
            { nameof(EnumDeclarationNode), "Enumerator_16x.png" },
            { nameof(InterfaceDeclarationNode), "Interface_16x.png" },
            { nameof(ClassDeclarationNode), "Class_16x.png" },
            { nameof(StructDeclarationNode), "Structure_16x.png" },
        };
        private static IAssetLoader assets;
        private static IAssetLoader Assets
        {
            get
            {
                if (assets == null) assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
                return assets;
            }
        }

        public int GetAffinityForObjects(Type fromType, Type toType)
        {
            if (fromType == typeof(string) && toType == typeof(IBitmap))
            {
                return 10;
            }
            return 0;
        }

        public bool TryConvert(object from, Type toType, object conversionHint, out object result)
        {
            do
            {
                if (toType != typeof(IBitmap) && toType != typeof(IImage)) break;
                if (!(from is string key)) break;

                string drawingName;
                if (!nodes.TryGetValue(key, out drawingName)) break;
                result = new Bitmap(Assets.Open(new Uri($"avares://Crosslight.Language.Viewer/Assets/Icons/{drawingName}")));
                return true;

            } while (false);
            result = null;
            return false;
        }
    }
}
