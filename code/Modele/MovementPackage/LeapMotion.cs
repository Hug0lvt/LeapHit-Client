using Leap;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele.MovementPackage
{
    class LeapMotion
    {

        private static Controller controller = new Controller();
        private static LeapListener listener = new LeapListener();
        public float Coord { get; private set; }

        public LeapMotion()
        {
            controller.AddListener(listener);
            listener.OnHandMade += OnHandMade;
        }

        void OnHandMade(HandList hands)
        {
            Coord = hands.FirstOrDefault().PalmPosition.y;
            Debug.WriteLine("EventLeap value={0}",Coord);
        }


        public void OnClosing()
        {
            controller.RemoveListener(listener);
            controller.Dispose();
            listener.Dispose();
        }

    }
}
