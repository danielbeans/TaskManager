using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TaskManager.Dialogs;
using TaskManager.Models;
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

            var mainViewModel = new MainViewModel();
            var taskString = new WebRequestHandler().Get("http://localhost/TaskManagerAPI/task").Result;
            var tasks = JsonConvert.DeserializeObject<List<Models.Task>>(taskString);
            tasks.ForEach(t => mainViewModel.Items.Add(new TaskViewModel(t)));
            var appointmentsString = new WebRequestHandler().Get("http://localhost/TaskManagerAPI/appointment").Result;
            var appointments = JsonConvert.DeserializeObject<List<Appointment>>(appointmentsString);
            appointments.ForEach(a => mainViewModel.Items.Add(new AppointmentViewModel(a)));

            DataContext = mainViewModel;
            (DataContext as MainViewModel).RefreshList();
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

            if(dataContext.SelectedItem != null)
            {
                var itemDialog = new ItemDialog(dataContext.SelectedItem, dataContext.Items);
                await itemDialog.ShowAsync();
                (DataContext as MainViewModel).RefreshList();
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).RemoveItem();
            (DataContext as MainViewModel).RefreshList();
        }

        private void SearchBox_TextChanged(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox != null)
            {
                var dataContext = DataContext as MainViewModel;
                dataContext.Query = textBox.Text;
                dataContext.RefreshList();
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).SaveState();
        }

        private void SortMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            var option = ((MenuFlyoutItem)sender).Tag.ToString();

            switch (option)
            {
                case "priority":
                    (DataContext as MainViewModel).SortByPriority();
                    break;
                default:
                    (DataContext as MainViewModel).SortOff();
                    break;
            }

        }
    }
}
