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
        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();

        public CategoryPage()
        {
            this.InitializeComponent();
            _categoryVM = new CategoryViewModel();
        }
        {
            _authenticationVM = auth;
        }

        private async void InitializeCategories()
        {
            var res = await _authenticationVM.Client.GetAsync("http://hyphen-solutions.be/unipack/api/category");
            var stringRes = res.Content.ReadAsStringAsync().Result;
            var categories = JsonConvert.DeserializeObject<List<CategoryDto>>(stringRes);

            categories.ForEach(c => _categoryVM.AddCategory(new Category { Id = c.CategoryId, AddedOn = c.AddedOn, Name = c.Name, NumberOfItems = 0 }));
            CategoryGrid.DataContext = _categoryVM.categories;
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            CategoryAddContentDialog addContentDialog = new CategoryAddContentDialog(_authenticationVM, _categoryVM);

            await addContentDialog.ShowAsync();
            if (!addContentDialog.Success)
                return;
            Categories = _categoryVM.categories;
        }

        public async void DeleteCategory(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var cat = (Category)button.DataContext;

            _categoryVM.DeleteCategory(cat.Id);
            await _authenticationVM.Client.DeleteAsync("http://hyphen-solutions.be/unipack/api/category/" + cat.Id);
        }

        public async void EditCategory(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var cat = (Category)button.DataContext;

            CategoryEditContentDialog editContentDialog = new CategoryEditContentDialog(_authenticationVM, _categoryVM, cat);

            await editContentDialog.ShowAsync();
            if (!editContentDialog.Success)
                return;
            Categories = _categoryVM.categories;

        }

        public void Clear()
        {
            _categoryVM.Clear();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            _authenticationVM = (AuthenticationViewModel)e.Parameter;
            InitializeCategories();
        }

        public void CategoryGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CategoryGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            Category category = (Category) e.ClickedItem;
            DetailParameters param = new DetailParameters(_authenticationVM, category);

            page.Navigate(typeof(CategoryDetailPage), param);
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            Categories = _categoryVM.categories;
            CategoryGrid.DataContext = new ObservableCollection<Category>(Categories.Where(c => c.Name.ToLower().Contains(SearchBar.Text.ToLower())));
        }
    }
}

public class DetailParameters
{

    public AuthenticationViewModel _authenticationVM { get; set; }
    public Category _category { get; set; }

    public DetailParameters(AuthenticationViewModel AVM, Category cat)
    {
        _authenticationVM = AVM;
        _category = cat;
    }
}

