using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Unipack.Models;
using Unipack.ViewModels;
using System.Net.Http;
using Unipack.Views.Dialogs;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using Unipack.DTOs;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Unipack.Views
{
    public sealed partial class ItemPage : Page
    {
        private AuthenticationViewModel _authenticationVM;
        private ItemViewModel _ItemVM;
        public ObservableCollection<Item> items { get; set; } = new ObservableCollection<Item>();

        public ItemPage()
        {
            this.InitializeComponent();
            _ItemVM = new ItemViewModel();
        }
        public ItemPage(AuthenticationViewModel auth) : this()
        {
            _authenticationVM = auth;
        }

        private async void Initializeitems()
        {
            var res = await _authenticationVM.Client.GetAsync("http://hyphen-solutions.be/unipack/api/item");
            var stringRes = res.Content.ReadAsStringAsync().Result;
            var items = JsonConvert.DeserializeObject<List<ItemDto>>(stringRes);

            items.ForEach(c => _ItemVM.AddItem(new Item { ItemId = c.ItemId, AddedOn = c.AddedOn, Name = c.Name, Category = new Category { AddedOn = c.AddedOn, Id = c.CategoryId, Name = c.Name } }));
            ItemGrid.DataContext = _ItemVM.items;
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            ItemAddContentDialog addContentDialog = new ItemAddContentDialog(_authenticationVM, _ItemVM);

            await addContentDialog.ShowAsync();
            if (!addContentDialog.Success)
                return;
            items = _ItemVM.items;
        }

        public async void DeleteItem(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var cat = (Item)button.DataContext;

            _ItemVM.DeleteItem(cat.ItemId);
            await _authenticationVM.Client.DeleteAsync("http://hyphen-solutions.be/unipack/api/item/" + cat.ItemId);
        }

        public async void EditItem(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var cat = (Item)button.DataContext;

            ItemEditContentDialog editContentDialog = new ItemEditContentDialog(_authenticationVM, _ItemVM, cat);

            await editContentDialog.ShowAsync();
            if (!editContentDialog.Success)
                return;
            items = _ItemVM.items;

        }

        public void Clear()
        {
            _ItemVM.Clear();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            _authenticationVM = (AuthenticationViewModel)e.Parameter;
            Initializeitems();
        }

        public void ItemGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ItemGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            Item Item = (Item)e.ClickedItem;
            ItemDetailParameters param = new ItemDetailParameters(_authenticationVM, Item);

            page.Navigate(typeof(ItemDetailPage), param);
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            items = _ItemVM.items;
            ItemGrid.DataContext = new ObservableCollection<Item>(items.Where(c => c.Name.ToLower().Contains(SearchBar.Text.ToLower())));
        }
    }
}

public class ItemDetailParameters
{

    public AuthenticationViewModel _authenticationVM { get; set; }
    public Item _Item { get; set; }

    public ItemDetailParameters(AuthenticationViewModel AVM, Item cat)
    {
        _authenticationVM = AVM;
        _Item = cat;
    }
}

