﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using InterviewApp.Models;
using MvvmHelpers;
using Plugin.Media;
using Xamarin.Forms;

namespace InterviewApp.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {

        // Properties
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

        string imagedir = "";

        public Command UploadBtn { get; }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        // Ctors
        public NewItemViewModel()
        {
            SaveCommand   = new Command(Save, ValidateSave);
            CancelCommand = new Command(Cancel);
            UploadBtn = new Command(Upload);
        }

        // Methods
        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(_text) && !string.IsNullOrWhiteSpace(_description);
        }

        private void Cancel() => CancelAsync().SafeFireAndForget();
        private void Upload() => UploadAsync().SafeFireAndForget();

        private void Save() => SaveAsync().SafeFireAndForget();

        private async Task CancelAsync()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async Task UploadAsync()
        {
            // This will pop the current page off the navigation stack
            await CrossMedia.Current.Initialize();
            await CrossMedia.Current.PickPhotoAsync();
        }

        private async Task SaveAsync()
        {
            Item newItem = new Item()
            {
                Id = Guid.NewGuid(),
                Text = Text,
                Description = Description
            };

            await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
