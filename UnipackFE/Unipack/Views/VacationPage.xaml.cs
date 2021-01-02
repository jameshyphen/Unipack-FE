using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Unipack.Models;
using Unipack.ViewModels;
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
        
        public AuthenticationViewModel authVM { get; set; }
        ObservableCollection<Vacation> Vacations = new ObservableCollection<Vacation>(); //move this to viewModel later

        public VacationPage()
        {
            this.InitializeComponent();
            getData();
        }


        public void getData()
        {

            //HttpClient client = new HttpClient();
            //var json = await client.GetStringAsync(new Uri("http://195.201.101.209/unipack/api/vacation"));
            //var vacationList = JsonConvert.DeserializeObject<ICollection<Vacation>>(json);
            //vacationsGridView.ItemsSource = vacationList;

            //TestData
            VacationList l1 = new VacationList() { };
            VacationList l2 = new VacationList() { };

            Vacation v1 = new Vacation() { AddedOn = DateTime.Now, Name = "London", DateDeparture = DateTime.Today, DateReturn = DateTime.Now.AddDays(5), VacationLists = new Collection<VacationList>() { l1 } };
            Vacation v2 = new Vacation() { AddedOn = DateTime.Now, Name = "Berlin", DateDeparture = DateTime.Now.AddDays(3), DateReturn = DateTime.Now.AddDays(15), VacationLists = new Collection<VacationList>() { l1,l2 } };
            Vacation v3 = new Vacation() { AddedOn = DateTime.Now, Name = "Budapest", DateDeparture = DateTime.Now.AddDays(6), DateReturn = DateTime.Now.AddDays(30),VacationLists = new Collection<VacationList>() { l1 } };

            Vacations.Add(v1);
            Vacations.Add(v2);
            Vacations.Add(v3);


        }

        private void vacationsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Vacation selectedVacation = (Vacation)vacationsGridView.SelectedItem;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            authVM = (AuthenticationViewModel)e.Parameter;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
