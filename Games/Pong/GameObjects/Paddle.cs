using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Reflection;
using System.Threading;

namespace Pong.GameObjects
{
    public class Paddle : GameObject
    {
        public string fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ,"..\\..\\..\\..\\main.exe");
        public string python = @"C:\Users\ramik\Desktop\leapHitpYTHIN\LeapHitClient\Games\build\exe.win-amd64-3.9\main.exe";
        public Process psi ;
        public float Value;
        private NamedPipeClientStream pipeClient;

        public void setValue(float value)
        {

            Value = value;
            //Console.WriteLine(psi.Id.ToString()+ Value);
        }
       
        public void update()
        {
            Debug.WriteLine(fileName);
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
                                this.close();
                            }
                            if (temp == "ready")
                            {
                                Debug.WriteLine("lessgo");
                                continue;
                            }
                            Position.Y=float.Parse(temp);
                        }
                    }
                }


            }
        }

        public void start()
        {
            psi = new Process();
            var startInfo = new ProcessStartInfo(python)
            {
                RedirectStandardOutput = false,
                UseShellExecute = false,
                CreateNoWindow = false,
                RedirectStandardError = false
            };


            psi = Process.Start(startInfo);
            Thread t = new Thread(() => update());
            t.Start();
        }

        public void close()
        {
            pipeClient?.Close();
            psi?.Kill();
        }
    }
}