using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Galaxi.Bus.Message
{
    public class TickedCreated
    {
        public TickedCreated(int functionId)
        {
            FunctionId = functionId;
        }

        public int FunctionId { get; }
    }
}
