﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_Notes.ViewModel;

namespace WPF_Notes.View
{
    /// <summary>
    /// Interaction logic for NotesWindow.xaml
    /// </summary> 
    public partial class NotesWindow : Window
    {
        public NotesWindow()
        {
            InitializeComponent();
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RichTextBox box = (RichTextBox)sender;
            var vm = (NotesVM)box.DataContext;
            vm.RichTextBoxTextChangedCommand.Execute(box);
        }

        private void RichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            RichTextBox box = (RichTextBox)sender;
            var vm = (NotesVM)box.DataContext;
            vm.RichTextBoxSelectionChangedCommand.Execute(box);
            

        }

        private void ComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RichTextBox box = RichTextBox;
            var vm = (NotesVM)box.DataContext;
            vm.FontSizeComboBoxTextChangedCommand.Execute(box);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RichTextBox box = RichTextBox;
            var vm = (NotesVM)box.DataContext;
            vm.FontComboBoxSelectionChangedCommand.Execute(box);
        }
    }
}
