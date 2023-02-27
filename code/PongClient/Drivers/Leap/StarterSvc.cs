using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI_LeapMotionGemini
{
    public static class StarterSvc
    {

        public static void LaunchSvcLeapMotion()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            
            //startInfo.FileName= Path.Combine(AppDomain.CurrentDomain.BaseDirectory)
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = @"C:\Dev\LeapHitTeam\MesTests\CLI_LeapMotionGemini\CLI_LeapMotionGemini\Driver\svcleap\LeapSvc.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            
            try
            {
                
                //optimisation posible si le svc est déja start
                using (Process process = Process.Start(startInfo))
                {
                    if(process != null)
                    {
                        process.WaitForExitAsync();
                        Console.WriteLine("Service ON !");
                    }
                    else
                    {
                        Console.WriteLine("Le Service n'a pas pu démarré !");
                    }
                    
                }
                
            }
            catch
            {
                Console.WriteLine("Une erreur c'est produite a la création du service !");
            }
        }

        public static  void StopSvcLeapMotion()
        {
            foreach (var process in Process.GetProcessesByName("LeapSvc"))
            {
                process.Kill();
                Console.WriteLine("Kill");
            }
            
        }
    }
}
