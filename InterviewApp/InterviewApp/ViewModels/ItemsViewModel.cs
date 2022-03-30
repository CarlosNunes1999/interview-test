using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using InterviewApp.Models;
using InterviewApp.Views;
using MvvmHelpers;
using Xamarin.Forms;

namespace InterviewApp.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
       
        #region Properties
        public ObservableCollection<Item> Items { get; }

        private Item? _selectedItem;
        public Item? SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                SelectItem(value);
            }
        }

        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command SelectItemCommand { get; }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();

            LoadItemsCommand = new Command(LoadItems);
            AddItemCommand = new Command(AddItem);
            SelectItemCommand = new Command<Item?>(SelectItem);
        }

        #region Methods
        private void LoadItems() => LoadItemsAsync().SafeFireAndForget();
        /// <summary>
        /// Load all items
        /// </summary>
        /// <returns></returns>
        private async Task LoadItemsAsync()
        {
            IsBusy = true;

            try
            {
                Items.Clear();

                IEnumerable<Item> items = await DataStore.GetItemsAsync(true);
                foreach (Item item in items)
                    Items.Add(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        /// <summary>
        /// Add an item on db
        /// </summary>
        private void AddItem()
        {
            Shell.Current.GoToAsync(nameof(NewItemPage)).SafeFireAndForget();
        }
        /// <summary>
        /// Select an item
        /// </summary>
        /// <param name="item"></param>
        private void SelectItem(Item? item)
        {
            if (item == default)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}").SafeFireAndForget();
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        } 
        #endregion
    }
}