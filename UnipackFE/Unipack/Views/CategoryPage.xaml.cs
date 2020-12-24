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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Unipack.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CategoryPage : Page
    {
        private AuthenticationViewModel authenticationVM;
        public ObservableCollection<Category> categories { get; set; }

        public CategoryPage()
        {
            this.InitializeComponent();

            categories = new ObservableCollection<Category>();

            AddCategory(new Category() {AddedOn = DateTime.Now, Name = "Tech", NumberOfItems = 12});
            AddCategory(new Category() {AddedOn = DateTime.Now, Name = "Books", NumberOfItems = 31 });
            AddCategory(new Category() {AddedOn = DateTime.Now, Name = "Food", NumberOfItems = 15 });
        }

        public void AddCategory(Category cat)
        {
            categories.Add(cat);
        }

        public void Clear()
        {
            categories.Clear();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            authenticationVM = (AuthenticationViewModel)e.Parameter;
        }
    }
}

