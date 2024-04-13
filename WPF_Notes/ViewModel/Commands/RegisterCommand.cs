using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF_Notes.ViewModel.Commands
{
    internal class RegisterCommand : ICommand
    {
        public LoginVM LoginVM { get; set; }
        public event EventHandler? CanExecuteChanged;

        public RegisterCommand(LoginVM vm) 
        { 
            LoginVM = vm;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            //TODO: Register method
        }
    }
}
