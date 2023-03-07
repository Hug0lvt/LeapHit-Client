using Leap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele.MovementPackage.MotionSensorPong.LeapMotionPackage
{
    public class LeapListener : Listener
    {
        public event Action<HandList> OnHandMade;

        public override void OnFrame(Controller controller)
        {
            var frame = controller.Frame();
            if (frame.Hands.Count > 0)
                Task.Factory.StartNew(() => OnHandMade(frame.Hands));
        }
    }
}
