using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class ObjectTransfert<T>
    {
        public Messages Message { get; set; }
        public T Data { get; set; }
    }
}
