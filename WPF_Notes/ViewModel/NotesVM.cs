using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
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

        private bool boldToggleButtonState;

        public bool BoldToggleButtonState
        {
            get { return boldToggleButtonState; }
            set
            {
                boldToggleButtonState = value;
                OnPropertyChanged("BoldToggleButtonState");
            }
        }

        private bool italicToggleButtonState;

        public bool ItalicToggleButtonState
        {
            get { return italicToggleButtonState; }
            set
            {
                italicToggleButtonState = value;
                OnPropertyChanged("ItalicToggleButtonState");
            }
        }

        private bool underlineToggleButtonState;

        public bool UnderlineToggleButtonState
        {
            get { return underlineToggleButtonState; }
            set
            {
                underlineToggleButtonState = value;
                OnPropertyChanged("UnderlineToggleButtonState");
            }
        }

        private IEnumerable fontComboBoxItemSource;

        public IEnumerable FontComboBoxItemSource
        {
            get { return fontComboBoxItemSource; }
            set 
            { 
                fontComboBoxItemSource = value;
                OnPropertyChanged("FontComboBoxItemSource");
            }
        }

        private object fontComboBoxSelectedItem;

        public object FontComboBoxSelectedItem
        {
            get { return fontComboBoxSelectedItem; }
            set 
            { 
                fontComboBoxSelectedItem = value;
                OnPropertyChanged("FontComboBoxSelectedValue");
            }
        }


        private IEnumerable fontSizeComboBoxItemSource;

        public IEnumerable FontSizeComboBoxItemSource
        {
            get { return fontSizeComboBoxItemSource; }
            set 
            { 
                fontSizeComboBoxItemSource = value;
                OnPropertyChanged("FontSizeComboBoxItemSource");
            }
        }

        private object fontSizeComboBoxSelectedItem;

        public object FontSizeComboBoxSelectedItem
        {
            get { return fontSizeComboBoxSelectedItem; }
            set 
            {
                fontSizeComboBoxSelectedItem = value;
                OnPropertyChanged("FontSizeComboBoxSelectedValue");
            }

        }


        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }
        public ExitApplicationCommand ExitApplicationCommand { get; set; }
        public SpeechToTextToolbarCommand SpeechToTextToolbarCommand { get; set; }
        public RichTextBoxTextChangedCommand RichTextBoxTextChangedCommand { get; set; }
        public RichTextBoxSelectionChangedCommand RichTextBoxSelectionChangedCommand { get; set; }
        public BoldTextToolbarToggleCommand BoldTextToolbarToggleCommand { get; set; }
        public ItalicTextToolbarToggleCommand ItalicTextToolbarToggleCommand { get; set; }
        public UnderlineTextToolbarToggleCommand UnderlineTextToolbarToggleCommand { get; set; }
        public FontSizeComboBoxTextChangedCommand FontSizeComboBoxTextChangedCommand { get; set; }
        public FontComboBoxSelectionChangedCommand FontComboBoxSelectionChangedCommand { get; set; }


        public event PropertyChangedEventHandler? PropertyChanged;

        public NotesVM()
        {
            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
            ExitApplicationCommand = new ExitApplicationCommand(this);
            RichTextBoxTextChangedCommand = new RichTextBoxTextChangedCommand(this);
            RichTextBoxSelectionChangedCommand = new RichTextBoxSelectionChangedCommand(this);
            BoldTextToolbarToggleCommand = new BoldTextToolbarToggleCommand(this);
            ItalicTextToolbarToggleCommand = new ItalicTextToolbarToggleCommand(this);
            UnderlineTextToolbarToggleCommand = new UnderlineTextToolbarToggleCommand(this);
            FontSizeComboBoxTextChangedCommand = new FontSizeComboBoxTextChangedCommand(this);
            FontComboBoxSelectionChangedCommand = new FontComboBoxSelectionChangedCommand(this);

            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();
            GetNotebooks();

            var fontFamilies = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            FontComboBoxItemSource = fontFamilies;


            List<double> fontSizes = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            FontSizeComboBoxItemSource = fontSizes;
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetStatusBarText(RichTextBox box) 
        {
            int charactersAmount = (new TextRange(box.Document.ContentStart, box.Document.ContentEnd)).Text.Length;
            StatusBarText = $"Note document length: {charactersAmount} characters";
        }

        public void SetRichtextboxFontToggleButtons(RichTextBox box) 
        {
            var selectedWeight = box.Selection.GetPropertyValue(TextElement.FontWeightProperty);
            var selectedStyle = box.Selection.GetPropertyValue(TextElement.FontStyleProperty);
            var selectedDecorations = box.Selection.GetPropertyValue(Inline.TextDecorationsProperty);

            BoldToggleButtonState = (selectedWeight != DependencyProperty.UnsetValue) && selectedWeight.Equals(FontWeights.Bold);
            ItalicToggleButtonState = (selectedStyle != DependencyProperty.UnsetValue) && selectedStyle.Equals(FontStyles.Italic);
            UnderlineToggleButtonState = (selectedDecorations != DependencyProperty.UnsetValue) && selectedDecorations.Equals(TextDecorations.Underline);

            FontComboBoxSelectedItem = box.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            FontSizeComboBoxSelectedItem = box.Selection.GetPropertyValue(TextElement.FontSizeProperty);
        }

        public void BoldToggleClicked(RichTextBox box) 
        {
            if (BoldToggleButtonState) 
            {
                box.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
            }
            else 
            {
                box.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);
            }
        }

        public void ItalicToggleClicked(RichTextBox box)
        {
            if (ItalicToggleButtonState)
            {
                box.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
            }
            else
            {
                box.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);
            }
        }

        public void UnderlineToggleClicked(RichTextBox box)
        {
            if (UnderlineToggleButtonState)
            {
                box.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
            else
            {
                box.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
            }
        }

        public void FontSizeComboBoxTextChanged(RichTextBox box) 
        {
            if(FontSizeComboBoxSelectedItem == null) return;
            box.Selection.ApplyPropertyValue(Inline.FontSizeProperty, FontSizeComboBoxSelectedItem);
        }

        public void FontComboBoxSelectionChanged(RichTextBox box) 
        {
            if (FontComboBoxSelectedItem == null) return;
            box.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, FontComboBoxSelectedItem);
        }
    }
}
