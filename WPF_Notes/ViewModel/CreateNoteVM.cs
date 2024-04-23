﻿using CommunityToolkit.Mvvm.ComponentModel;
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
    internal partial class CreateNoteVM : ObservableObject
    {
        [ObservableProperty]
        private string? noteTitel;

        private Notebook selectedNotebook;

        public CreateNoteVM()
        {
            
        }
        public CreateNoteVM(Notebook notebook)
        {
            selectedNotebook = notebook;
        }

        [RelayCommand]
        private void CreateNewNote(Window window) 
        {
            if (NoteTitel == null) return;
            Note newNote = new Note()
            {
                NotebookId = selectedNotebook.Id,
                Titel = NoteTitel,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            DatabaseHelper.Insert(newNote);

            window.Close();
        }
    }
}
