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
using Unipack.Models.Commands;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Unipack.Views
{
    public sealed partial class CategoryPage : Page
    {
        private AuthenticationViewModel _authenticationVM;
        private CategoryViewModel _categoryVM;
        public ObservableCollection<Category> categories { get; set; } = new ObservableCollection<Category>();
        private DeleteCategoryCommand DeleteCommand { get; set; }

        public CategoryPage()
        {
            this.InitializeComponent();
            _categoryVM = new CategoryViewModel();
            InitializeCategories();
            DeleteCommand = new DeleteCategoryCommand(_categoryVM);

        }

        private void InitializeCategories()
        {
            //TODO hier api call doen voor alle categoriën te adden & demo data verwijderen
            _categoryVM.AddCategory(new Category() { AddedOn = DateTime.Now, Name = "Tech", NumberOfItems = 12 });
            _categoryVM.AddCategory(new Category() { AddedOn = DateTime.Now, Name = "Books", NumberOfItems = 31 });
            _categoryVM.AddCategory(new Category() { AddedOn = DateTime.Now, Name = "Food", NumberOfItems = 15 });

            categories = _categoryVM.categories;
            CategoryGrid.DataContext = categories;
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            CategoryAddContentDialog addContentDialog = new CategoryAddContentDialog(_authenticationVM, _categoryVM);

            await addContentDialog.ShowAsync();
            if (!addContentDialog.Success)
                return;
            CategoryGrid.DataContext = _categoryVM.categories;
        }
        public void DeleteCategory(string cat)
        {
            _categoryVM.DeleteCategory(cat);
            //TODO hier pai call doen voor categorie te verwijderen

        }

        public void Clear()
        {
            _categoryVM.Clear();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            _authenticationVM = (AuthenticationViewModel)e.Parameter;
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
            categories = _categoryVM.categories;
            CategoryGrid.DataContext = new ObservableCollection<Category>(categories.Where(c => c.Name.ToLower().Contains(SearchBar.Text.ToLower())));
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

