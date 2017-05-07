using System;
using System.Linq;
using System.IO;

using Quartz;

namespace BackupUtilityLib
{
    public class FolderCopy : IJob
    {
        #region fields
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);     //log4net logiranje 
        private static int backupNum = int.Parse(BackupSources.GetConfig("numOfBackups"));                                                          //specificira broj zadnjih vremenskih verzija koje se spremaju     
        private static bool continueBackupOnError = Convert.ToBoolean(BackupSources.GetConfig("continueBackupOnError"));                            //determines if backup process continues in the case of a single file/folder error during backup
        private static string backupDestinationDir = BackupSources.GetConfig("backupDestinationDir");
        public HashBasedFolderCheck _hashBasedFolderCheck;
        #endregion

        /// <summary>
        /// glavna metoda
        /// </summary>
        private static void BackupFolder(string sourceDir, string destDir)       
        {
            DirectoryInfo directory = new DirectoryInfo(sourceDir);
            if(directory.Exists == false)                                           //ako source ne postoji
            {
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: "+ sourceDir);
            }
            DirectoryInfo[] subDirectories = directory.GetDirectories();            //vraca subdirektorije trenutnog dir-a
            if (Directory.Exists(destDir) == false)                                 //ako dest ne postoji
            {
                Directory.CreateDirectory(destDir);
            }
            FileInfo[] content = directory.GetFiles();                              //vraca file listu trenutnog dir-a
            foreach(FileInfo file in content)
            {
                string path = Path.Combine(destDir, file.Name);

                if (continueBackupOnError == false)
                {
                    file.CopyTo(path);
                }
                else
                {
                    try
                    {
                        file.CopyTo(path);
                    }
                    catch (Exception ex)
                    {
                        log.ErrorFormat(ex.Message);
                    }
                } 
            }
            foreach(DirectoryInfo dir in subDirectories)
            {
                string path = Path.Combine(destDir, dir.Name);
                BackupFolder(dir.FullName, path);       //rekurzivni poziv ove metode za svaki subdirektorij
            }
            return;
        }

        /// <summary>
        /// brise najstariji backup
        /// </summary>
        private static void DeleteOldestBackup(string dirName)                
        {
            foreach (var directory in new DirectoryInfo(backupDestinationDir).GetDirectories($"*{dirName}*").OrderByDescending(x => x.LastWriteTime).Skip(backupNum))
                directory.Delete(true);
        }

        /// <summary>
        /// za scheduler
        /// </summary>
        public void Execute(IJobExecutionContext context)           
        {
            string sourceDir = context.JobDetail.JobDataMap["source"].ToString();
            string sourceName = sourceDir.Split('\\').Last();
            _hashBasedFolderCheck = new HashBasedFolderCheck(sourceDir);
            if(BackupStateManager.ReadFolderHash(_hashBasedFolderCheck) !=null) _hashBasedFolderCheck = BackupStateManager.ReadFolderHash(_hashBasedFolderCheck);
            string backupDir;
            try
            {
                if (_hashBasedFolderCheck.ChangedState)
                {
                    backupDir = $@"{backupDestinationDir}\Backup{sourceName}{_hashBasedFolderCheck.BackupCount.ToString()}";
                    BackupFolder(sourceDir, backupDir);
                    _hashBasedFolderCheck.BackupCount++;
                    _hashBasedFolderCheck.UpdateHash();
                    log.InfoFormat("Backup{0}{1} - Hash={2}", sourceName, _hashBasedFolderCheck.BackupCount - 1, _hashBasedFolderCheck.CurrentHash);

                    if(_hashBasedFolderCheck.BackupCount >= backupNum)
                    {
                        DeleteOldestBackup(_hashBasedFolderCheck.SourceName);
                    }
                }
                else
                {
                    log.InfoFormat("No backup {0} - Hash={1}", sourceName, _hashBasedFolderCheck.CurrentHash);
                }
            }
            catch
            {
                log.ErrorFormat("Error with the backup operation");
            }
        }
    }
}