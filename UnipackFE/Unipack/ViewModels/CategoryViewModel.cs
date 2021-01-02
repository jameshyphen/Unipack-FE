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

        public void DeleteCategory(Category cat)
        {
            categories.Remove(cat);
            //TODO hier pai call doen voor categorie te verwijderen
        }

        public void Clear()
        {
            categories.Clear();
        }
    }
}
