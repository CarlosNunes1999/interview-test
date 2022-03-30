using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using InterviewApp.Models;
using InterviewApp.Interfaces;

namespace InterviewApp.Services
{
    public class SqliteDataStore : IDataStore<Item,Account>
    {


        #region Properties
        private readonly string _file;

        #endregion

        /// <summary>
        /// Constructor SqliteDataStore
        /// </summary>
        public SqliteDataStore()
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
                Directory.CreateDirectory(libFolder);

            _file = Path.Combine(libFolder, "datastore.db3");

            SQLitePCL.Batteries.Init();
        }

        #region Methods

        /// <summary>
        /// Esure that the database exists, if not creates the database
        /// </summary>
        /// <returns></returns>
        private async Task<DataContext> GetContextAsync()
        {
            DataContext db = new DataContext(_file);

            await db.Database.EnsureCreatedAsync();

            return db;
        }

        /// <summary>
        /// Returns a list of all the Items in the table
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            DataContext? db = null;
            try
            {
                db = await GetContextAsync();

                return db.Items.ToList();
            }
            finally
            {
                db?.Dispose();
            }
        }
        /// <summary>
        /// Search for the item with the id on the parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Item?> GetItemAsync(Guid id)
        {
            DataContext? db = null;
            try
            {
                db = await GetContextAsync();

                Item? item = await db.Items.FindAsync(id);
                return item;
            }
            finally
            {
                db?.Dispose();
            }
        }
        /// <summary>
        /// Add an item on the database
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> AddItemAsync(Item item)
        {
            DataContext? db = null;
            try
            {
                db = await GetContextAsync();

                db.Items.Add(item);
                await db.SaveChangesAsync();

                return true;
            }
            finally
            {
                db?.Dispose();
            }
        }
        /// <summary>
        /// Update an item on the database
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> UpdateItemAsync(Item item)
        {
            DataContext? db = null;
            try
            {
                db = await GetContextAsync();

                db.Items.Update(item);
                await db.SaveChangesAsync();

                return true;
            }
            finally
            {
                db?.Dispose();
            }
        }
        /// <summary>
        /// Delete an item on the database 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteItemAsync(Guid id)
        {
            DataContext? db = null;
            try
            {
                db = await GetContextAsync();

                Item? item = await db.Items.FindAsync(id);
                if (item == null)
                    return false;

                db.Items.Remove(item);
                await db.SaveChangesAsync();

                return true;
            }
            finally
            {
                db?.Dispose();
            }
        } 
        #endregion

        #region New Methods
        /// <summary>
        /// Get account with te username and password received
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<List<Account>?> GetAccountAsync(string username,string password)
        {
            DataContext? db = null;
            try
            {
                db = await GetContextAsync();

                return  db.Accounts.Where(user => user.Username == username && user.Password == password).ToList();
            }
            finally
            {
                db.Dispose();
            }
        }
        /// <summary>
        /// Add an account on the table Accounts
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<bool> AddAccountAsync(Account account)
        {
            DataContext? db = null;
            try
            {
                db = await GetContextAsync();

                db.Accounts.Add(account);
                await db.SaveChangesAsync();

                return true;
            }
            finally
            {
                db?.Dispose();
            }
        }
        /// <summary>
        /// Retreives all accounts on the database
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Account>> GetAccountsAsync(bool forceRefresh = false)
        {
            DataContext? db = null;
            try
            {
                db = await GetContextAsync();

                return db.Accounts.ToList();
            }
            finally
            {
                db?.Dispose();
            }
        }
        #endregion
    }
}
