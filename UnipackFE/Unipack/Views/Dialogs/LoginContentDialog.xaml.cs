using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
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
using Newtonsoft.Json;
using Unipack.DTOs;
using Unipack.Models;
using Unipack.ViewModels;

namespace Unipack.Views.Dialogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginContentDialog : ContentDialog
    {
        public AuthenticationViewModel authVM { get; set; }
        public bool Success { get; set; }
        public LoginContentDialog(AuthenticationViewModel authvm)
        {
            authVM = authvm;
            Success = false;
            this.InitializeComponent();
        }
        public string GetUsername()
        {
            return this.TxtUsername.Text;
        }

        public string GetPassword()
        {
            return this.PsbPassword.Password;
        }

        public async Task Login()
        {
            try
            {
                if (!Validate())
                    return;
                await authVM.Login(new Login {Username = TxtUsername.Text, Password = PsbPassword.Password});
                Success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong: {e}");
                this.TxtBottomError.Text = e.Message;
            }
        }


        public bool Validate()
        {
            if (this.TxtUsername.Text.Length == 0)
            {
                this.TxtTopError.Text = "Username is required.";
                return false;
            }

            if (this.PsbPassword.Password.Length == 0)
            {
                this.TxtBottomError.Text = "Password is required.";
                return false;
            }
   
            if (this.TxtUsername.Text.Length < 6)
            {
                this.TxtTopError.Text = "Username must be minimum 6 characters.";
                return false;
            }
            if (this.PsbPassword.Password.Length < 6)
            {
                this.TxtBottomError.Text = "Password must be minimum 6 characters.";
                return false;
            }

            return true;
        }

        private void BtnLogin_OnClick(object sender, RoutedEventArgs e)
        {
            this.TxtTopError.Text = "";
            this.TxtBottomError.Text = "";
            Login().Wait();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
