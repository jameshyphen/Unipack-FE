using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unipack.Models;

namespace Unipack.ViewModels
{
    public class VacationDetailViewModel
    {
        public ObservableCollection<VacationLocation> Locations { get; set; } = new ObservableCollection<VacationLocation>();
        public ObservableCollection<PackList> PackLists { get; set; } = new ObservableCollection<PackList>();
        public Vacation vacation { get; set; }
        public User User { get; set; }

        public void AddPackList(PackList packList)
        {
            PackLists.Add(packList);
        }

        public void AddItemToPackList(PackList packList, Item item)
        {
            var list = PackLists.Where(p => p.PackListId == packList.PackListId).FirstOrDefault();
            list.PackItems.Add(item);
        }
        public void DeleteItem(PackList packList, Item item)
        {
            var list = PackLists.Where(p => p.PackListId == packList.PackListId).FirstOrDefault();
            list.PackItems.Remove(item);
        }

        public void Clear()
        {
            Clear();
        }

        internal void DeleteTask(PackList packList, PackTask task)
        {
            var list = PackLists.Where(p => p.PackListId == packList.PackListId).FirstOrDefault();
            list.Tasks.Remove(task);
        }
    }
}
