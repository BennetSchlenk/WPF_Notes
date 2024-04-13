using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Notes.Model;
using WPF_Notes.ViewModel.Commands;
using WPF_Notes.ViewModel.Helpers;

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
        public ExitApplicationCommand ExitApplicationCommand { get; set; }

        public NotesVM()
        {
            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
            ExitApplicationCommand = new ExitApplicationCommand(this);
        }

        public void CreateNewNotebook() 
        {
            Notebook newNotebook = new Notebook()
            {
                Name = "New Notebook"
            };

            DatabaseHelper.Insert(newNotebook);
        }

        public void CreateNewNote(int notebookId) 
        {
            Note newNote = new Note() 
            {
                NotebookId = notebookId,
                Titel = "New Note",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            DatabaseHelper.Insert(newNote);
        }

    }
}
