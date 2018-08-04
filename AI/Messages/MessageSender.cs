using System;
using System.Collections.Generic;
using System.Text;

namespace AI.Messages
{
    public interface MessageSender
    {
        void SendMessage(ToExplorerMessage msg);

        void SendMessage(ToWokrerMessage msg);
    }
}
