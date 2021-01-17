using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Unipack.DTOs;
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
    public sealed partial class ItemEditContentDialog : ContentDialog
    {
        public AuthenticationViewModel _authVM { get; set; }
        public ItemViewModel _catVM { get; set; }
        public bool Success { get; set; }
        public Item Current { get; set; }
        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();
        public ItemEditContentDialog(AuthenticationViewModel authVM, ItemViewModel catVM, Item cat)
        {
            _authVM = authVM;
            _catVM = catVM;
            Success = false;
            InitializeCategories();
            this.InitializeComponent();
            Current = cat;
            TxtItemName.Text = cat.Name;
        }
        public Category GetCategory()
        {
            return (Category)CmbCategory.SelectedItem;
        }
        public string GetItemName()
        {
            return Current.Name;
        }

        public async Task Edit()
        {
            try
            {
                if (!Validate())
                    return;
                var category = GetCategory();
                var Item = new ItemDto { Name = GetItemName(), AddedOn = Current.AddedOn, ItemId = Current.ItemId, CategoryId = category.Id};
                var ItemJson = JsonConvert.SerializeObject(Item);
                await _authVM.Client.PutAsync("http://hyphen-solutions.be/unipack/api/Item/"+Item.ItemId,
                    new StringContent(ItemJson, System.Text.Encoding.UTF8, "application/json"));
                Current.Name = Item.Name;
                Current.Category = category;
                _catVM.EditItem(Current);
                Success = true;
                Hide();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong: {e}");
                this.TxtBottomError.Text = e.Message;
            }
        }

        private async void InitializeCategories()
        {
            var res = await _authVM.Client.GetAsync("http://hyphen-solutions.be/unipack/api/category");
            var stringRes = res.Content.ReadAsStringAsync().Result;
            var categories = JsonConvert.DeserializeObject<List<CategoryDto>>(stringRes);

            categories.ForEach(c => Categories.Add(new Category { Name = c.Name, Id = c.CategoryId }));

        }

        public bool Validate()
        {
            string result = "";
            bool success = true;

            if (GetItemName().Length == 0)
            {
                this.TxtItemName.Foreground = new SolidColorBrush(Colors.Red);
                result += "Name is required.\n";
                success = false;
            }

            if (result.Length > 0)
            {
                this.TxtBottomError.Text = result;
            }
            return success;
        }

        private async void BtnEdit_OnClick(object sender, RoutedEventArgs e)
        {
            this.TxtItemName.Foreground = new SolidColorBrush(Colors.Black);
            this.TxtBottomError.Text = "";
            await Edit();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
