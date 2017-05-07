using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace POC_console
{
    partial class BackupService : ServiceBase
    {
        public BackupService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            StartService();
        }

        public void StartService()
        {
            POC.Scheduler.Test();
            //Console.WriteLine("Windows service started");
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            StopService();
        }

        public void StopService()
        {
            POC.Scheduler.Shutdown();
        }

        protected override void OnShutdown()
        {
            POC.Scheduler.Shutdown();
        }
    }
}
