using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// ProjectNode represents the project abstraction in the language.
    /// E.g. in C# it is assembly, for Java it is executable .jar,
    /// in C++ it is static program.
    /// Nothing is higher than ProjectNode, so its Parent property is null.
    /// </summary>
    public class ProjectNode : Node
    {
        public ObservableCollection<ModuleNode> Modules { get => Modules; set { Modules = value; /*append value to Children*/} }
        public ProjectNode()
        {
            Modules = new ObservableCollection<ModuleNode>();
            Modules.CollectionChanged += Modules_CollectionChanged;
        }

        private void Modules_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == NotifyCollectionChangedAction.Add)
            {
                // append e.NewItems to Children
            }
        }

        public override string ToString()
        {
            return "ProjectNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
