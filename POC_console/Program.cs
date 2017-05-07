using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace POC_console
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    if (Environment.UserInteractive)
        //    {
        //        string param = string.Concat(args);
        //        switch(param)
        //        {
        //            //case
        //            //case
        //            default:
        //                BackupService service = new BackupService();
        //                service.StartService();
        //                Console.WriteLine("Press enter to stop service");
        //                Console.Read();
        //                service.Stop();
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        ServiceBase.Run(new BackupService());
        //        //todo add installer
        //    }
        //}

        static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                string parameter = string.Concat(args);
                switch (parameter)
                {
                    case "--install":
                        ManagedInstallerClass.InstallHelper(new[] { Assembly.GetExecutingAssembly().Location });
                        break;
                    case "--uninstall":
                        ManagedInstallerClass.InstallHelper(new[] { "/u", Assembly.GetExecutingAssembly().Location });
                        break;
                }

                //string param = string.Concat(args);
                //        switch(param)
                //        {
                //            //case
                //            //case
                //            default:
                //                BackupService service = new BackupService();
                //                service.StartService();
                //                Console.WriteLine("Press enter to stop service");
                //                Console.Read();
                //                service.Stop();
                //                break;
                //        }
            }
            else
            {
                ServiceBase[] servicesToRun = new ServiceBase[]
                                  {
                              new BackupService()
                                  };
                ServiceBase.Run(servicesToRun);
            }
        }
    }
}