using AI.Target;
using System;
using System.Collections.Generic;
using System.Text;

namespace AI.Contexts
{
    public class WorkerContext
    {
        public int StackIndex { get; set; }

        public int NodeIndex { get; set; }

        public AIFunction Function { get; set; }
    }
}
