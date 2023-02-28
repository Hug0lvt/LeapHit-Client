using Leap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongClient.Drivers.Leap
{
    public class LeapListener : Listener
    {
        public event Action<HandList> OnHandMade;
        private long _now;
        private long _previous;
        private long _timeDifference;

        public override void OnFrame(Controller controller)
        {
            var frame = controller.Frame();
            _now = frame.Timestamp;
            _timeDifference = _now - _previous;
            _previous = frame.Timestamp;

            if (frame.Hands.IsEmpty || _timeDifference < 1000) return;

            //Appel frame
            if (frame.Hands.Count > 0)
                Task.Factory.StartNew(() => OnHandMade(frame.Hands));

        }
    }
}
