using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Unipack.Views.Dialogs.ItemDetail
{
    public sealed partial class ItemDetailAddNewContentDialog : ContentDialog
    {
        public AuthenticationViewModel _authVM { get; set; }
        public ItemDetailViewModel _catDVM { get; set; }
        public bool Success { get; set; }

        public ItemDetailAddNewContentDialog(AuthenticationViewModel authVM, ItemDetailViewModel catDVM)
        {
            _authVM = authVM;
            _catDVM = catDVM;
            Success = false;
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
