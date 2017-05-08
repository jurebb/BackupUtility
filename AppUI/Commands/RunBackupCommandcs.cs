using AppUI.ViewModels;
using System;
using System.Windows.Input;

namespace AppUI.Commands
{
    class RunBackupCommand : ICommand
    {
        private SettingsViewModel _viewModel;
        public static bool RunBackupButtonEnabled = true; 

        public RunBackupCommand(SettingsViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return RunBackupButtonEnabled;
        }

        public void Execute(object parameter)
        {
            RunBackupButtonEnabled = false;
            _viewModel.RunBackupUtility();
        }
    }
}
