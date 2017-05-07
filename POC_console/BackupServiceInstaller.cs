using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;


namespace POC_console
{
    [RunInstaller(true)]
    class BackupServiceInstaller : Installer
    {
        public BackupServiceInstaller()
        {
            ServiceProcessInstaller serviceProcessInstaller =
                                   new ServiceProcessInstaller();
            ServiceInstaller serviceInstaller = new ServiceInstaller();

            //# Service Account Information
            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            serviceProcessInstaller.Username = null;
            serviceProcessInstaller.Password = null;

            //# Service Information
            serviceInstaller.DisplayName = "Backup Utility Windows Service";
            serviceInstaller.StartType = ServiceStartMode.Automatic;

            //# This must be identical to the WindowsService.ServiceBase name
            //# set in the constructor of WindowsService.cs
            serviceInstaller.ServiceName = "BackupService";

            this.Installers.Add(serviceProcessInstaller);
            this.Installers.Add(serviceInstaller);
        }
    }
}
