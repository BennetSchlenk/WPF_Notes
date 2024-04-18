using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using WPF_Notes.Model;
using WPF_Notes.ViewModel.Commands;
using WPF_Notes.ViewModel.Helpers;

namespace WPF_Notes.ViewModel
{
    internal class NotesVM : INotifyPropertyChanged
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
                OnPropertyChanged(nameof(SelectedNotebook));
                GetNotes();
			}
		}

        private string statusBarText;

        public string StatusBarText
        {
            get { return statusBarText; }
            set
            {
                statusBarText = value;
                OnPropertyChanged("StatusBarText");
            }
        }

        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }
        public ExitApplicationCommand ExitApplicationCommand { get; set; }
        public SpeechToTextToolbarCommand SpeechToTextToolbarCommand { get; set; }
        public RichTextBoxTextChangedCommand RichTextBoxTextChangedCommand { get; set; }
        public BoldTextToolbarCommand BoldTextToolbarCommand { get; set; }
        public ItalicTextToolbarCommand ItalicTextToolbarCommand { get; set; }
        public UnderlineTextToolbarCommand UnderlineTextToolbarCommand { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public NotesVM()
        {
            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
            ExitApplicationCommand = new ExitApplicationCommand(this);
            RichTextBoxTextChangedCommand = new RichTextBoxTextChangedCommand(this);
            BoldTextToolbarCommand = new BoldTextToolbarCommand(this);
            ItalicTextToolbarCommand = new ItalicTextToolbarCommand(this);
            UnderlineTextToolbarCommand = new UnderlineTextToolbarCommand(this);

            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();
            GetNotebooks();
        }

        public void CreateNewNotebook() 
        {
            Notebook newNotebook = new Notebook()
            {
                Name = "New Notebook"
            };

            DatabaseHelper.Insert(newNotebook);
            GetNotebooks();
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
            GetNotes();
        }

        private void GetNotebooks() 
        {
            var notebooks = DatabaseHelper.Read<Notebook>();
            Notebooks.Clear();

            foreach (var notebook in notebooks)
            {
                Notebooks.Add(notebook);
            }
        }

        private void GetNotes()
        {
            if (SelectedNotebook == null) return;

            var notes = DatabaseHelper.Read<Note>().Where(n => n.NotebookId == SelectedNotebook.Id).ToList();
            Notes.Clear();

            foreach (var note in notes)
            {
                Notes.Add(note);
            }
        }

        private void OnPropertyChanged(string propertyName) 
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetStatusBarText(RichTextBox box) 
        {
            int charactersAmount = (new TextRange(box.Document.ContentStart, box.Document.ContentEnd)).Text.Length;
            StatusBarText = $"Note document length: {charactersAmount} characters";
        }
    }
}
