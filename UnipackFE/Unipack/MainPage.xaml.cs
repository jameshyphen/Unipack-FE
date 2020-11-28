using System;
using System.Threading.Tasks;
using Unipack.Views;
using Unipack.Models;
using Unipack.ViewModels;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using GalaSoft.MvvmLight.Command;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Unipack
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private AuthenticationViewModel _authenticationVM;
        public MainPage()
        {
            _authenticationVM = new AuthenticationViewModel();
            this.InitializeComponent();
            this.initUserless();
        }

        private void OnPointEnter(object sender, PointerRoutedEventArgs e)
        {
            this.Background = new SolidColorBrush(Color.FromArgb(10, 0, 0, 0));
            this.Foreground = new SolidColorBrush(Colors.White);
            VisualStateManager.GoToState(this, "VisualStateNormal", true);
        }

        private void OnPointExit(object sender, PointerRoutedEventArgs e)
        {
            this.Background = new SolidColorBrush(Color.FromArgb(25, 0, 0, 0));
            this.Foreground = new SolidColorBrush(Colors.White);
            VisualStateManager.GoToState(this, "VisualStateNormal", true);
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "Sign in",
                Content = loginPage,
                PrimaryButtonText = "Log in",
                CloseButtonText = "Cancel",
                PrimaryButtonCommand = new RelayCommand(async () =>
                {
                    await this._authenticationVM.Login(new Login
                    {
                        Username = loginPage.GetUsername(),
                        Password = loginPage.GetPassword()
                    });
                }, () => loginPage.Validate()),
            };

            await noWifiDialog.ShowAsync();
            if (this._authenticationVM.User != null)
            {
                string WelcomeString = $"Welcome, {_authenticationVM.User.FirstName}";
                WelcomeDropDown.DataContext = WelcomeString;
                this.BtnLogin.Visibility = Visibility.Collapsed;
                this.BtnRegister.Visibility = Visibility.Collapsed;
                this.MenuFlyOutLogin.Visibility = Visibility.Collapsed;
                this.MenuFlyOutRegister.Visibility = Visibility.Collapsed;
                this.MenuFlyOutAccountSettings.Visibility = Visibility.Visible;
                this.MenuFlyOutLogOut.Visibility = Visibility.Visible;
            }
        }

        private async void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterPage registerPage = new RegisterPage();

            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "Register",
                Content = registerPage,
                PrimaryButtonText = "Register",
                CloseButtonText = "Cancel",
                PrimaryButtonCommand = new RelayCommand(async () =>
                {
                    var password = registerPage.GetPassword();
                    var passwordConfirmation = registerPage.GetPasswordConfirmation();
                    if(password.Equals(passwordConfirmation))
                    await this._authenticationVM.Register(new Register
                    {
                        UserName = registerPage.GetUsername(),
                        Password = password
                    });
                }, () => registerPage.Validate()),
            };

            await noWifiDialog.ShowAsync();
            if (this._authenticationVM.User != null)
            {
                string WelcomeString = $"Welcome, {_authenticationVM.User.FirstName}";
                WelcomeDropDown.DataContext = WelcomeString;
                this.BtnLogin.Visibility = Visibility.Collapsed;
                this.BtnRegister.Visibility = Visibility.Collapsed;
                this.MenuFlyOutLogin.Visibility = Visibility.Collapsed;
                this.MenuFlyOutRegister.Visibility = Visibility.Collapsed;
                this.MenuFlyOutAccountSettings.Visibility = Visibility.Visible;
                this.MenuFlyOutLogOut.Visibility = Visibility.Visible;
            }
        }
        private void BtnAccountSettings_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            this._authenticationVM.LogOut();
            this.initUserless();
        }

        private void initUserless()
        {
            string WelcomeString = "Welcome, Please log in.";
            this.BtnLogin.Visibility = Visibility.Visible;
            this.BtnRegister.Visibility = Visibility.Visible;
            this.MenuFlyOutLogin.Visibility = Visibility.Visible;
            this.MenuFlyOutRegister.Visibility = Visibility.Visible;
            this.MenuFlyOutAccountSettings.Visibility = Visibility.Collapsed;
            this.MenuFlyOutLogOut.Visibility = Visibility.Collapsed;
            WelcomeDropDown.DataContext = WelcomeString;
        }
    }
}
