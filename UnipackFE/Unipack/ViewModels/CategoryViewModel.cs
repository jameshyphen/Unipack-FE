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

        public void EditCategory(Category cat)
        {
            categories.Where(c => c.Id == cat.Id).Select(c => c = cat);
        }

        public void DeleteCategory(int id)
        {
            Category cat = GetCategoryById(id);
            categories.Remove(cat);
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
