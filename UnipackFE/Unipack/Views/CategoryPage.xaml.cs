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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Unipack.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CategoryPage : Page
    {
        private AuthenticationViewModel authenticationVM;

        public CategoryPage()
        {
            this.InitializeComponent();

            ObservableCollection<Category> dataList = new ObservableCollection<Category>();

            Category c1 = new Category() {AddedOn = DateTime.Now, Name = "Tech", NumberOfItems = 12};
            Category c2 = new Category() {AddedOn = DateTime.Now, Name = "Books", NumberOfItems = 31 };
            Category c3 = new Category() {AddedOn = DateTime.Now, Name = "Food", NumberOfItems = 15 };

            dataList.Add(c1);
            dataList.Add(c2);
            dataList.Add(c3);

            ListViewCategories.ItemsSource = dataList;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            authenticationVM = (AuthenticationViewModel)e.Parameter;
        }
    }
}

