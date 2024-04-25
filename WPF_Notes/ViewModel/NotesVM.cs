﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using WPF_Notes.Model;
using WPF_Notes.View;
using WPF_Notes.ViewModel.Commands;
using WPF_Notes.ViewModel.Helpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WPF_Notes.ViewModel
{
    internal partial class NotesVM : ObservableObject
    {
        public ObservableCollection<Notebook> Notebooks { get; set; }
        public ObservableCollection<Note> Notes { get; set; }



        [ObservableProperty, NotifyCanExecuteChangedFor(nameof(OpenNewNoteWindowCommand))]
        private Notebook selectedNotebook;
        partial void OnSelectedNotebookChanged(Notebook value)
        {
            if (SelectedNotebook == null)
            {
                if (Notebooks.Count > 0)
                {
                    SelectedNotebook = Notebooks.OrderBy(p => p.CreatedAt).Reverse().First();
                    GetNotes();
                }
            }
            else
            {
                GetNotes();
                DarkerColor = LightenDarkenColor(SelectedNotebook.Color,0.9f);
            }


        }

        [ObservableProperty]
        private string darkerColor;

        [ObservableProperty]
        private Note selectedNote;

        [ObservableProperty]
        private string statusBarNotebooksText;

        [ObservableProperty]
        private string statusBarNotesText;

        [ObservableProperty]
        private string statusBarRichTextBoxText;

        [ObservableProperty]
        private bool boldToggleButtonState;

        [ObservableProperty]
        private bool italicToggleButtonState;

        [ObservableProperty]
        private bool underlineToggleButtonState;

        [ObservableProperty]
        private IEnumerable fontComboBoxItemSource;

        [ObservableProperty]
        private object fontComboBoxSelectedItem;

        [ObservableProperty]
        private IEnumerable fontSizeComboBoxItemSource;

        [ObservableProperty]
        private object fontSizeComboBoxSelectedItem;


        public event PropertyChangedEventHandler? PropertyChanged;

        public NotesVM()
        {
            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();
            GetNotebooks();

            if (Notebooks.Count > 0)
            {
                SelectedNotebook = Notebooks.OrderBy(p => p.CreatedAt).Reverse().First();
            }


            var fontFamilies = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            FontComboBoxItemSource = fontFamilies;


            List<double> fontSizes = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            FontSizeComboBoxItemSource = fontSizes; ;
        }

        [RelayCommand]
        private void OpenNewNotebookWindow()
        {
            CreateNotebookWindow detailWindow = new CreateNotebookWindow();
            detailWindow.ShowDialog();

            GetNotebooks();
            SelectedNotebook = Notebooks.OrderBy(p => p.CreatedAt).Reverse().First();
        }

        [RelayCommand]
        private void OpenEditNotebookWindow(Notebook notebook)
        {
            SelectedNotebook = notebook;
            EditNotebookWindow editWindow = new EditNotebookWindow(SelectedNotebook);
            editWindow.ShowDialog();

            GetNotebooks();
            if (Notebooks.Contains(notebook))
            {
                SelectedNotebook = notebook;
            }
            else
            {
                if (Notebooks.Count > 0)
                {
                    SelectedNotebook = Notebooks.OrderBy(p => p.CreatedAt).Reverse().First();
                }
            }
        }

        private bool CanExecuteNewNoteWindow()
        {
            return SelectedNotebook != null;
        }

        [RelayCommand(CanExecute = nameof(CanExecuteNewNoteWindow))]
        private void OpenNewNoteWindow()
        {
            CreateNoteWindow detailWindow = new CreateNoteWindow(SelectedNotebook);
            detailWindow.ShowDialog();

            GetNotes();
            if(Notes.Count > 0)
                SelectedNote = Notes.OrderBy(p => p.CreatedAt).Reverse().First();
        }

        [RelayCommand]
        private void OpenEditNoteWindow(Note note)
        {
            SelectedNote = note;
            EditNoteWindow editWindow = new EditNoteWindow(SelectedNote);
            editWindow.ShowDialog();

            GetNotes();

            if (Notes.Contains(note))
            {
                SelectedNote = note;
            }
            else
            {
                if (Notes.Count > 0)
                {
                    SelectedNote = Notes.OrderBy(p => p.CreatedAt).Reverse().First();
                }
            }
        }

        [RelayCommand]
        private void ExitApplication()
        {
            Application.Current.Shutdown();
        }

        private void GetNotebooks()
        {
            var notebooks = DatabaseHelper.Read<Notebook>();
            Notebooks.Clear();

            foreach (var notebook in notebooks)
            {
                Notebooks.Add(notebook);
            }

            SetStatusBarNotebooksText(notebooks.Count);
        }

        private void GetNotes()
        {
            if (SelectedNotebook == null) return;

            var allNotes = DatabaseHelper.Read<Note>();
            var notes = allNotes.Where(n => n.NotebookId == SelectedNotebook.Id).ToList();
            Notes.Clear();

            foreach (var note in notes)
            {
                Notes.Add(note);
            }

            SetStatusBarNotesText(notes.Count, allNotes.Count);

            if (Notes.Count > 0)
            {
                SelectedNote = Notes.OrderBy(p => p.CreatedAt).Reverse().First();
            }
            else
            {
                SelectedNote = null;
            }

        }

        [RelayCommand]
        private void EvaluateSelectedNoteChange(NoteSelectionChangedCommandWrapper wrapper)
        {
            if (wrapper.oldValue == null)
            {
                //MessageBox.Show($"new: {wrapper.newValue.Titel}   old: No Note was Selected   selectedNote:{SelectedNote.Titel}");
                LoadNoteText(wrapper.newValue, wrapper.RichTextBox);
            }
            else
            {
                //MessageBox.Show($"new: {wrapper.newValue.Titel} old: {wrapper.oldValue.Titel}    selectedNote:{SelectedNote.Titel}");
                SaveNoteText(wrapper.oldValue, wrapper.RichTextBox);
                LoadNoteText(wrapper.newValue, wrapper.RichTextBox);
            }

        }

        private void LoadNoteText(Note note, RichTextBox box)
        {
            box.Document.Blocks.Clear();
            if (!string.IsNullOrEmpty(note.FileLocation) && note == SelectedNote)
            {
                FileStream fileStream = new FileStream(note.FileLocation, FileMode.Open);
                var content = new TextRange(box.Document.ContentStart, box.Document.ContentEnd);

                content.Load(fileStream, DataFormats.Rtf);
                fileStream.Close();
            }
        }

        private void SaveNoteText(Note note, RichTextBox box)
        {
            string folderPath = Environment.CurrentDirectory + "/NoteFolder/";
            string rtfFile = System.IO.Path.Combine(folderPath, $"{note.Id}.rtf");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            note.FileLocation = rtfFile;
            DatabaseHelper.Update(note);

            FileStream fileStream = new FileStream(rtfFile, FileMode.Create);

            var content = new TextRange(box.Document.ContentStart, box.Document.ContentEnd);

            content.Save(fileStream, DataFormats.Rtf);

            fileStream.Close();
        }


        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [RelayCommand]
        private void SetStatusBarRichTextBoxText(RichTextBox box)
        {
            int charactersAmount = (new TextRange(box.Document.ContentStart, box.Document.ContentEnd)).Text.Length;
            StatusBarRichTextBoxText = $"Note document length: {charactersAmount} characters";
        }

        private void SetStatusBarNotebooksText(int amountNotebooks)
        {
            StatusBarNotebooksText = $"{amountNotebooks} Notebooks";
        }

        private void SetStatusBarNotesText(int amountNotes, int totalAmountNotes)
        {
            StatusBarNotesText = $"{amountNotes} Notes / {totalAmountNotes} Notes Total";
        }

        [RelayCommand]
        private void SetRichtextboxFontToggleButtons(RichTextBox box)
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

        [RelayCommand]
        private void BoldToggleClicked(RichTextBox box)
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

        [RelayCommand]
        private void ItalicToggleClicked(RichTextBox box)
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

        [RelayCommand]
        private void UnderlineToggleClicked(RichTextBox box)
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

        [RelayCommand]
        private void FontSizeComboBoxTextChanged(RichTextBox box)
        {
            if (FontSizeComboBoxSelectedItem == null) return;
            box.Selection.ApplyPropertyValue(Inline.FontSizeProperty, FontSizeComboBoxSelectedItem);
        }

        [RelayCommand]
        private void FontComboBoxSelectionChanged(RichTextBox box)
        {
            if (FontComboBoxSelectedItem == null) return;
            box.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, FontComboBoxSelectedItem);
        }

        private string LightenDarkenColor(string col, float amt)
        {
            var color = ColorTranslator.FromHtml(col);

            int r = (int)(Convert.ToInt16(color.R) * amt);
            int g = (int)(Convert.ToInt16(color.G) *amt);
            int b = (int)(Convert.ToInt16(color.B) *amt);

            var hex = "#" + string.Format("{0:X2}{1:X2}{2:X2}", r, g, b);

            return hex;
        }
    }
}
