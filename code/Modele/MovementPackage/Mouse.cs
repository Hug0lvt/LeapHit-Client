using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace Modele.MovementPackage
{
    public class Mouse : IMovement
    {

        public float GetMovement()
        {
            return Microsoft.Xna.Framework.Input.Mouse.GetState().Y;
        }
    }

}
