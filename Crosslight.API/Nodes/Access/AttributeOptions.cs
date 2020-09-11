using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.API.Nodes.Access
{
    public class AttributeOptions
    {
        public AttributeTarget Target { get; set; }
    }

    public enum AttributeTarget
    {
        Project,
        Module,
        Field,
        Event, // TODO: if we won't use events, remove this.
        Method,
        Param,
        Property, // TODO: if we won't use properties, remove this.
        Return,
        Type, // Struct, class, interface, enum, or delegate.
    }
}
