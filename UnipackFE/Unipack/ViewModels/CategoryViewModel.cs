using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Unipack.DTOs;
using Unipack.Models;

namespace Unipack.ViewModels
{
    public class CategoryViewModel : BindableBase
    {
        private AuthenticationViewModel _authenticationVM;
        public ObservableCollection<Category> categories {  get; set; } = new ObservableCollection<Category>();
        public User User { get; set; }

        public void AddCategory(Category cat)
        {
            categories.Add(cat);
        }

        public void SetAuthVM(AuthenticationViewModel vm)
        {
            this._authenticationVM = vm;
        }

        public async void InitializeCategories()
        {
            var res = await _authenticationVM.Client.GetAsync("http://hyphen-solutions.be/unipack/api/category");
            var stringRes = res.Content.ReadAsStringAsync().Result;
            var categories = JsonConvert.DeserializeObject<List<CategoryDto>>(stringRes);

            categories.ForEach(c => this.AddCategory(new Category { Id = c.CategoryId, AddedOn = c.AddedOn, Name = c.Name, NumberOfItems = 0 }));
        }

        public async void AddCategoryAPI(CategoryDto dto)
        {
            var categoryJson = JsonConvert.SerializeObject(dto);
            var res = await _authenticationVM.Client.PostAsync("http://hyphen-solutions.be/unipack/api/category",
                new StringContent(categoryJson, System.Text.Encoding.UTF8, "application/json"));
            var stringRes = res.Content.ReadAsStringAsync().Result;
            var cat = JsonConvert.DeserializeObject<CategoryDto>(stringRes);
            this.AddCategory(new Category() { AddedOn = cat.AddedOn, Id = cat.CategoryId, Name = cat.Name });
        }

        public async void EditCategory(CategoryDto cat, Category current)
        {
            var categoryJson = JsonConvert.SerializeObject(cat);
            await _authenticationVM.Client.PutAsync("http://hyphen-solutions.be/unipack/api/category/" + cat.CategoryId,
                new StringContent(categoryJson, System.Text.Encoding.UTF8, "application/json"));
            categories.Where(c => c.Id == cat.CategoryId).Select(c => c.Name = cat.Name);
        }

        public async void DeleteCategory(int id)
        {
            Category cat = GetCategoryById(id);
            categories.Remove(cat);
            await _authenticationVM.Client.DeleteAsync("http://hyphen-solutions.be/unipack/api/category/" + cat.Id);
        }
        
        private Category GetCategoryById(int id)
        {
            return categories.First(c => c.Id == id);
        }

        public void Clear()
        {
            categories.Clear();
        }
    }
}
