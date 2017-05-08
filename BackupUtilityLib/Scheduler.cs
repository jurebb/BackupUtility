using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Configuration;
using BackupUtilityLib;

using Quartz;
using Quartz.Impl;

namespace BackupUtilityLib
{
    public class Scheduler
    {
        public Scheduler()
        {
        }

        public static List<IScheduler> scheds = new List<IScheduler>();

        void Main(string[] args)
        {
            Test();
        }

        public void Test()
        {
            string[] sources = BackupSources.SourceDirs;
            string[] cronScheds = BackupSources.Schedulers;
            int numOfSources = BackupSources.NumOfSources;
            
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            List<IJobDetail> jobDetails = new List<IJobDetail>();
            List<ITrigger> triggs = new List<ITrigger>();

            for (int i = 0; i < numOfSources; i++)
            {
                scheds.Add(schedulerFactory.GetScheduler());

                jobDetails.Add(JobBuilder.Create<FolderCopy>()
                    .WithIdentity("BackupJob" + i.ToString())
                    .Build());
                jobDetails[i].JobDataMap["source"] = sources[i];
                triggs.Add(TriggerBuilder.Create()
                    .ForJob(jobDetails[i])
                    .WithCronSchedule(cronScheds[i])                      //("0 0 12 * * ?") svaki dan u 12h ("0 * 10 * * ?") svaka min. u 10h ("0 * * * * ?") svaki min
                    .WithIdentity("BackupTrigger" + i.ToString())
                    .StartNow()
                    .Build());
            }
      
            int j = 0;
            foreach(IScheduler sched in scheds)
            {
                sched.ScheduleJob(jobDetails[j], triggs[j]);
                sched.Start();
                j++;
            }
        }

        public void Shutdown()
        {
            int j = 0;
            foreach (IScheduler sched in scheds)
            {
                //sched.Shutdown(true);
                sched.Clear();
                j++;
            }
            scheds.Clear();
        }
    }
}
