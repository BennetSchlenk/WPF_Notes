using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace WPF_Notes.ViewModel.Commands
{
    internal class ExitApplicationCommand : ICommand
    {
        public NotesVM NotesVM { get; set; }
        public event EventHandler? CanExecuteChanged;
        public ExitApplicationCommand(NotesVM vm)
        {
            NotesVM = vm;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
