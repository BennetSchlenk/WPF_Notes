using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF_Notes.Model
{
    internal class NoteSelectionChangedCommandWrapper
    {
        public required RichTextBox RichTextBox { get; set; }
        public Note? oldValue { get; set; }
        public required Note newValue { get; set; }
    }
}
