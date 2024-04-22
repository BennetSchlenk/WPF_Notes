using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Notes.Model
{
    public class Notebook
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int UserId { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(7)]
        public string Color { get; set; }
    }
}
