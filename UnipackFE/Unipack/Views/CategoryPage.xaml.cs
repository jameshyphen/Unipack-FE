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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Unipack.Views
{
    public sealed partial class CategoryPage : Page
    {
        private AuthenticationViewModel _authenticationVM;
        private CategoryViewModel _categoryVM;
        public RelayCommand<string> DeleteCommand;

        public CategoryPage()
        {
            this.InitializeComponent();
            _categoryVM = new CategoryViewModel();
            InitializeCategories();
            CategoryGrid.DataContext = _categoryVM.categories;
            DeleteCommand = new RelayCommand<string>((param) => DeleteCategory(param));
        }

        public CategoryPage(AuthenticationViewModel auth)
        {
            this.InitializeComponent();
            _categoryVM = new CategoryViewModel();
            InitializeCategories();
            CategoryGrid.DataContext = _categoryVM.categories;
            _authenticationVM = auth;
        }

        private void InitializeCategories()
        {
            //TODO hier api call doen voor alle categoriën te adden & demo data verwijderen
            _categoryVM.AddCategory(new Category() { AddedOn = DateTime.Now, Name = "Tech", NumberOfItems = 12 });
            _categoryVM.AddCategory(new Category() { AddedOn = DateTime.Now, Name = "Books", NumberOfItems = 31 });
            _categoryVM.AddCategory(new Category() { AddedOn = DateTime.Now, Name = "Food", NumberOfItems = 15 });
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            CategoryAddContentDialog addContentDialog = new CategoryAddContentDialog(_authenticationVM, _categoryVM);

            await addContentDialog.ShowAsync();
            if (!addContentDialog.Success)
                return;
        }
        public void DeleteCategory(string cat)
        {
            _categoryVM.DeleteCategory(GetCategoryByName(cat));
            //TODO hier pai call doen voor categorie te verwijderen
        }
        private Category GetCategoryByName(string name)
        {
            try
            {
                return _categoryVM.categories.First(x => x.Name.Equals(name));
            }
            catch
            {
                return null;
            }
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
            //TODO doe hier iets mee
        }
    }
}