using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Notes.Model;
using WPF_Notes.View;
using WPF_Notes.ViewModel.Helpers;

namespace WPF_Notes.ViewModel
{
    internal partial class EditNotebookVM : ObservableObject
    {
        [ObservableProperty]
        private string notebookName;

        [ObservableProperty]
        private string notebookColor;

        private Notebook selectedNoteBook;
        public EditNotebookVM()
        {

        }
        public EditNotebookVM(Notebook notebook)
        {
            selectedNoteBook = notebook;
            notebookName = selectedNoteBook.Name;
            notebookColor = selectedNoteBook.Color;
        }

        [RelayCommand]
        private void EditNotebook(Window window)
        {
            EditNotebookWindow editWindow = (EditNotebookWindow)window;

            selectedNoteBook.Name = NotebookName;
            selectedNoteBook.Color = NotebookColor;

            DatabaseHelper.Update(selectedNoteBook);


            window.Close();
        }

        [RelayCommand]
        private void DeleteNotebook(Window window)
        {
            var allNotesOfSelectedNotebook = DatabaseHelper.Read<Note>().Where(n => n.NotebookId == selectedNoteBook.Id).ToList();
            var notes = allNotesOfSelectedNotebook;

            foreach (var note in notes)
            {
                if (!string.IsNullOrEmpty(note.FileLocation)) 
                {
                    File.Delete(note.FileLocation);
                }
                DatabaseHelper.Delete(note);
            }

            DatabaseHelper.Delete(selectedNoteBook);

            window.Close();
        }

        [RelayCommand]
        private void CloseWindow(Window window)
        {
            window.Close();
        }
    }
}
