using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Unipack.DTOs;
using Unipack.Enums;
using Unipack.Models;
using Unipack.ViewModels;
using Unipack.Views.Dialogs;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Unipack.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
   
    public sealed partial class VacationPage : Page
    {

        private AuthenticationViewModel _authVM;
        private VacationViewModel _vacationVM;
        ObservableCollection<Vacation> Vacations = new ObservableCollection<Vacation>(); 

        public VacationPage()
        {
            this.InitializeComponent();
            _vacationVM = new VacationViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _authVM = (AuthenticationViewModel)e.Parameter;
            InitializeVacations();
        }

        public async void InitializeVacations()
        {

            var json = await _authVM.Client.GetAsync("http://hyphen-solutions.be/unipack/api/vacation");
            var stringRes = json.Content.ReadAsStringAsync().Result;
            var vacationList = JsonConvert.DeserializeObject<List<VacationDto>>(stringRes);

            vacationList.ForEach(v => _vacationVM.AddVacation(new Vacation { VacationId = v.VacationId, AddedOn = v.AddedOn, Name = v.Name, DateDeparture = v.DateDeparture,
                DateReturn = v.DateReturn, Locations = v.Locations.Select(loc => new VacationLocation
                {
                    AddedOn = loc.AddedOn, CityName = loc.CityName, CountryName = loc.CountryName, DateArrival = loc.DateArrival, DateDeparture = loc.DateDeparture, VacationLocationId = loc.VacationLocationId
                }).ToList(),
                PackLists = v.PackLists.Select(pl => new PackList
                {
                    AddedOn = pl.AddedOn,
                    PackItems = pl.Items.Select(i => new PackItem
                    {
                        ItemId = i.ItemId,  PackedQuantity = i.Quantity

                    }).ToList(),
                    Name = pl.Name,
                    Tasks = pl.Tasks.Select(t => new PackTask { AddedOn = t.AddedOn, Name = t.Name, Priority = (Priority)t.Priority, Deadline = t.DeadLine, PackTaskId = t.PackTaskId }).ToList(),
                    PackListId = pl.PackListId
                }).ToList()
            }));
            vacationsGridView.DataContext = _vacationVM.vacations.OrderBy(v => v.DateDeparture);


        }

        private void vacationsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Vacation selectedVacation = (Vacation)e.ClickedItem;
            VacationDetailParameters param = new VacationDetailParameters(_authVM, selectedVacation);

            Frame.Navigate(typeof(VacationDetailPage), param);
        }

        

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            VacationAddContentDialog addContentDialog = new VacationAddContentDialog(_authVM, _vacationVM);
            await addContentDialog.ShowAsync();
            if (!addContentDialog.Success)
                return;
            Vacations = _vacationVM.vacations;
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var vac = (Vacation)button.DataContext;

            _vacationVM.DeleteVacation(vac.VacationId);
            await _authVM.Client.DeleteAsync("http://hyphen-solutions.be/unipack/api/vacation/" + vac.VacationId);
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var vac = (Vacation)button.DataContext;

            VacationEditContentDialog editContentDialog = new VacationEditContentDialog(_authVM, _vacationVM, vac);

            await editContentDialog.ShowAsync();
            if (!editContentDialog.Success)
                return;
            Vacations = _vacationVM.vacations;
        }
    }
}


public class VacationDetailParameters
{

    public AuthenticationViewModel _authenticationVM { get; set; }
    public Vacation _vacation { get; set; }

    public VacationDetailParameters(AuthenticationViewModel AVM, Vacation vac)
    {
        _authenticationVM = AVM;
        _vacation = vac;
    }
}
