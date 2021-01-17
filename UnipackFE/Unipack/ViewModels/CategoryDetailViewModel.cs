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
        public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();
        public Category category { get; set; }
        public User User { get; set; }

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public void DeleteItem(Item item)
        {
            Items.Remove(item);
        }

        public void Clear()
        {
            Items.Clear();
        }

    }
}
