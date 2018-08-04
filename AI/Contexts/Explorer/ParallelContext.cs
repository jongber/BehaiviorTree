using System;
using System.Collections.Generic;
using System.Text;
using AI.Nodes;
using static AI.TreeExplorer;
using AI.Messages;
using AI.Messages.ToExplorer;
using AI.Messages.ToTaskRunner;

namespace AI.Contexts.Explorer
{
    public class ParallelContext : ExplorerContext
    {
        private List<Node.State> childrenResult = new List<Node.State>();

        public ParallelContext(MessageSender sender, int stackIndex, int myIndex, ParallelNode node) 
            : base(sender, stackIndex, myIndex, node)
        {
        }

        public ParallelNode ParallelNode => (ParallelNode)this.Node;

        public override void OnStackPush()
        {
            for (int i = 0; i < this.ParallelNode.Children.Count; ++i)
            {
                var child = this.ParallelNode.Children[i];
                if (child.NodeType == Node.Type.Leaf && child is LeafNode leaf)
                {
                    Sender.SendMessage(new PushTaskMessage
                    {
                        Function = leaf.CreateFunction(),
                        MyIndex = i,
                        StackIndex = this.StackIndex
                    });
                }
                else
                {
                    Sender.SendMessage(new PushNodeMessage(i, child));
                }
            }
        }

        public override Node.State OnReceiveResult(int childIndex, Node.State result)
        {
            Console.WriteLine($"{childIndex} received");

            childrenResult.Add(result);
            if (childrenResult.Count == this.ParallelNode.Children.Count)
            {
                return Node.State.Success;
            }
            else
            {
                return Node.State.Running;
            }
        }
    }
}
