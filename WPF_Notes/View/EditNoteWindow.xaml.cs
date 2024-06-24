using System.Windows;
using WPF_Notes.Model;
using WPF_Notes.ViewModel;

namespace WPF_Notes.View
{
    /// <summary>
    /// Interaction logic for EditNoteWindow.xaml
    /// </summary>
    public partial class EditNoteWindow : Window
    {
        public EditNoteWindow(Note note)
        {
            InitializeComponent();
            DataContext = new EditNoteVM(note);
        }
    }
}
