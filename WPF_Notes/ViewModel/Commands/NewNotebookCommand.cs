using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF_Notes.ViewModel.Commands
{
    internal class NewNotebookCommand : ICommand
    {
        public NotesVM NotesVM { get; set; }
        public event EventHandler? CanExecuteChanged;
        public NewNotebookCommand(NotesVM vm)
        { 
            NotesVM = vm;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            //TODO: Create new Notebook
        }
    }
}
