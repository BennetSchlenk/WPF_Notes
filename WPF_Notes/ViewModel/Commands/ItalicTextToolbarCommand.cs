﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace WPF_Notes.ViewModel.Commands
{
    class ItalicTextToolbarCommand : ICommand
    {
        public NotesVM NotesVM { get; set; }
        public event EventHandler? CanExecuteChanged;
        public ItalicTextToolbarCommand(NotesVM vm)
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

            box.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
        }
    }
}
