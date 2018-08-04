using AI.Messages;
using AI.Messages.ToExplorer;
using AI.Nodes;
using AI.Target;
using System;
using System.Collections.Generic;
using System.Text;

namespace AI
{
    public class AIRunner : MessageSender
    {
        private List<ToWokrerMessage> toRunnerMessages = new List<ToWokrerMessage>();
        private List<ToExplorerMessage> toExplorerMessages = new List<ToExplorerMessage>();

        private object aiTarget;
        private TaskWorker worker;
        private TreeExplorer explorer;

        private Node root;

        public AIRunner(object target)
        {
            aiTarget = target;
            worker = new TaskWorker(this);
            explorer = new TreeExplorer(this);
        }

        public void Initialize(Node root)
        {
            this.root = root;

            this.SendMessage(new PushNodeMessage(root));
        }

        public void Run(int elapsed)
        {
            ////explorer.Explore();
            worker.DoWork(this.aiTarget, elapsed);
        }

        public void SendMessage(ToWokrerMessage msg)
        {
            this.worker.HandleMessage(msg);
        }

        public void SendMessage(ToExplorerMessage msg)
        {
            this.explorer.HandleMessage(msg);
        }
    }
}
