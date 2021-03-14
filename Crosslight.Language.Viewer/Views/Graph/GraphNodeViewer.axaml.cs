using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Media;
using Avalonia.ReactiveUI;
using Crosslight.Language.Viewer.ViewModels.Graph;
using Crosslight.Language.Viewer.Views.Utils;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive.Disposables;

namespace Crosslight.Language.Viewer.Views.Graph
{
    public class GraphNodeViewer : ReactiveUserControl<NodeViewModel>
    {
        private TextBlock NodeText => this.FindControl<TextBlock>("nodeText");
        private Button NodeButton => this.FindControl<Button>("nodeButton");
        private Image NodeIcon => this.FindControl<Image>("nodeIcon");

        private static readonly NodeTypeToIconConverter conNtoI = new NodeTypeToIconConverter();

        public GraphNodeViewer()
        {
            this.WhenActivated(disp =>
            {
                this.OneWayBind(ViewModel, x => x.Data, x => x.NodeText.Text)
                    .DisposeWith(disp);

                this.OneWayBind(ViewModel, x => x.Type, x => x.NodeIcon.Source, null, conNtoI)
                    .DisposeWith(disp);
                this.WhenAnyValue(x => x.ViewModel.State)
                    .Subscribe(UpdateStyle)
                    .DisposeWith(disp);
                this.BindCommand(ViewModel, x => x.SetStartNode, x => x.NodeButton)
                    .DisposeWith(disp);
            });
            this.InitializeComponent();
        }

        private void UpdateStyle(NodeState state)
        {
            NodeButton.Classes.Clear();
            NodeButton.Classes.Add(StateToString(state));
        }

        private string StateToString(NodeState state)
        {
            return state switch
            {
                NodeState.Active => "Active",
                NodeState.Inactive => "Inactive",
                NodeState.Primary => "Primary",
                _ => throw new NotImplementedException(),
            };
        }

        public Drawing GetDrawing(string name)
        {
            try
            {
                return Resources[name] as Drawing;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
