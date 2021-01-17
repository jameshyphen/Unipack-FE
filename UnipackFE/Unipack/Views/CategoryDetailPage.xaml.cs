using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unipack.DTOs;
using Unipack.Enums;
using Unipack.Models;
using Unipack.ViewModels;
using Unipack.Views.Dialogs;
using Unipack.Views.Dialogs.CategoryDetail;
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
    public sealed partial class CategoryDetailPage : Page
    {
        private AuthenticationViewModel _authenticationVM;
        private CategoryDetailViewModel _categoryDVM;
        public CategoryDetailPage()
        {
            this.InitializeComponent();
        }

        private void InitializeCategoryDetails(Category cat)
        {
            _categoryDVM = new CategoryDetailViewModel(cat, _authenticationVM);
            header.Text = _categoryDVM.Category.Name;
            Refresh();
        }

        private void Refresh()
        {
            ItemGrid.DataContext = _categoryDVM.Items;
        }

        private async void BtnAddNew_Click(object sender, RoutedEventArgs e)
        {
            CategoryDetailAddNewContentDialog addContentDialog = new CategoryDetailAddNewContentDialog(_categoryDVM);

            await addContentDialog.ShowAsync();
            if (!addContentDialog.Success)
                return;
            Refresh();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var item = (Item)button.DataContext;

            _categoryDVM.DeleteItem(item);

            Refresh();
        }

        public void ItemGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void ItemGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            Item item = (Item)e.ClickedItem;

            page.Navigate(typeof(ItemPage), _authenticationVM);
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            ItemGrid.DataContext = new ObservableCollection<Item>(_categoryDVM.Items.Where(c => c.Name.ToLower().Contains(SearchBar.Text.ToLower())));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            CategoryDetailParameters param = (CategoryDetailParameters)e.Parameter;
            _authenticationVM = param._authenticationVM;
            InitializeCategoryDetails(param._category);
        }
    }
}
