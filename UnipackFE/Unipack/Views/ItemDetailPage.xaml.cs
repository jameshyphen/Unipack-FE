using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unipack.DTOs;
using Unipack.Enums;
using Unipack.Models;
using Unipack.ViewModels;
using Unipack.Views.Dialogs;
using Unipack.Views.Dialogs.ItemDetail;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Unipack.Views
{
    public sealed partial class ItemDetailPage : Page
    {
        private AuthenticationViewModel _authenticationVM;
        private ItemDetailViewModel _ItemDVM;
        public ItemDetailPage()
        {
            this.InitializeComponent();
        }

        private async void InitializeItemDetails(Item cat)
        {
            _ItemDVM = new ItemDetailViewModel() { Item = cat };
            header.Text = _ItemDVM.Item.Name;
            ItemGrid.DataContext = _ItemDVM.items;
        }

        private async void BtnAddNew_Click(object sender, RoutedEventArgs e)
        {
            ItemDetailAddNewContentDialog addContentDialog = new ItemDetailAddNewContentDialog(_authenticationVM, _ItemDVM);

            await addContentDialog.ShowAsync();
            if (!addContentDialog.Success)
                return;
        }

        public void ItemGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void ItemGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            Item item = (Item)e.ClickedItem;

            page.Navigate(typeof(ItemPage), _authenticationVM);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ItemDetailParameters param = (ItemDetailParameters)e.Parameter;
            _authenticationVM = param._authenticationVM;
            InitializeItemDetails(param._Item);
        }
    }
}
