using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unipack.Models;

namespace Unipack.ViewModels
{
    class CategoryDetailViewModel
    {
        public ObservableCollection<Item> items { get; set; } = new ObservableCollection<Item>();
        public User User { get; set; }

        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public void DeleteItem(Item item)
        {
            items.Remove(item);
            //TODO hier pai call doen voor categorie te verwijderen
        }

        public void Clear()
        {
            items.Clear();
        }

    }
}
