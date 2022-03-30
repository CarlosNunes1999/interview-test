using System;
using Microsoft.EntityFrameworkCore;
using InterviewApp.Models;

namespace InterviewApp.Services
{
    public class DataContext : DbContext
    {
        #region Properties
        public readonly string _file;
        public DbSet<Item> Items => Set<Item>();
        public DbSet<Account> Accounts => Set<Account>();
        #endregion

        /// <summary>
        /// Constructor for DataContext
        /// </summary>
        /// <param name="file"></param>
        public DataContext(string file)
        {
            if (string.IsNullOrWhiteSpace(file))
                throw new ArgumentNullException(nameof(file));

            _file = file;
        }

        #region Methods
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_file}");
        } 
        #endregion
    }
}
