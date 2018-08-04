using System;
using System.Collections.Generic;
using System.Text;
using static AI.Nodes.Node;

namespace AI.Target
{
    public interface AIFunction
    {
        void Ready(object target);

        State Run(object target, int elapsed);

        void Finished(object target);
    }
}
