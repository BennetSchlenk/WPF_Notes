﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF_Notes.ViewModel.Commands
{
    internal class FontComboBoxSelectionChangedCommand
    {
        public NotesVM NotesVM { get; set; }
        public event EventHandler? CanExecuteChanged;
        public FontComboBoxSelectionChangedCommand(NotesVM vm)
        {
            NotesVM = vm;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            RichTextBox box = parameter as RichTextBox;
            if (box == null) return;

            NotesVM.FontComboBoxSelectionChanged(box);
        }
    }
}
