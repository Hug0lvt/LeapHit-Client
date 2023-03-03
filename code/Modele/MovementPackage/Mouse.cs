using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Input;


namespace Modele.MovementPackage
{
    public class Mouse : IMovement
    {

        public float GetMovement()
        {
            return MouseExtended.GetState().Position.Y;
        }

        public void StartMovement()
        {
            return;
        }

        public void StopMovement()
        {
            return;
        }
    }

}
