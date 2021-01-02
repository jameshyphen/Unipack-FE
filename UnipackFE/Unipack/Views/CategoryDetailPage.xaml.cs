using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unipack.Models;
using Unipack.ViewModels;
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
            //TODO hier api call doen voor ophalen
            _categoryDVM.AddItem(new Item());


            //ItemGrid.DataContext = _categoryDVM.items;
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
