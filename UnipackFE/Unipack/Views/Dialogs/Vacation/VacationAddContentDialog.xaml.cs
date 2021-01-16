using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Unipack.DTOs;
using Unipack.Models;
using Unipack.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace Unipack.Views.Dialogs.Vacation
{
    public sealed partial class VacationAddContentDialog : ContentDialog
    {
        ICollection<VacationLocation> Locations { get; set; }
        private AuthenticationViewModel _authVM;
        private VacationViewModel _vacVM;
        public bool Success { get; set; }
        public VacationAddContentDialog(AuthenticationViewModel authVM, VacationViewModel vacVM)
        {
            _vacVM = vacVM;
            _authVM = authVM;
            Success = false;
            this.InitializeComponent();
        }

        private async void BtnAdd_OnClick(object sender, RoutedEventArgs e)
        {
            this.TxtVacationName.Foreground = new SolidColorBrush(Colors.Black);
            this.TxtBottomError.Text = "";
            await Create();
        }

        private async Task Create()
        {
            try
            {
                if (!Validate())
                    return;
                HttpClient client = new HttpClient();
                var vacation = GetVacationData();
                var vacationJson = JsonConvert.SerializeObject(vacation);
                await _authVM.Client.PostAsync("http://hyphen-solutions.be/unipack/api/vacation",
                    new StringContent(vacationJson, System.Text.Encoding.UTF8, "application/json"));
                _vacVM.AddVacation(new Models.Vacation { AddedOn = vacation.AddedOn, Name = vacation.Name, DateDeparture = vacation.DateDeparture, 
                                                            DateReturn = vacation.DateReturn, Locations = (ICollection<Models.VacationLocation>)vacation.Locations,
                                                            VacationId = vacation.VacationId, PackLists = (ICollection<Models.PackList>)vacation.PackLists});
                Success = true;
                Hide();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong: {e}");
                this.TxtBottomError.Text = e.Message;
            }
        }

        public bool Validate()
        {
            string result = "";
            bool success = true;
            var vac = GetVacationData();

            if (vac.Name.Length == 0)
            {
                this.TxtVacationName.Foreground = new SolidColorBrush(Colors.Red);
                result += "Name is required.\n";
                success = false;
            }

            if(vac.DateReturn < vac.DateDeparture)
            {
                this.ReturnDatePicker.Foreground = new SolidColorBrush(Colors.Red);
                result += "Date of return can not be before departure";
            }
            if (result.Length > 0)
            {
                this.TxtBottomError.Text = result;
            }
            return success;
        }

        public VacationDto GetVacationData()
        {
            var dateDepart = DepartureDatePicker.Date;
            DateTime departureTime = dateDepart.Value.DateTime;

            var dateReturn = ReturnDatePicker.Date;
            DateTime returnTime = dateReturn.Value.DateTime;


            var newVac = new VacationDto() {AddedOn = DateTime.Now, Name = TxtVacationName.Text, Locations = (ICollection<VacationLocationDto>)Locations, 
                                            DateDeparture = departureTime, DateReturn = returnTime  };
            return newVac;
        }

        private async void BtnAddVacation_Click(object sender, RoutedEventArgs e)
        {
            this.ReturnDatePicker.Foreground = new SolidColorBrush(Colors.Black);
            this.TxtVacationName.Foreground = new SolidColorBrush(Colors.Black);

            await Create();

        }

        private void ButtonAddLocation_Click(object sender, RoutedEventArgs e)
        {
            var dateArrive = ArrivalDateLocation.Date;
            DateTime arriveTime = dateArrive.Value.DateTime;

            var dateDepart = DepartureDateLocation.Date;
            DateTime departTime = dateDepart.Value.DateTime;

            var loc = new VacationLocation() { AddedOn = DateTime.Now, CityName = CityNameLoc.Text, CountryName = CountryNameLoc.Text, DateArrival = arriveTime, DateDeparture = departTime };
            Locations.Add(loc);
            LocationListView.DataContext = Locations;

            AddLocationForm.Visibility = Visibility.Collapsed;
        }

        private void ButtonCancelLocation_Click(object sender, RoutedEventArgs e)
        {
            CountryNameLoc.Text = "";
            CityNameLoc.Text = "";
            ArrivalDateLocation.Date = DateTimeOffset.Now;
            DepartureDateLocation.Date = DateTimeOffset.Now;
            AddLocationForm.Visibility = Visibility.Collapsed;
        }
    }
}
