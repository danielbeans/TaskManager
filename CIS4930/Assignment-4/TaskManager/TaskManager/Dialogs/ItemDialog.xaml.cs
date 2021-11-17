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

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var context = DataContext as DialogViewModel;
            if (context.BoundTask != null)
            {
                var task = context.BoundTask;
                if (task.Item.Id <= 0)
                {
                    task.Item.Id = Item.s_currentID;
                    Item.s_currentID++;
                    _items.Add(task);
                }
            }
            else if (context.BoundAppointment != null)
            {
                var appointment = context.BoundAppointment;
                if (appointment.Item.Id <= 0)
                {
                    appointment.Item.Id = Item.s_currentID;
                    Item.s_currentID++;
                    _items.Add(appointment);
                }
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
