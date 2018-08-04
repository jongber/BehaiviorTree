using AI.Contexts;
using AI.Messages;
using AI.Messages.ToExplorer;
using AI.Messages.ToTaskRunner;
using AI.Target;
using System;
using System.Collections.Generic;
using System.Text;

namespace AI
{
    public class TaskWorker
    {
        private Queue<WorkerContext> initQ = new Queue<WorkerContext>();
        private List<WorkerContext> contextList = new List<WorkerContext>();

        public TaskWorker(MessageSender sender)
        {
            this.Sender = sender;
        }

        public MessageSender Sender { get; }

        public void DoWork(object target, int elapsed)
        {
            WorkerContext ctx = null;
            while (initQ.TryDequeue(out ctx))
            {
                if (ctx.Function != null)
                {
                    ctx.Function.Ready(target);
                    contextList.Add(ctx);
                }
                else
                {
                    this.Sender.SendMessage(new ChildResultMessage
                    {
                        ChildIndex = ctx.NodeIndex,
                        StackIndex = ctx.StackIndex,
                        Result = Nodes.Node.State.Failure
                    });
                }
            }

            List<WorkerContext> delCandi = new List<WorkerContext>();
            foreach (var context in contextList)
            {
                if (context.Function != null)
                {
                    var result = context.Function.Run(target, elapsed);
                    if (result != Nodes.Node.State.Running)
                    {
                        this.Sender.SendMessage(new ChildResultMessage
                        {
                            ChildIndex = context.NodeIndex,
                            StackIndex = context.StackIndex,
                            Result = result
                        });

                        delCandi.Add(context);

                        context.Function.Finished(target);
                    }
                }
                else
                {
                    this.Sender.SendMessage(new ChildResultMessage
                    {
                        ChildIndex = context.NodeIndex,
                        StackIndex = context.StackIndex,
                        Result = Nodes.Node.State.Failure
                    });

                    delCandi.Add(context);
                }
            }

            foreach (var candi in delCandi)
            {
                contextList.Remove(candi);
            }
        }

        public void HandleMessage(ToWokrerMessage msg)
        {
            if (msg is PushTaskMessage newMsg)
            {
                this.HandleMessage(newMsg);
            }
        }

        private void HandleMessage(PushTaskMessage msg)
        {
            initQ.Enqueue(new WorkerContext
            {
                StackIndex = msg.StackIndex,
                Function = msg.Function,
                NodeIndex = msg.MyIndex
            });
        }
    }
}
