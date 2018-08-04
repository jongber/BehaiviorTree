using AI.Contexts;
using AI.Contexts.Explorer;
using AI.Messages;
using AI.Messages.ToExplorer;
using AI.Nodes;
using System;
using System.Collections.Generic;

namespace AI
{
    public class TreeExplorer
    {
        private MessageSender sender;
        ////private List<ToExplorerMessage> messages = new List<ToExplorerMessage>();

        public List<ExplorerContext> Stack { get; } = new List<ExplorerContext>();

        public TreeExplorer(MessageSender sender)
        {
            this.sender = sender;
        }

        ////public void Explore()
        ////{
        ////    List<ToExplorerMessage> old = messages;

        ////    messages = new List<ToExplorerMessage>();

        ////    foreach (var msg in old)
        ////    {
        ////        if (msg is PushNodeMessage pMsg)
        ////        {
        ////            this.HandleMessage(pMsg);
        ////        }
        ////        else if (msg is ChildResultMessage cMsg)
        ////        {
        ////            this.HandleMessage(cMsg);
        ////        }
        ////    }
        ////}

        public void HandleMessage(ToExplorerMessage msg)
        {
            ////messages.Add(msg);
            if (msg is PushNodeMessage pMsg)
            {
                this.HandleMessage(pMsg);
            }
            else if (msg is ChildResultMessage cMsg)
            {
                this.HandleMessage(cMsg);
            }
        }

        private void HandleMessage(PushNodeMessage msg)
        {
            Console.WriteLine("PushNodeMessage Received");

            ExplorerContext context = null;

            switch (msg.Node.NodeType)
            {
                case Node.Type.Parallel:
                    context = ExplorerContext.Create(this.Stack.Count, sender, msg.MyIndex, msg.Node);
                    break;
                case Node.Type.Sequence:
                    context = ExplorerContext.Create(this.Stack.Count, sender, msg.MyIndex, msg.Node);
                    break;
            }

            if (context != null && msg.Node is CompositeNode compositeNode)
            {
                if (compositeNode.Children.Count == 0)
                {
                    this.HandleMessage(new ChildResultMessage
                    {
                        ChildIndex = context.MyIndex,
                        Result = Node.State.Success,
                        StackIndex = this.Stack.Count - 1
                    });
                }
                else
                {
                    this.Stack.Add(context);
                    context.OnStackPush();
                }
            }
        }

        private void HandleMessage(ChildResultMessage msg)
        {
            Console.WriteLine("ChildEndMessaged Received");
            //// TODO change this to while loop... not use recursive..
            try
            {
                var context = this.Stack[msg.StackIndex];

                var parentResult = context.OnReceiveResult(msg.ChildIndex, msg.Result);

                if (parentResult != Nodes.Node.State.Running)
                {
                    this.Stack.Remove(context);

                    var parentIndex = msg.StackIndex - 1;
                    if (parentIndex >= 0)
                    {
                        var parentContext = this.Stack[parentIndex];

                        this.HandleMessage(new ChildResultMessage
                        {
                            StackIndex = parentIndex,
                            Result = parentResult,
                            ChildIndex = context.MyIndex
                        });
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
