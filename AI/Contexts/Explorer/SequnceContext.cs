using System;
using System.Collections.Generic;
using System.Text;
using AI.Nodes;
using static AI.TreeExplorer;
using AI.Messages;

namespace AI.Contexts.Explorer
{
    public class SequnceContext : ExplorerContext
    {
        public SequnceContext(MessageSender sender, int stackIndex, int myIndex, SequenceNode node) 
            : base(sender, stackIndex, myIndex, node)
        {
        }

        public override void OnStackPush()
        {
        }

        public override Node.State OnReceiveResult(int childIndex, Node.State result)
        {
            throw new NotImplementedException();
        }
    }
}
