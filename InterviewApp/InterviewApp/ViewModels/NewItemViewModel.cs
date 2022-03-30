using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.Graphics;
using InterviewApp.Models;
using MvvmHelpers;
using Plugin.Media;
using Xamarin.Forms;

namespace InterviewApp.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {

        
        #region Properties
        private string _text = "";
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value, onChanged: SaveCommand.ChangeCanExecute);
        }

        private string _description = "";
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value, onChanged: SaveCommand.ChangeCanExecute);
        }
        public string? _imagePath = "";
        public string? imagePath
        {

            get => _imagePath;
            set => SetProperty(ref _imagePath, value);
        }

        public Command UploadImageCommand { get; }
        public Command SaveCommand { get; }
        public Command CancelCommand { get; } 
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public NewItemViewModel()
        {
            SaveCommand   = new Command(Save, ValidateSave);
            CancelCommand = new Command(Cancel);
            UploadImageCommand = new Command(UploadImage);
        }

        #region Methods
        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(_text) && !string.IsNullOrWhiteSpace(_description);
        }

        private void Cancel() => CancelAsync().SafeFireAndForget();
        private void UploadImage() => UploadImageAsync().SafeFireAndForget();

        private void Save() => SaveAsync().SafeFireAndForget();

        private async Task CancelAsync()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async Task UploadImageAsync()
        {
            // This will pop the current page off the navigation stack
            await CrossMedia.Current.Initialize();
            var imageFile = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full,
                CompressionQuality = 60
            }
            );
            if(imageFile != null)imagePath = imageFile.Path;
            
      
            
        }

        private async Task SaveAsync()
        {
            Item newItem = new Item()
            {
                Id = Guid.NewGuid(),
                Text = Text,
                Description = Description,
                imagePath = imagePath
            };

            await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        } 
        #endregion
    }
}
