using System;
using System.Collections.Generic;
using System.Text;

namespace AI.Nodes
{
    public class CompositeNode : Node
    {
        public CompositeNode(Node.Type type) : base(type)
        {
        }

        public List<Node> Children { get; } = new List<Node>();
    }
}
