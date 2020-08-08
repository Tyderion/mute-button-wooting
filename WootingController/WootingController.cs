using System.Linq;
using System.ServiceProcess;
using WootingCommon;

namespace WootingController
{
    class WootingController
    {
        static void Main(string[] args)
        {
            ServiceController wootingService = ServiceController.GetServices().Where(service => service.ServiceName == "WootingService").FirstOrDefault();

            if (wootingService != null && wootingService.Status == ServiceControllerStatus.Running)
            {
                wootingService.ExecuteCommand((int)WootingCommands.Toggle);
            }
        }
    }
}
