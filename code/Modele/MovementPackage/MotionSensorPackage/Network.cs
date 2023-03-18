using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele.MovementPackage.MotionSensorPackage
{
    public class Network : MotionSensor
    {
        public override float GetMovement()
        {
            throw new NotImplementedException();
        }

        public override void StartMovement()
        {
            base.StartMovement();
        }

        public override void StopMovement()
        {
            base.StopMovement();
        }
    }
}
