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
                HttpClient client = new HttpClient();
                var login = new Login {Username = TxtUsername.Text, Password = PsbPassword.Password};
                var loginJson = JsonConvert.SerializeObject(login);
                var res = await client.PostAsync("https://localhost:44349/unipack/api/account/login",
                    new StringContent(loginJson, System.Text.Encoding.UTF8, "application/json"));
                var result = res.Content.ReadAsStringAsync().Result;
                var auth = JsonConvert.DeserializeObject<AuthenticationDto>(result);

                if (res.IsSuccessStatusCode)
                {
                    authVM.Token = auth.Token;
                    authVM.User = new User
                    {
                        UserId = auth.UserDto.UserId,
                        Username = auth.UserDto.Username,
                        FirstName = auth.UserDto.FirstName,
                        Email = auth.UserDto.Email,
                        LastName = auth.UserDto.LastName
                    };
                }
                else
                {
                    throw new Exception(auth.Message);
                }
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

            if (this.TxtUsername.Text.Length == 0)
            {
                this.TxtUsername.Foreground = new SolidColorBrush(Colors.Red);
                result += "Username is required.\n";
                success = false;
            }
            else if (this.TxtUsername.Text.Length < 6)
            {
                this.TxtUsername.Foreground = new SolidColorBrush(Colors.Red);
                result += "Username must be minimum 6 characters.\n";
                success = false;
            }

            if (this.PsbPassword.Password.Length == 0)
            {
                this.PsbPassword.Foreground = new SolidColorBrush(Colors.Red);
                result += "Password is required.\n";
                success = false;
            } else if (this.PsbPassword.Password.Length < 6)
            {
                this.PsbPassword.Foreground = new SolidColorBrush(Colors.Red);
                result += "Password must be minimum 6 characters.\n";
                success = false;
            }

            if (result.Length > 0)
            {
                this.TxtBottomError.Text = result;
            }
            return success;
        }

        private async void BtnLogin_OnClick(object sender, RoutedEventArgs e)
        {
            this.TxtUsername.Foreground = new SolidColorBrush(Colors.Black);
            this.PsbPassword.Foreground = new SolidColorBrush(Colors.Black);
            this.TxtBottomError.Text = "";
            await Login();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

    }
}
