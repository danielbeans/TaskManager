using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TaskManager.Dialogs
{
    public sealed partial class ItemDialog : ContentDialog
    {
        private IList<ItemViewModel> _items;
        public ItemDialog(IList<ItemViewModel> Items)
        {
            this.InitializeComponent();
            DataContext = new DialogViewModel(null);
            _items = Items;
        }

        public ItemDialog(ItemViewModel selectedItem, IList<ItemViewModel> Items)
        {
            this.InitializeComponent();
            DataContext = new DialogViewModel(selectedItem);
            _items = Items;
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var context = DataContext as DialogViewModel;
            if (context.BoundTask != null)
            {
                var task = context.BoundTask;
                if (task.Item.Id <= 0)
                {
                    var newTaskString = await new WebRequestHandler().Post("http://localhost/TaskManagerAPI/task/add", task.Item);
                    var newTask = JsonConvert.DeserializeObject<Task>(newTaskString);
                    _items.Add(new TaskViewModel(newTask));
                }
                else
                {
                    var res = await new WebRequestHandler().Post("http://localhost/TaskManagerAPI/task/update", task.Item);
                    Debug.WriteLine(res);
                }
            }
            else if (context.BoundAppointment != null)
            {
                var appointment = context.BoundAppointment;
                if (appointment.Item.Id <= 0)
                {
                    var newAppString = await new WebRequestHandler().Post("http://localhost/TaskManagerAPI/appointment/add", appointment.Item);
                    var newApp = JsonConvert.DeserializeObject<Task>(newAppString);
                    _items.Add(appointment);
                }
                else
                {
                    var res = await new WebRequestHandler().Post("http://localhost/TaskManagerAPI/appointment/update", appointment.Item);
                    Debug.WriteLine(res);
                }
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
