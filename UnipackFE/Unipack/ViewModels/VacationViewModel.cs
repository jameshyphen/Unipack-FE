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
    public class VacationViewModel : BindableBase
    {
        public ObservableCollection<Vacation> vacations { get; set; } = new ObservableCollection<Vacation>();
        public User User { get; set; }

        public void AddVacation(Vacation vacation)
        {
            vacations.Add(vacation);
        }

        public void EditVacation(Vacation vacation)
        {
            vacations.Where(v => v.VacationId  == vacation.VacationId ).Select(v => v = vacation);
        }

        public void DeleteVacation(int id)
        {
            Vacation vacation = GetVacationById(id);
            vacations.Remove(vacation);
        }

        private Vacation GetVacationById(int id)
        {
            return vacations.First(v => v.VacationId == id);
        }

        public void Clear()
        {
            vacations.Clear();
        }
    }
}
