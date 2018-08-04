using AI.Nodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AI.Messages.ToExplorer
{
    public class ChildResultMessage : ToExplorerMessage
    {
        public int ChildIndex { get; set; } = -1;

        public int StackIndex { get; set; } = -1;

        public Node.State Result { get; set; } = Node.State.Running;
    }
}
