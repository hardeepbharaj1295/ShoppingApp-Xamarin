using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using App1.Resources.Model;
using SQLite;

namespace App1.Resources.Helper
{
    class Database
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public bool createDatabase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Persons.db")))
                {
                    connection.CreateTable<Person>();
                    connection.CreateTable<Products>();
                    connection.CreateTable<History>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
        public bool insertIntoTable(Person person)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Persons.db")))
                {
                    connection.Insert(person);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool insertProducts(Products product)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Persons.db")))
                {
                    connection.Insert(product);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool loginIntoTable(string email, string password)
        {
            try
            {
                var db = new SQLiteConnection(System.IO.Path.Combine(folder, "Persons.db"));
                var data = db.Table<Person>();

                var data1 = data.Where(x => x.Email == email && x.Password == password).FirstOrDefault();

                if (data1 != null)
                {
                    return true;
                }
                else
                {
                    Log.Info("SQLite Login Wrong : ", "Wrong Password");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Info("SQLite Login : ", ex.Message);
                return false;
            }
        }

        public List<Products> selectTable()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Persons.db")))
                {
                    return connection.Table<Products>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }

        public bool buyProduct(int id, History history)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Persons.db")))
                {
                    connection.Query<Products>("UPDATE Products set ProductQuantity=ProductQuantity-1 Where Id=?", id);
                    connection.Insert(history);
                    return true;

                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLite Buy", ex.Message);
                return false;
            }
        }

        public List<History> selectUserData(string email)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Persons.db")))
                {
                    return connection.Query<History>("SELECT * FROM History where UserEmail = ?", email).ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }

        public bool deleteProduct(int id, History history)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Persons.db")))
                {
                    connection.Query<Products>("UPDATE Products set ProductQuantity=ProductQuantity+1 Where Id=?", id);
                    connection.Delete(history);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public int selectCount()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Persons.db")))
                {
                    return connection.Table<Products>().Count();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return -1;
            }
        }
    }
}