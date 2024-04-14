using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_Notes.Model;

namespace WPF_Notes.ViewModel.Commands
{
    internal class NewNoteCommand : ICommand
    {
        public NotesVM NotesVM { get; set; }
        public event EventHandler? CanExecuteChanged 
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public NewNoteCommand(NotesVM vm) 
        { 
            NotesVM = vm;
        }

        public bool CanExecute(object? parameter)
        {
            if(parameter as Notebook != null) 
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        public void Execute(object? parameter)
        {
            Notebook selectedNotebook = parameter as Notebook;

            if(selectedNotebook == null) 
            { 
                //TODO: Notify
                return; 
            }

            NotesVM.CreateNewNote(selectedNotebook.Id);
        }
    }
}
