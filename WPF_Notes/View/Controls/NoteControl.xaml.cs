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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Notes.Model;

namespace WPF_Notes.View.Controls
{
    /// <summary>
    /// Interaction logic for NoteControl.xaml
    /// </summary>
    public partial class NoteControl : UserControl
    {
        public Note Note
        {
            get { return (Note)GetValue(NoteProperty); }
            set { SetValue(NoteProperty, value); }
        }

        public static readonly DependencyProperty NoteProperty =
            DependencyProperty.Register("Note", typeof(Note), typeof(NoteControl), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NoteControl noteControl = (NoteControl)d;

            if (noteControl == null) return;
            noteControl.DataContext = noteControl.Note;
        }

        public ICommand EditNoteCommand
        {
            get { return (ICommand)GetValue(EditNoteCommandProperty); }
            set { SetValue(EditNoteCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EditCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditNoteCommandProperty =
            DependencyProperty.Register("EditNoteCommand", typeof(ICommand), typeof(NoteControl), new PropertyMetadata(null));


        public NoteControl()
        {
            InitializeComponent();
            EditNoteButton.Command = EditNoteCommand;
        }

        private void EditNoteButton_Click(object sender, RoutedEventArgs e)
        {
            Button but = (Button)sender;
            EditNoteCommand.Execute(but.DataContext);
        }
    }
}
