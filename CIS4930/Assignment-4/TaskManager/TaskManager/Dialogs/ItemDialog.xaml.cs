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
        private IList<Item> _items;
        public ItemDialog(IList<Item> Items)
        {
            this.InitializeComponent();
            DataContext = new DialogViewModel(null);
            _items = Items;
        }

        public ItemDialog(Item selectedItem, IList<Item> Items)
        {
            this.InitializeComponent();
            DataContext = new DialogViewModel(selectedItem);
            _items = Items;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var context = DataContext as DialogViewModel;
            if (context.BoundTask != null)
            {
                var task = context.BoundTask;
                if (task.Id <= 0)
                {
                    _items.Add(task);
                    //Debug.WriteLine("Task");
                }
            }
            else if (context.BoundAppointment != null)
            {
                var appointment = context.BoundAppointment;
                if (appointment.Id <= 0)
                {
                    _items.Add(appointment);
                    //Debug.WriteLine("Apointment");
                }
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
