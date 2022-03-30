using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using InterviewApp.Models;
using InterviewApp.Interfaces;

namespace InterviewApp.Services
{
    public class MockDataStore : IDataStore<Item,Account>
    {
        #region Properties

        private readonly List<Item> _items;
        private readonly List<Account> _accounts;
        private readonly List<Account> ResultsAccount;

        #endregion

        /// <summary>
        /// Constructor MockDataStore
        /// </summary>
        public MockDataStore()
        {
            _items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid(), Text = "First item",  Description="This is an item description."},
                new Item { Id = Guid.NewGuid(), Text = "Second item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid(), Text = "Third item",  Description="This is an item description." },
                new Item { Id = Guid.NewGuid(), Text = "Fourth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid(), Text = "Fifth item",  Description="This is an item description." },
                new Item { Id = Guid.NewGuid(), Text = "Sixth item",  Description="This is an item description." }
            };

            _accounts = new List<Account>()
            {
                new Account {Id = Guid.NewGuid(), Username = "FirstUser", Password="Password"}
            };
            ResultsAccount = new List<Account>();
        }
        #region Methods

        /// <summary>
        /// Add an Item 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> AddItemAsync(Item item)
        {
            _items.Add(item);

            return await Task.FromResult(true);
        }
        /// <summary>
        /// Updates and item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = _items.Where(i => i.Id.Equals(item.Id)).FirstOrDefault();
            _items.Remove(oldItem);
            _items.Add(item);

            return await Task.FromResult(true);
        }
        /// <summary>
        /// Deletes an item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteItemAsync(Guid id)
        {
            var oldItem = _items.Where(i => i.Id.Equals(id)).FirstOrDefault();
            _items.Remove(oldItem);

            return await Task.FromResult(true);
        }
        /// <summary>
        /// Gets and item with certain id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Item?> GetItemAsync(Guid id)
        {
            return await Task.FromResult(_items.FirstOrDefault(i => i.Id.Equals(id)));
        }
        /// <summary>
        /// Retreives a list of all items
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(_items);
        }

        #region New Methods
        /// <summary>
        /// Retreives a account with certain username and password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<List<Account>> GetAccountAsync(string username, string password)
        {
            
            return await Task.FromResult(_accounts.Where(user => user.Username == username && user.Password == password).ToList());
        }
        /// <summary>
        /// Get a list of all accounts on the table
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Account>> GetAccountsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(ResultsAccount);
        }
        /// <summary>
        /// Add an account on the table
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<bool> AddAccountAsync(Account account)
        {
            _accounts.Add(account);

            return await Task.FromResult(true);
        }
        #endregion 

        #endregion
    }
}