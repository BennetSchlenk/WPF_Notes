using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.IO;
using System.Windows;
using WPF_Notes.Model;
using WPF_Notes.View;
using WPF_Notes.ViewModel.Helpers;

namespace WPF_Notes.ViewModel
{
    internal partial class EditNoteVM : ObservableObject
    {
        [ObservableProperty]
        private string noteTitel;

        private Note selectedNote;

        public EditNoteVM()
        {

        }

        public EditNoteVM(Note note)
        {
            selectedNote = note;
            noteTitel = selectedNote.Titel;
        }

        [RelayCommand]
        private void EditNote(Window window)
        {
            EditNoteWindow editWindow = (EditNoteWindow)window;

            selectedNote.Titel = NoteTitel;
            selectedNote.UpdatedAt = DateTime.Now;

            DatabaseHelper.Update(selectedNote);

            window.Close();
        }

        [RelayCommand]
        private void DeleteNote(Window window)
        {
            if (selectedNote.FileLocation != null)
            {
                File.Delete(selectedNote.FileLocation);
            }
            DatabaseHelper.Delete(selectedNote);

            window.Close();
        }

        [RelayCommand]
        private void CloseWindow(Window window)
        {
            window.Close();
        }
    }
}
