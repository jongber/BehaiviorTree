using AI.Nodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AI.Messages.ToExplorer
{
    public class PushNodeMessage : ToExplorerMessage
    {
        public PushNodeMessage(int myIndex, Node node)
        {
            this.Node = node;
            this.MyIndex = myIndex;
        }

        public PushNodeMessage(Node node)
        {
            this.Node = node;
            this.MyIndex = -1;
        }

        public Node Node { get; }
        
        public int MyIndex { get; }
    }
}
