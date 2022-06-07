using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sender
{
    public interface IMessageBusClient
    {
        void Send(ProductDto message);
    }

    
}
