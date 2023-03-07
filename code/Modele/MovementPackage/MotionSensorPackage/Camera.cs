using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Modele.MovementPackage.MotionSensorPackage
{
    public class Camera : MotionSensor
    {
        private string exeFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..\\..\\..\\..\\Modele\\CameraExe\\dist\\main\\main.exe");
        private Process process;
        private Thread thread;
        private float coordonate;
        private NamedPipeClientStream pipeClient;
        private bool _stopThread = false;


        public void SetCoordonate(float value)
        {

            coordonate = value;
        }

        public void update()
        {
            Debug.WriteLine(exeFile);
            pipeClient = new NamedPipeClientStream(".", "CSServer", PipeDirection.In);
            using (pipeClient)
            {
                {
                    pipeClient.Connect();
                    using (StreamReader sr = new StreamReader(pipeClient))
                    {
                        // Display the read text to the console
                        string temp;
                        while (!_stopThread)
                        {
                            string? srValue = sr.ReadLine();
                            if (srValue != null)
                            {
                                temp = srValue;


                                if (temp == "VideoClosed")
                                {
                                    Debug.WriteLine("i notopent");
                                    sr.Close();
                                    return;
                                }
                                if (temp == "ready")
                                {
                                    setReady(true);
                                    continue;
                                }
                                SetCoordonate(float.Parse(temp) * 1080 / 800);
                            }
                        }
                        return;
                    }
                }


            }
        }

        public override float GetMovement()
        {
            return coordonate;
        }

        public override void StartMovement()
        {
            process = new Process();
            var startInfo = new ProcessStartInfo(exeFile)
            {
                RedirectStandardOutput = false,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = false,
            };


            process = Process.Start(startInfo);
            thread = new Thread(() => update());
            thread.Start();
        }

        public override void StopMovement()
        {
            _stopThread = true;
            pipeClient?.Close();
            process?.Kill();
        }
    }
}
