using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Unipack.DTOs;
using Unipack.Models;
using Unipack.ViewModels;
using Unipack.Views.Dialogs.Vacation;
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
        ObservableCollection<Vacation> Vacations = new ObservableCollection<Vacation>(); //move this to viewModel later

        public VacationPage()
        {
            this.InitializeComponent();
            _vacationVM = new VacationViewModel();
            getData();
        }


        public async void getData()
        {

            var json = await _authVM.Client.GetAsync("http://hyphen-solutions.be/unipack/api/vacation");
            var stringRes = json.Content.ReadAsStringAsync().Result;
            var vacationList = JsonConvert.DeserializeObject<List<VacationDto>>(stringRes);

            vacationList.ForEach(v => _vacationVM.AddVacation(new Vacation { VacationId = v.VacationId , AddedOn = v.AddedOn, Name = v.Name, DateDeparture = v.DateDeparture, 
                                                                            DateReturn = v.DateReturn, Locations = (ICollection<VacationLocation>)v.Locations, 
                                                                            PackLists = (ICollection<PackList>)v.PackLists }));
            vacationsGridView.DataContext = _vacationVM.vacations;


        }

        private void vacationsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Vacation selectedVacation = (Vacation)vacationsGridView.SelectedItem;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _authVM = (AuthenticationViewModel)e.Parameter;
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            VacationAddContentDialog addContentDialog = new VacationAddContentDialog(_authVM, _vacationVM);
            await addContentDialog.ShowAsync();
            if (!addContentDialog.Success)
                return;
            Vacations = _vacationVM.vacations;
        }

    }
}
