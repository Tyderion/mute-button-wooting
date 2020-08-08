﻿using System.ServiceProcess;

namespace WootingService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new WootingService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
