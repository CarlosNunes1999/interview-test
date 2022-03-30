using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using InterviewApp.Models;

namespace InterviewApp.Interfaces
{
    public interface IDataStore<T,A>
    {
        #region Items
        Task<bool> AddItemAsync(T item);

        Task<bool> UpdateItemAsync(T item);

        Task<bool> DeleteItemAsync(Guid id);

        Task<T?> GetItemAsync(Guid id);

        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        #endregion

        #region Accounts
        Task<List<Account>> GetAccountAsync(string username, string password);

        Task<bool> AddAccountAsync(A account);

        Task<IEnumerable<A>> GetAccountsAsync(bool forceRefresh = false);
        
        #endregion


    }
}
