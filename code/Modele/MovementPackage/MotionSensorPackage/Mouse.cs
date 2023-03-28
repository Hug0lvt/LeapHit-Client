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


namespace Modele.MovementPackage.MotionSensorPackage
{
    public class Mouse : MotionSensor
    {
        public override float GetMovement()
        {
            return MouseExtended.GetState().Position.Y;
        }
    }
}
