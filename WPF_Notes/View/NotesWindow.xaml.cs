using System;
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
using WPF_Notes.Model;
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
            RichTextBox box = RichTextBox;
            var vm = (NotesVM)box.DataContext;
            vm.SetStatusBarRichTextBoxTextCommand.Execute(box);
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

        private void RichTexBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            RichTextBox box = RichTextBox;
            var vm = (NotesVM)box.DataContext;
            vm.SetRichtextboxFontToggleButtonsCommand.Execute(box);

        }

        private void NoteListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RichTextBox box = RichTextBox;
            var vm = (NotesVM)box.DataContext;

            if (e.AddedItems.Count == 0) return;

            Note newNote = e.AddedItems[0] as Note;
            Note? oldNote = (e.RemovedItems.Count == 0) ? null : e.RemovedItems[0] as Note;

            //if(oldNote != null) 
            //{
            //    MessageBox.Show($"new: {newNote.Titel} // old: {oldNote.Titel}");
            //}
            //else 
            //{
            //    MessageBox.Show($"new: {newNote.Titel} // old: None was previously selected");
            //}

            NoteSelectionChangedCommandWrapper objectWrapper = new NoteSelectionChangedCommandWrapper()
            {
                RichTextBox = box,
                oldValue = oldNote,
                newValue = newNote
            };

            vm.EvaluateSelectedNoteChangeCommand.Execute(objectWrapper);
        }
    }
}
