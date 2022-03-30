using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using MvvmHelpers;
using InterviewApp.Models;
using Android.Graphics;

namespace InterviewApp.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        #region Properties
        public Guid? Id { get; set; }

        private string? _text;
        public string? Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private string? _description;
        public string? Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }


        private string? _itemId;
        public string? ItemId
        {
            get => _itemId;
            set => SetProperty(ref _itemId, value, onChanged: () => LoadAsync(Guid.TryParse(_itemId, out Guid guid) ? guid : Guid.Empty).SafeFireAndForget());
        }
        public string? _imagePath = "";
        public string? imagePath
        {
            get => _imagePath;
            set => SetProperty(ref _imagePath, value);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Loads all the Items on the database to the page
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public async Task LoadAsync(Guid itemId)
        {
            try
            {
                if (itemId.Equals(Guid.Empty))
                    return;

                Item? item = await DataStore.GetItemAsync(itemId);

                Id = item?.Id;
                Text = item?.Text;
                Description = item?.Description;
                imagePath = item?.imagePath;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        } 
        #endregion
    }
}
