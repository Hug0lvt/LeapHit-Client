using Modele.MovementPackage;
using PongClient.Drivers.Leap;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CLI_LeapMotionGemini
{
    public class LeapController : IMovement
    {

        LeapMotion device;

        private void LaunchSvcLeapMotion()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            //Verif Path
            startInfo.FileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "LeapSvc.exe");
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            
            try
            {
                
                //optimisation posible si le svc est déja start
                using (Process process = Process.Start(startInfo))
                {
                    if(process != null)
                    {
                        process.WaitForExitAsync();
                        Debug.WriteLine("Service ON !");
                    }
                    else
                    {
                        Debug.WriteLine("Le Service n'a pas pu démarré !");
                    }
                    
                }
                
            }
            catch
            {
                Debug.WriteLine("Une erreur c'est produite a la création du service !");
            }
        }

        private void StopSvcLeapMotion()
        {
            foreach (var process in Process.GetProcessesByName("LeapSvc"))
            {
                process.Kill();
                Debug.WriteLine("Kill");
            }
            
        }

        public void Start()
        {
            LaunchSvcLeapMotion();
            device= new LeapMotion();
        }

        public void Stop()
        {
            device.OnClosing();
            StopSvcLeapMotion();
        }

        public float GetMovement()
        {
            return device.Coord;
        }
    }
}
