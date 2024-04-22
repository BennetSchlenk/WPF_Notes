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
            ContextGrid.DataContext = new EditNotebookVM(notebook);
        }
    }
}
