using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

//using MonoGame.Extended; est ce que je me met en dependance de monogame

namespace Modele.Mouvement
{
    public class Mouse : IControlMouvement
    {

        public float GetCoordonate()
        {
            //return MouseExtended.GetState().Position.Y;
        }
    }

}
