using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Input;

namespace WPF_Notes.ViewModel.Commands
{
    class UnderlineTextToolbarToggleCommand : ICommand
    {
        public NotesVM NotesVM { get; set; }
        public event EventHandler? CanExecuteChanged;
        public UnderlineTextToolbarToggleCommand(NotesVM vm)
        {
            NotesVM = vm;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            RichTextBox box = parameter as RichTextBox;
            if (box == null) return;

            NotesVM.UnderlineToggleClicked(box);
        }
    }
}
