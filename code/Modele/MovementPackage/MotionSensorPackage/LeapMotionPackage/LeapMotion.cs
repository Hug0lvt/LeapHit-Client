using Leap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele.MovementPackage.MotionSensorPackage.LeapMotionPackage
{
    public class LeapMotion : MotionSensor
    {

        private static Controller controller = new Controller();
        private static LeapListener listener = new LeapListener();
        public float Coordonate;

        public LeapMotion()
        {
            controller.AddListener(listener);
            listener.OnHandMade += OnHandMade;
        }

        public void SetCoordonate(float value)
        {
            Coordonate = value;
        }

        void OnHandMade(HandList hands)
        {
            float cord = hands.FirstOrDefault().PalmPosition.y;

            cord = (float)(1080 - 3 * cord); //pour ne pas trop aller en haut
            cord += 300; //pour ne pas trop aller en bas

            if (cord < 0)
                cord = 0;
            if (cord > 1080)
                cord = 1080;
            SetCoordonate(cord);
            //Console.WriteLine(this.Coordonate);
        }

        public void OnClosing()
        {
            controller.RemoveListener(listener);
            controller.Dispose();
            listener.Dispose();
        }

        public override float GetMovement() => Coordonate;

    }
}
