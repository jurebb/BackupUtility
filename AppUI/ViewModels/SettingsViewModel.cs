using AppUI.Commands;
using AppUI.Models;
using Hardcodet.Wpf.TaskbarNotification;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AppUI.ViewModels
{
    class SettingsViewModel
    {
        Settings _settings;
        public ObservableCollection<string> NumOfSourcesEntries { get; private set; }
        public ObservableCollection<string> ContinueBackupOnErrorEntries { get; private set; }
        public BackupUtilityLib.Scheduler backupVar;
            
        public SettingsViewModel()
        {
            NumOfSourcesEntries = new ObservableCollection<string>
            {
                "1",
                "2",
                "3"
            };

            ContinueBackupOnErrorEntries = new ObservableCollection<string>
            {
                "Yes",
                "No"
            };

            _settings = new Settings()
            {
                NumOfSources = Properties.Settings.Default.numOfSources,
                NumOfBackups = Properties.Settings.Default.numOfBackups,
                BackupDestinationDir = Properties.Settings.Default.backupDestinationDir,
                SourceDir1 = Properties.Settings.Default.sourceDir1,
                SourceDir2 = Properties.Settings.Default.sourceDir2,
                SourceDir3 = Properties.Settings.Default.sourceDir3,
                Scheduler1 = Properties.Settings.Default.scheduler1,
                Scheduler2 = Properties.Settings.Default.scheduler2,
                Scheduler3 = Properties.Settings.Default.scheduler3,
                IsContinueBackupOnError = Properties.Settings.Default.continueBackupOnError
            };
            
            RunBackup = new RunBackupCommand(this);
        }

        #region properties
        public ICommand SaveCommand
        {
            get;
            private set;
        }

        public ICommand RunBackup
        {
            get;
            private set;
        }

        public Settings Settings
        {
            get
            {
                return _settings;
            }
        }
        #endregion

        public void SaveChanges()
        {
            if(backupVar != null)
            {
                backupVar.Shutdown();
            }

            Properties.Settings.Default.numOfSources = _settings.NumOfSources;
            Properties.Settings.Default.numOfBackups = _settings.NumOfBackups;
            Properties.Settings.Default.backupDestinationDir = _settings.BackupDestinationDir;
            Properties.Settings.Default.sourceDir1 = _settings.SourceDir1;
            Properties.Settings.Default.sourceDir2 = _settings.SourceDir2;
            Properties.Settings.Default.sourceDir3 = _settings.SourceDir3;
            Properties.Settings.Default.scheduler1 = _settings.Scheduler1;
            Properties.Settings.Default.scheduler2 = _settings.Scheduler2;
            Properties.Settings.Default.scheduler3 = _settings.Scheduler3;
            Properties.Settings.Default.continueBackupOnError = _settings.IsContinueBackupOnError;
            
            Properties.Settings.Default.Save();
        }

        public void RunBackupUtility()
        {
            backupVar = new BackupUtilityLib.Scheduler();
            backupVar.Test();
        }
    }
}
