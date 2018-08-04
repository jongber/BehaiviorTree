using AI.Contexts.Explorer;
using AI.Messages;
using AI.Nodes;
using System;
using System.Collections.Generic;
using System.Text;
using static AI.TreeExplorer;

namespace AI.Contexts
{
    public abstract class ExplorerContext
    {
        public ExplorerContext(MessageSender sender, int stackIndex, int myIndex, Node node)
        {
            this.Sender = sender;
            this.Node = node;
            this.StackIndex = stackIndex;
            this.MyIndex = myIndex;
        }

        public static ExplorerContext Create(int stackIndex, MessageSender sender, int myIndex, Node node)
        {
            switch (node.NodeType)
            {
                case Node.Type.Parallel:
                    return new ParallelContext(sender, stackIndex, myIndex, (ParallelNode)node);
                case Node.Type.Sequence:
                    return new SequnceContext(sender, stackIndex, myIndex, (SequenceNode)node);
            }

            return null;
        }

        public MessageSender Sender { get; }

        public int StackIndex { get; set; } = -1;

        public int MyIndex { get; set; } = -1;

        public Node Node { get; }

        public Node.State State { get; set; } = Node.State.Running;

        public abstract void OnStackPush();

        public abstract Node.State OnReceiveResult(int childIndex, Node.State result);
    }
}
