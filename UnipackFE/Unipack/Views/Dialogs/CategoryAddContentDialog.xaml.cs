using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Unipack.Models;
using Unipack.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Unipack.Views.Dialogs
{
    public sealed partial class CategoryAddContentDialog : ContentDialog
    {
        public AuthenticationViewModel _authVM { get; set; }
        public CategoryViewModel _catVM { get; set; }
        public bool Success { get; set; }
        public CategoryAddContentDialog(AuthenticationViewModel authVM, CategoryViewModel catVM)
        {
            _authVM = authVM;
            _catVM = catVM;
            Success = false;
            this.InitializeComponent();
        }

        public string GetCategoryName()
        {
            return TxtCategoryName.Text;
        }

        public async Task Create()
        {
            try
            {
                if (!Validate())
                    return;
                HttpClient client = new HttpClient();
                var category = new Category { Name = GetCategoryName(), AddedOn = DateTime.Now, NumberOfItems = 0};
                var categoryJson = JsonConvert.SerializeObject(category);
                //TODO hier api call
                _catVM.AddCategory(category);
                Success = true;
                Hide();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong: {e}");
                this.TxtBottomError.Text = e.Message;
            }
        }


        public bool Validate()
        {
            string result = "";
            bool success = true;

            if (GetCategoryName().Length == 0)
            {
                this.TxtCategoryName.Foreground = new SolidColorBrush(Colors.Red);
                result += "Name is required.\n";
                success = false;
            }

            if (result.Length > 0)
            {
                this.TxtBottomError.Text = result;
            }
            return success;
        }

        private async void BtnAdd_OnClick(object sender, RoutedEventArgs e)
        {
            this.TxtCategoryName.Foreground = new SolidColorBrush(Colors.Black);
            this.TxtBottomError.Text = "";
            await Create();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
