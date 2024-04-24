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
        private string? notebookName;

        [ObservableProperty]
        private string? notebookColor;

        [RelayCommand]
        private void CreateNewNotebook(Window window)
        {
            if (NotebookColor == null || NotebookName == null) return;
            Notebook newNotebook = new Notebook()
            {
                Name = NotebookName,
                CreatedAt = DateTime.Now,
                Color = NotebookColor
            };

            DatabaseHelper.Insert(newNotebook);

            window.Close();
        }

        [RelayCommand]
        private void CloseWindow(Window window) 
        {
            window.Close();
        }
    }
}
