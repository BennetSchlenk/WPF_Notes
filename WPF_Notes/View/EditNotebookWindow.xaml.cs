using System.Windows;
using WPF_Notes.Model;
using WPF_Notes.ViewModel;

namespace WPF_Notes.View
{
    /// <summary>
    /// Interaction logic for EditNotebookWindow.xaml
    /// </summary>
    public partial class EditNotebookWindow : Window
    {
        public EditNotebookWindow(Notebook notebook)
        {
            InitializeComponent();
            DataContext = new EditNotebookVM(notebook);
        }
    }
}
