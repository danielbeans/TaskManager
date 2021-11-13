using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TaskManager.Dialogs;
using TaskManager.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace TaskManager
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public MainPage()
        {
            this.InitializeComponent();

            if(File.Exists(MainViewModel.PersistencePath))
            {
                DataContext = JsonConvert.DeserializeObject<MainViewModel>(File.ReadAllText(MainViewModel.PersistencePath), MainViewModel.Settings);
            }
            else
            {
                DataContext = new MainViewModel();
            }
        }

        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            var itemDialog = new ItemDialog((DataContext as MainViewModel).Items);
            await itemDialog.ShowAsync();
            (DataContext as MainViewModel).RefreshList();
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            var dataContext = DataContext as MainViewModel;
            var itemDialog = new ItemDialog(dataContext.SelectedItem, dataContext.Items);
            await itemDialog.ShowAsync();
            (DataContext as MainViewModel).RefreshList();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).RemoveItem();
            (DataContext as MainViewModel).RefreshList();
        }

        private void Search(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            var dataContext = DataContext as MainViewModel;
            dataContext.Query = args.QueryText;
            dataContext.RefreshList();
        }

        //private void Search_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        //{
        //    if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
        //    {
        //        sender.ItemsSource = (DataContext as MainViewModel).FilteredItems;
        //    }
        //}

        private void Save(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).SaveState();
        }

        private void Sort(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).Sort();
        }
    }
}
