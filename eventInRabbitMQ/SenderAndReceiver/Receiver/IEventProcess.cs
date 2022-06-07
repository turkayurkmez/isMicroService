using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receiver
{
    public interface IEventProcess
    {
        void ProcessEvent(string message);
        
    }





}
