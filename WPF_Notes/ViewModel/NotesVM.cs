using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Notes.Model;
using WPF_Notes.ViewModel.Commands;

namespace WPF_Notes.ViewModel
{
    internal class NotesVM
    {
        public ObservableCollection<Notebook> Notebooks { get; set; }
        public ObservableCollection<Note> Notes { get; set; }

        private Notebook selectedNotebook;

		public Notebook SelectedNotebook
        {
			get { return selectedNotebook; }
			set 
			{ 
				selectedNotebook = value; 
				//TODO: Get Notes in NoteBook
			}
		}

        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }

        public NotesVM()
        {
            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
        }

    }
}
