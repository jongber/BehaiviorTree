using AI.Target;
using AI.Target.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AI.Nodes
{
    public class LeafNode : Node
    {
        public LeafNode() : base(Type.Leaf)
        {
        }
        
        public AIFunction CreateFunction()
        {
            return new TestAIFunction();
        }
    }
}
