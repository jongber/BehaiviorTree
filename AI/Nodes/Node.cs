using System;
using System.Collections.Generic;
using System.Text;

namespace AI.Nodes
{
    public class Node
    {
        public enum Type
        {
            Parallel,
            Sequence,
            Leaf
        }

        public enum State
        {
            Success,
            Failure,
            Running
        }

        public Node(Type type)
        {
            this.NodeType = type;
        }

        public Type NodeType { get; }
    }
}
