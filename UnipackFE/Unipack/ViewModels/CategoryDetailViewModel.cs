using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unipack.Models;

namespace Unipack.ViewModels
{
    public class CategoryDetailViewModel
    {
        public ObservableCollection<Item> items { get; set; } = new ObservableCollection<Item>();
        public Category category { get; set; }
        public User User { get; set; }

        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public void DeleteItem(Item item)
        {
            items.Remove(item);
        }

        public void Clear()
        {
            items.Clear();
        }

    }
}
