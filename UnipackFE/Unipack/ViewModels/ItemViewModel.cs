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
    public class ItemViewModel : BindableBase
    {
        public ObservableCollection<Item> items { get; set; } = new ObservableCollection<Item>();
        public User User { get; set; }

        public void AddItem(Item cat)
        {
            items.Add(cat);
        }

        public void EditItem(Item item)
        {
            items.Where(c => c.ItemId == item.ItemId).Select(c => c = item);
        }

        public void DeleteItem(int id)
        {
            Item cat = GetItemById(id);
            items.Remove(cat);
        }

        private Item GetItemById(int id)
        {
            return items.First(c => c.ItemId == id);
        }

        public void Clear()
        {
            items.Clear();
        }
    }
}
