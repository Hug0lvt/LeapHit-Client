using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele.MovementPackage.MotionSensorPackage
{
    public abstract class MotionSensor : IMovement, INotifyPropertyChanged
    {

        private bool _ready = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Ready
        {
            get { return _ready; }
            set
            {
                if (_ready != value)
                {
                    _ready = value;
                    NotifyPropertyChanged("Ready");
                }
            }
        }
        public abstract float GetMovement();

        public virtual void StartMovement() { SetReady(true); }

        public virtual void StopMovement() { SetReady(false); }

        protected void SetReady(bool ready) { Ready = ready; }

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
