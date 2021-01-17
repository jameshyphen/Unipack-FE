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
    public sealed partial class CategoryPage : Page
    {
        private AuthenticationViewModel _authenticationVM;
        private CategoryViewModel _categoryVM;

        public CategoryPage()
        {
            this.InitializeComponent();
            _categoryVM = new CategoryViewModel();
        }
        public CategoryPage(AuthenticationViewModel auth) : this()
        {
            _authenticationVM = auth;
            _categoryVM.SetAuthVM(_authenticationVM);

        }

        private void InitializeCategories()
        {
            _categoryVM.InitializeCategories();
            Refresh();
        }

        private void Refresh()
        {
            CategoryGrid.DataContext = _categoryVM.categories;
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            CategoryAddContentDialog addContentDialog = new CategoryAddContentDialog(_categoryVM);

            await addContentDialog.ShowAsync();
            if (!addContentDialog.Success)
                return;
            Refresh();
        }

        public void DeleteCategory(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var cat = (Category)button.DataContext;

            _categoryVM.DeleteCategory(cat.Id);
            Refresh();
        }

        public async void EditCategory(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var cat = (Category)button.DataContext;

            CategoryEditContentDialog editContentDialog = new CategoryEditContentDialog(_categoryVM, cat);

            await editContentDialog.ShowAsync();
            if (!editContentDialog.Success)
                return;
            Refresh();
        }

        public void Clear()
        {
            _categoryVM.Clear();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            _authenticationVM = (AuthenticationViewModel)e.Parameter;
            _categoryVM.SetAuthVM(_authenticationVM);
            InitializeCategories();
        }

        public void CategoryGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void CategoryGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            Category category = (Category) e.ClickedItem;
            CategoryDetailParameters param = new CategoryDetailParameters(_authenticationVM, category);

            page.Navigate(typeof(CategoryDetailPage), param);
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            CategoryGrid.DataContext = new ObservableCollection<Category>(_categoryVM.categories.Where(c => c.Name.ToLower().Contains(SearchBar.Text.ToLower())));
        }
    }
}

public class CategoryDetailParameters
{

    public AuthenticationViewModel _authenticationVM { get; set; }
    public Category _category { get; set; }

    public CategoryDetailParameters(AuthenticationViewModel AVM, Category cat)
    {
        _authenticationVM = AVM;
        _category = cat;
    }
}

