using ConsoleApp1;
using System.Diagnostics;




void Main()
{
    CameraTacthil camera=new CameraTacthil();
    Thread t = new Thread(() => camera.update());
    t.Start();
    while (true)
    {

        Console.WriteLine("The Value: "+camera.Value+"\n");
     
  

    }

}

Main();