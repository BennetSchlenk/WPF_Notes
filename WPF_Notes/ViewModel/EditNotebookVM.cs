using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
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

            selectedNoteBook.Name = notebookName;
            selectedNoteBook.Color = notebookColor;

            DatabaseHelper.Update(selectedNoteBook);


            window.Close();
        }
    }
}
