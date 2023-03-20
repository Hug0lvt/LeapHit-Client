using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class Informations<T>
    {
        public Action Action { get; set; }
        public double Frame { get; set; }
        public T TypeData { get; set; }
    }
}
