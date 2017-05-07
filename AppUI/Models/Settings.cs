using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AppUI.Models
{
    public class Settings : INotifyPropertyChanged
    {
        #region fields
        private string _numOfSources;
        private string _numOfBackups;
        private string _backupDestinationDir;
        private List<string> _sourceDirs = new List<string>();
        private List<string> _schedulers = new List<string>();
        private bool _continueBackupOnError;
        #endregion

        public Settings()
        {
        }

        #region properties
        public string NumOfSources
        {
            get
            {
                return _numOfSources;
            }
            set
            {
                _numOfSources = value;
                OnPropertyChanged("NumOfSources");
            }
        }

        public string NumOfBackups
        {
            get
            {
                return _numOfBackups;
            }
            set
            {
                _numOfBackups = value;
                OnPropertyChanged("NumOfBackups");
            }
        }

        public string BackupDestinationDir
        {
            get
            {
                return _backupDestinationDir;
            }
            set
            {
                _backupDestinationDir = value;
                OnPropertyChanged("BackupDestinationDir");
            }
        }

        public string SourceDir1
        {
            get
            {
                return _sourceDirs[0];
            }
            set
            {
                if (_sourceDirs.ElementAtOrDefault(0) != null)
                {
                    _sourceDirs.RemoveAt(0);
                }
                _sourceDirs.Insert(0, value);
                OnPropertyChanged("SourceDir1");
            }
        }

        public string SourceDir2
        {
            get
            {
                return _sourceDirs[1];
            }
            set
            {
                if(_sourceDirs.ElementAtOrDefault(1) != null)
                {
                    _sourceDirs.RemoveAt(1);
                }
                _sourceDirs.Insert(1, value);
                OnPropertyChanged("SourceDir2");
            }
        }

        public string SourceDir3
        {
            get
            {
                return _sourceDirs[2];
            }
            set
            {
                if (_sourceDirs.ElementAtOrDefault(2) != null)
                {
                    _sourceDirs.RemoveAt(2);
                }
                _sourceDirs.Insert(2, value);
                OnPropertyChanged("SourceDir3");
            }
        }

        public string Scheduler1
        {
            get
            {
                return _schedulers[0];
            }
            set
            {
                if (_schedulers.ElementAtOrDefault(0) != null)
                {
                    _schedulers.RemoveAt(0);
                }
                _schedulers.Insert(0, value);
                OnPropertyChanged("Scheduler1");
            }
        }

        public string Scheduler2
        {
            get
            {
                return _schedulers[1];
            }
            set
            {
                if (_schedulers.ElementAtOrDefault(1) != null)
                {
                    _schedulers.RemoveAt(1);
                }
                _schedulers.Insert(1, value);
                OnPropertyChanged("Scheduler2");
            }
        }

        public string Scheduler3
        {
            get
            {
                return _schedulers[2];
            }
            set
            {
                if (_schedulers.ElementAtOrDefault(2) != null)
                {
                    _schedulers.RemoveAt(2);
                }
                _schedulers.Insert(2, value);
                OnPropertyChanged("Scheduler3");
            }
        }

        public bool IsContinueBackupOnError
        {
            get
            {
                return _continueBackupOnError;
            }
            set
            {
                _continueBackupOnError = value;
            }
        }

        public string ContinueBackupOnError
        {
            get
            {
                return _continueBackupOnError == true ? "Yes" : "No";
            }
            set
            {
                _continueBackupOnError = value == "Yes" ? true : false;
                OnPropertyChanged("ContinueBackupOnError");
            }
        }
        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

    }
}
