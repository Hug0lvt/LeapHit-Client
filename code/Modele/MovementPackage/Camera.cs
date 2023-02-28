using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele.MovementPackage
{
    public class Camera  : IMovement
    {
        public string fileNamePath { get; private set; }
        public string python { get; private set; }

        public Process process { get; private set; } = new Process();
        public float coordonate { get; private set; }

        private NamedPipeClientStream pipeClient;


        public void update()
        {
            Debug.WriteLine(fileNamePath);
            pipeClient = new NamedPipeClientStream(".", "CSServer", PipeDirection.In);
            using (pipeClient)

            {
                {
                    // Connect to the pipe or wait until the pipe is available.
                    //Console.Write("Attempting to connect to pipe...");
                    pipeClient.Connect();

                    //Console.WriteLine("Connected to pipe.");
                    //Console.WriteLine("There are currently {0} pipe server instances open.",
                    //pipeClient.NumberOfServerInstances);
                    using (StreamReader sr = new StreamReader(pipeClient))
                    {
                        // Display the read text to the console
                        string temp;
                        while ((temp = sr.ReadLine()) != null)
                        {
                            if (temp == "VideoClosed")
                            {
                                Debug.WriteLine("i notopent");
                                sr.Close();
                                this.Stop();
                            }
                            //Position.Y=float.Parse(temp);
                        }
                    }
                }


            }
        }

        public void Start()
        {
            process = new Process();
            var startInfo = new ProcessStartInfo(python, fileNamePath)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = false,
                RedirectStandardError = true
            };


            process = Process.Start(startInfo);
            Thread t = new Thread(() => update());
            t.Start();
        }

        public void Stop()
        {
            pipeClient?.Close();
            process?.Kill();
        }

        public float GetMovement()
        {
            return coordonate;
        }
    }
}
