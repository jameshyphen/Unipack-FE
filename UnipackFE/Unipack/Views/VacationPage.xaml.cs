using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public bool Success { get; set; }
        public VacationPage() {
            this.InitializeComponent();
        }
        public VacationPage(AuthenticationViewModel authvm) : this()
        {
            authVM = authvm;
            Success = false;
        }
        public VacationPage(AuthenticationViewModel authvm) : this()
        {
            authVM = authvm;
        }

        public async void getData()
        {

            HttpClient client = new HttpClient();
            var json = await client.GetStringAsync(new Uri("http://195.201.101.209/unipack/api/vacation"));
            var vacationList = JsonConvert.DeserializeObject<ICollection<Vacation>>(json);
            vacationsListView.ItemsSource = vacationList;

        }
    }
}
