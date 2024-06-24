using System.Windows;
using WPF_Notes.Model;
using WPF_Notes.ViewModel;

namespace WPF_Notes.View
{
    /// <summary>
    /// Interaction logic for CreateNoteWindow.xaml
    /// </summary>
    public partial class CreateNoteWindow : Window
    {
        public CreateNoteWindow(Notebook notebook)
        {
            InitializeComponent();
            DataContext = new CreateNoteVM(notebook);
        }
    }
}
