using AppUI.ViewModels;
using System;
using System.Windows.Input;

namespace AppUI.Commands
{
    class RunBackupCommand : ICommand
    {
        private SettingsViewModel _viewModel;

        public RunBackupCommand(SettingsViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public bool CanExecute(object parameter)
        {
            return true;        //button je uvijek enablean by design
        }

        public void Execute(object parameter)
        {
            _viewModel.RunBackupUtility();
        }
    }
}
