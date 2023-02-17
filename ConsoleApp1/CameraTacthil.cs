using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public  class CameraTacthil
    {
        string fileName = @"C:\Users\ramik\PycharmProjects\pythonProject\main.py";
         string python = @"C:\Users\ramik\PycharmProjects\pythonProject\venv\Scripts\python.exe";
        public Process psi = new Process();
        public float Value;

        public void setValue(float value)
        {
            
            Value = value;
            //Console.WriteLine(psi.Id.ToString()+ Value);
        }
        public CameraTacthil()
        {


            var startInfo = new ProcessStartInfo(python, fileName)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true
            };


            psi = Process.Start(startInfo);
            
        }
        public void update()
        {
            using (NamedPipeClientStream pipeClient =
            new NamedPipeClientStream(".", "CSServer", PipeDirection.In))
            {
                {
                    // Connect to the pipe or wait until the pipe is available.
                    Console.Write("Attempting to connect to pipe...");
                    pipeClient.Connect();

                    Console.WriteLine("Connected to pipe.");
                    Console.WriteLine("There are currently {0} pipe server instances open.",
                       pipeClient.NumberOfServerInstances);
                    Debug.WriteLine("i entered the Using");
                    using (StreamReader sr = new StreamReader(pipeClient))
                    {
                        // Display the read text to the console
                        string temp;
                        while ( true)
                        {
                            Debug.WriteLine("i entered the While");
                            string? srValue = sr.ReadLine();
                            if (srValue != null)
                            {
                                temp = srValue;
                                if(temp == "Not Open")
                                {
                                    Debug.WriteLine("i notopent");
                                    Thread.Sleep(1000);
                                }
                                //setValue(float.Parse(temp ?? ""));
                            }
                            
                        }
                        Debug.WriteLine("i exited the While");

                    }
                    Debug.WriteLine("i exited the Using");
                }


            }
        }
    }
}
