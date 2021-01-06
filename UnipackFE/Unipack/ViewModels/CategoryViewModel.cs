using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unipack.Models;

namespace Unipack.ViewModels
{
    public class CategoryViewModel : BindableBase
    {
        public ObservableCollection<Category> categories { get; set; } = new ObservableCollection<Category>();
        public User User { get; set; }

        public void AddCategory(Category cat)
        {
            categories.Add(cat);
        }

        public void DeleteCategory(string categoryName)
        {
            Category cat = GetCategoryByName(categoryName);
            categories.Remove(cat);
            //TODO hier pai call doen voor categorie te verwijderen
        }
        
        private Category GetCategoryByName(string name)
        {
            return categories.First(c => c.Name.Equals(name));
        }

        public void Clear()
        {
            categories.Clear();
        }
    }
}
