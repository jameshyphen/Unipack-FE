using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unipack.Models;
using Unipack.ViewModels;
using Unipack.Views.Dialogs;
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
            _categoryDVM = new CategoryDetailViewModel() {category = cat};
            //TODO hier api call doen voor ophalen
            header.Text = _categoryDVM.category.Name;
            ItemGrid.DataContext = _categoryDVM.items;
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
        }

        public void ItemGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ItemGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            Category category = (Category)e.ClickedItem;
            DetailParameters param = new DetailParameters(_authenticationVM, category);

            page.Navigate(typeof(CategoryDetailPage), param);
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
