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

        private async void InitializeCategoryDetails(Category cat)
        {
            _categoryDVM = new CategoryDetailViewModel() { category = cat };
            header.Text = _categoryDVM.category.Name;
            var res = await _authenticationVM.Client.GetAsync("http://hyphen-solutions.be/unipack/api/category/"+_categoryDVM.category.Id+"/items");
            var stringRes = res.Content.ReadAsStringAsync().Result;
            var items = JsonConvert.DeserializeObject<CategoryDto>(stringRes);

            items.Items.ToList().ForEach(c => _categoryDVM.AddItem(new Item {AddedOn = c.AddedOn, ItemId = c.ItemId, Name = c.Name, Priority = (Priority) c.Priority}));
            ItemGrid.DataContext = _categoryDVM.items;
        }

        private async void BtnAddNew_Click(object sender, RoutedEventArgs e)
        {
            CategoryDetailAddNewContentDialog addContentDialog = new CategoryDetailAddNewContentDialog(_authenticationVM, _categoryDVM);

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
            DetailParameters param = (DetailParameters)e.Parameter;
            _authenticationVM = param._authenticationVM;
            InitializeCategoryDetails(param._category);
        }
    }
}
