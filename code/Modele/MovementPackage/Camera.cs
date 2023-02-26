using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Modele.MovementPackage
{
    public class Camera  : IMovement
    {
        public string exeFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..\\..\\..\\..\\build\\exe.win-amd64-3.9\\main.exe");
        public Process process;
        public float coordonate;
        private NamedPipeClientStream pipeClient;

        public void setCoordonate(float value)
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
                        while (true)
                        {
                            string? srValue = sr.ReadLine();
                            if (srValue != null)
                            {
                                 temp = srValue;
                            
                              
                                if (temp == "VideoClosed")
                                {
                                    Debug.WriteLine("i notopent");
                                    sr.Close();
                                    this.close();
                                }
                                if (temp == "ready")
                                {
                                    Debug.WriteLine("lessgo");
                                    continue;
                                }
                                coordonate= float.Parse(temp);
                            }
                        }
                    }
                }


            }
        }

        public void startMovement()
        {
            Debug.WriteLine("les go");
            process = new Process();
            var startInfo = new ProcessStartInfo(exeFile)
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

        public void close()
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
