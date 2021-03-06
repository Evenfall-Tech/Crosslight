﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.API.Nodes.Entities
{
    public enum TemplateTypeParameterVariance
    {
        Ordinary,
        In,
        Out,
    }
    /// <summary>
    /// <see cref="TemplateTypeParameterNode"/> represents
    /// template type paremeter T for generics.
    /// Supports [in|out] variance.
    /// </summary>
    public class TemplateTypeParameterNode : AttributedNode
    {
        public override string Type => nameof(TemplateTypeParameterNode);
        public TemplateTypeParameterVariance Variance { get; protected set; }
        public string Name { get; }
        public TemplateTypeParameterNode(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return Type;
        }
    }
}
