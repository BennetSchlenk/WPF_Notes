using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Notes.ViewModel.Helpers
{
    internal class DatabaseHelper
    {
        private static string dbFile = Path.Combine(Environment.CurrentDirectory,"WPF_Notes.db3");

        public static List<T> Read<T>() where T: new()
        {
            List<T> resultItems;

            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>();
                resultItems = connection.Table<T>().ToList();    
            }

            return resultItems;
        }

        public static bool Insert<T>(T item) 
        {
            bool result = false;

            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>();
                int rowsInserted = connection.Insert(item);
                if(rowsInserted > 0) 
                {
                    result = true;  
                }
            }

                return result;
        }

        public static bool Update<T>(T item)
        {
            bool result = false;

            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>();
                int rowsUpdated = connection.Update(item);
                if (rowsUpdated > 0)
                {
                    result = true;
                }
            }

            return result;
        }

        public static bool Delete<T>(T item)
        {
            bool result = false;

            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>();
                int rowsDeleted = connection.Delete(item);
                if (rowsDeleted > 0)
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
