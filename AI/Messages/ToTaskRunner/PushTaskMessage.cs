using AI.Nodes;
using AI.Target;
using System;
using System.Collections.Generic;
using System.Text;

namespace AI.Messages.ToTaskRunner
{
    public class PushTaskMessage : ToWokrerMessage
    {
        public AIFunction Function { get; set; }

        public int MyIndex { get; set; }

        public int StackIndex { get; set; }
    }
}
