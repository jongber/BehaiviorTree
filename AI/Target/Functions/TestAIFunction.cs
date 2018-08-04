using System;
using System.Collections.Generic;
using System.Text;
using AI.Nodes;

namespace AI.Target.Functions
{
    public class TestAIFunction : AIFunction
    {
        private int sum = 0;

        public void Finished(object target)
        {
        }

        public void Ready(object target)
        {
        }

        public Node.State Run(object target, int elapsed)
        {
            if (sum > 0)
            {
                Console.WriteLine("AI RUN Success");
                return Node.State.Success;
            }
            else
            {
                sum++;
                Console.WriteLine("AI RUN Running");
                return Node.State.Running;
            }
        }
    }
}
