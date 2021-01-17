using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unipack.DTOs;
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
    public sealed partial class VacationDetailPage : Page
    {
        private AuthenticationViewModel _authVM;
        private VacationDetailViewModel _vacationDVM;
        Vacation Vacation;
        public ObservableCollection<VacationLocation> Locations { get; set; } = new ObservableCollection<VacationLocation>();
        public ObservableCollection<PackList> PackLists { get; set; } = new ObservableCollection<PackList>();
        public VacationDetailPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            VacationDetailParameters param = (VacationDetailParameters)e.Parameter;
            _authVM = param._authenticationVM;
            InitializeVacationDetails(param._vacation);
        }

        private void InitializeVacationDetails(Vacation vac)
        {
            Vacation = vac;
            _vacationDVM = new VacationDetailViewModel() { vacation = vac };
            Header.Text = vac.Name;
            Locations = (ObservableCollection<VacationLocation>)vac.Locations;
            PackLists = (ObservableCollection<PackList>)vac.PackLists;
        }

        private async void DeleteItemButton_Click(object sender, RoutedEventArgs e)
        {
            PackItem item = (PackItem)PackListView.SelectedItem;
            
            _vacationDVM.DeleteItem(item);
            await _authVM.Client.DeleteAsync("http://hyphen-solutions.be/unipack/api/item/" + item.PackItemId);

        }

        private async void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            PackTask task = (PackTask)PackListView.SelectedItem;
            _vacationDVM.DeleteTask(task);
            await _authVM.Client.DeleteAsync("http://hyphen-solutions.be/unipack/api/task/" + task.PackTaskId);
        }

        private void ItemCheckBox_Click(object sender, RoutedEventArgs e)
        {
            PackItem item = (PackItem)PackListView.SelectedItem;
            //GET checkbox?


            //await _authVM.Client.
        }

        private void TaskCheckBox_Click_1(object sender, RoutedEventArgs e)
        {
        }

        private void VacationPageDetails(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MapPage), Locations);
        }

    }
}
