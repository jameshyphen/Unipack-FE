using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Unipack.DTOs;
using Unipack.Enums;
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


namespace Unipack.Views.Dialogs
{
    public sealed partial class VacationAddContentDialog : ContentDialog
    {
        ObservableCollection<VacationLocation> Locations { get; set; }
        private AuthenticationViewModel _authVM;
        private VacationViewModel _vacVM;
        public bool Success { get; set; }
        public VacationAddContentDialog(AuthenticationViewModel authVM, VacationViewModel vacVM)
        {
            _vacVM = vacVM;
            _authVM = authVM;
            Success = false;
            Locations = new ObservableCollection<VacationLocation>();
            this.InitializeComponent();
            AddLocationForm.Visibility = Visibility.Collapsed;
            BtnDeleteLocation.Visibility = Visibility.Collapsed;

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
                                                            DateReturn = vacation.DateReturn, 
                                                            Locations = vacation.Locations.Select(loc => 
                                                                    new VacationLocation {AddedOn = loc.AddedOn, CityName = loc.CityName, 
                                                                                            CountryName = loc.CountryName, DateArrival = loc.DateArrival, 
                                                                                            DateDeparture =loc.DateDeparture, 
                                                                                            VacationLocationId = loc.VacationLocationId }).ToList(),
                                                            VacationId = vacation.VacationId, 
                                                            PackLists = vacation.PackLists.Select(pl => 
                                                                    new PackList
                                                                    {
                                                                        AddedOn = pl.AddedOn,
                                                                        Name = pl.Name,
                                                                        PackListId = pl.PackListId,
                                                                        Items = pl.Items.Select(i => new Item
                                                                        {
                                                                            Name = i.Name,
                                                                            AddedOn = i.AddedOn, //Category = i.CategoryId,
                                                                            ItemId = i.ItemId,
                                                                            Priority = (Priority)i.Priority
                                                                        }).ToList()
                                                                    }).ToList()
                                                            });
                
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
            Locations.OrderBy(l => l.DateArrival);
            LocationListView.DataContext = Locations;

            BtnDeleteLocation.Visibility = Visibility.Visible;
            ResetLocationForm();
        }

        private void ButtonCancelLocation_Click(object sender, RoutedEventArgs e)
        {
            ResetLocationForm();
        }

        private void ButtonExpandAddLocation_Click(object sender, RoutedEventArgs e)
        {
            AddLocationForm.Visibility = Visibility.Visible;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void ResetLocationForm() 
        {
            CountryNameLoc.Text = "";
            CityNameLoc.Text = "";
            ArrivalDateLocation.Date = DateTimeOffset.Now;
            DepartureDateLocation.Date = DateTimeOffset.Now;
            AddLocationForm.Visibility = Visibility.Collapsed;
        }

        private void BtnDeleteLocation_Click(object sender, RoutedEventArgs e)
        {
            VacationLocation selectedVacation = (VacationLocation)LocationListView.SelectedItem;
            if (selectedVacation != null)
                Locations.Remove(selectedVacation);

            if (Locations.Count == 0)
                BtnDeleteLocation.Visibility = Visibility.Collapsed;
        }
    }
}
