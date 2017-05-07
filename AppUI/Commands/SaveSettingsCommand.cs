using AppUI.ViewModels;
using System;
using System.Windows.Input;

namespace AppUI.Commands
{
    class SaveSettingsCommand : ICommand
    {
        private SettingsViewModel _viewModel;

        public SaveSettingsCommand(SettingsViewModel viewModel)
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
            _viewModel.SaveChanges();
        }
    }
}
