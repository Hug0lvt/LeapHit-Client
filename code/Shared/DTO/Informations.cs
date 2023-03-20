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
        public double Frame { get; set; }
        public string TypeData { get; set; }

        public Informations(Action action, double frame, string type)
        {
            Action = action;
            Frame = frame;
            TypeData = type;
        }
    }
}
