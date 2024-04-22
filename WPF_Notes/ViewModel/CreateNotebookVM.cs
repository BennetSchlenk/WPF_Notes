using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Notes.Model;
using WPF_Notes.ViewModel.Helpers;

namespace WPF_Notes.ViewModel
{
    internal partial class CreateNotebookVM : ObservableObject
    {
        [ObservableProperty]
        private string? notebookNameText;

        [ObservableProperty]
        private string? notebookColorText;

        [RelayCommand]
        private void CreateNewNotebook(Window window)
        {
            if (NotebookColorText == null || NotebookNameText == null) return;
            Notebook newNotebook = new Notebook()
            {
                Name = NotebookNameText,
                Color = NotebookColorText
            };

            DatabaseHelper.Insert(newNotebook);

            window.Close();
        }
    }
}
