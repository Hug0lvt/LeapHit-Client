using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele.SkinPackage
{
    public class Skin
    {
        private string asset;
        private string name;

        public Skin(string asset, string name)
        {
            this.asset = asset;
            this.name = name;
        }

        public string Asset { get { return asset; } }
        public string Name { get { return name; } }
    }
}
