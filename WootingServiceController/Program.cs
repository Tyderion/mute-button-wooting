using System;
using System.Threading;


namespace WootingServiceController
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceController[] scServices;
            scServices = ServiceController.GetServices();

            foreach (ServiceController scTemp in scServices)
            {

                if (scTemp.ServiceName == "Simple Service")
                {
                    // Display properties for the Simple Service sample
                    // from the ServiceBase example.
                    ServiceController sc = new ServiceController("Simple Service");
                    Console.WriteLine("Status = " + sc.Status);
                    Console.WriteLine("Can Pause and Continue = " + sc.CanPauseAndContinue);
                    Console.WriteLine("Can ShutDown = " + sc.CanShutdown);
                    Console.WriteLine("Can Stop = " + sc.CanStop);
                    if (sc.Status == ServiceControllerStatus.Stopped)
                    {
                        sc.Start();
                        while (sc.Status == ServiceControllerStatus.Stopped)
                        {
                            Thread.Sleep(1000);
                            sc.Refresh();
                        }
                    }
                }
            }
        }
    }
}
