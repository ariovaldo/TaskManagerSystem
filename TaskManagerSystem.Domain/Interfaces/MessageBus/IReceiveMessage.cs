using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerSystem.Domain.Interfaces.MessageBus
{
    public interface IReceiveMessage
    {
        void ReceiveMessage<T>(T message);
    }
}
