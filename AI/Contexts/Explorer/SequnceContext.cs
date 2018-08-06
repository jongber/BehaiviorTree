using AI.Messages;
using AI.Messages.ToExplorer;
using AI.Messages.ToTaskRunner;
using AI.Nodes;
using System;

namespace AI.Contexts.Explorer
{
    public class SequnceContext : ExplorerContext
    {
        public SequnceContext(MessageSender sender, int stackIndex, int myIndex, SequenceNode node) 
            : base(sender, stackIndex, myIndex, node)
        {
        }

        public SequenceNode SequenceNode => (SequenceNode)this.Node;

        public override void OnStackPush()
        {
            var child = this.SequenceNode.Children[0];
            if (child.NodeType == Node.Type.Leaf && child is LeafNode leaf)
            {
                Sender.SendMessage(new PushTaskMessage
                {
                    Function = leaf.CreateFunction(),
                    MyIndex = 0,
                    StackIndex = this.StackIndex
                });
            }
            else
            {
                Sender.SendMessage(new PushNodeMessage(0, child));
            }
        }

        public override Node.State OnReceiveResult(int childIndex, Node.State result)
        {
            Console.WriteLine($"Sequence {childIndex} received");

            if (result == Node.State.Failure)
            {
                return Node.State.Failure;
            }
            else if (childIndex == this.SequenceNode.Children.Count - 1)
            {
                return Node.State.Success;
            }
            else
            {
                var child = this.SequenceNode.Children[childIndex + 1];
                if (child.NodeType == Node.Type.Leaf && child is LeafNode leaf)
                {
                    Sender.SendMessage(new PushTaskMessage
                    {
                        Function = leaf.CreateFunction(),
                        MyIndex = childIndex + 1,
                        StackIndex = this.StackIndex
                    });
                }
                else
                {
                    Sender.SendMessage(new PushNodeMessage(childIndex + 1, child));
                }
            }

            return Node.State.Running;
        }
    }
}
