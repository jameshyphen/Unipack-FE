using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Unipack.DTOs;
using Unipack.Enums;
using Unipack.Models;

namespace Unipack.ViewModels
{
    public class CategoryDetailViewModel
    {
        private AuthenticationViewModel _authenticationVM;
        public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();
        public Category Category { get; set; }
        public User User { get; set; }

        public CategoryDetailViewModel(Category cat, AuthenticationViewModel vm)
        {
            _authenticationVM = vm;
            Category = cat;
            InitializeCategoryDetails();
        }

        private async void InitializeCategoryDetails()
        {
            var res = await _authenticationVM.Client.GetAsync("http://hyphen-solutions.be/unipack/api/category/" + Category.Id + "/items");
            var stringRes = res.Content.ReadAsStringAsync().Result;
            var items = JsonConvert.DeserializeObject<CategoryDto>(stringRes);

            items.Items.ToList().ForEach(c => AddItem(new Item { AddedOn = c.AddedOn, ItemId = c.ItemId, Name = c.Name, Priority = (Priority)c.Priority }));
        }

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public async void AddItemAPI(ItemDto dto)
        {
            var itemJson = JsonConvert.SerializeObject(dto);
            var res = await _authenticationVM.Client.PostAsync("http://hyphen-solutions.be/unipack/api/item",
                new StringContent(itemJson, System.Text.Encoding.UTF8, "application/json"));
            var stringRes = res.Content.ReadAsStringAsync().Result;
            var it = JsonConvert.DeserializeObject<ItemDto>(stringRes);
            AddItem(new Item() { Name = it.Name, AddedOn = it.AddedOn, Category = Category, ItemId = it.ItemId, Priority = (Priority)it.Priority });
        }

        public async void DeleteItem(Item item)
        {
            Items.Remove(item);
            await _authenticationVM.Client.DeleteAsync("http://hyphen-solutions.be/unipack/api/item/" + item.ItemId);
        }

        public void Clear()
        {
            Items.Clear();
        }

    }
}
