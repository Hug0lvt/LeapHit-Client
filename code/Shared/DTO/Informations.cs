using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class Informations
    {
        public Action Action { get; set; }
        public long Frame { get; set; }
        public string TypeData { get; set; }

        public Informations(Action action, long frame, string type)
        {
            Action = action;
            Frame = frame;
            TypeData = type;
        }

        public Informations()
        {
            
        }
    }
}
