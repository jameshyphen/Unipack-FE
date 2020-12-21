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
    public sealed partial class RegisterContentDialog : ContentDialog
    {
        public AuthenticationViewModel authVM { get; set; }
        public bool Success { get; set; }
        public RegisterContentDialog(AuthenticationViewModel authvm)
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

        public async Task Register()
        {
            try
            {
                if (!Validate())
                    return;
                await authVM.Register(new Register {Username = TxtUsername.Text, Password = PsbPassword.Password, FirstName = TxtFirstName.Text, LastName = TxtLastName.Text, Email = TxtEmail.Text});
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
            } else if (this.TxtUsername.Text.Length < 6)
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
            if (this.TxtFirstName.Text.Length == 0)
            {
                this.TxtFirstName.Foreground = new SolidColorBrush(Colors.Red);
                result += "First name is required.\n";
                success = false;
            }
            if (this.TxtLastName.Text.Length == 0)
            {
                this.TxtLastName.Foreground = new SolidColorBrush(Colors.Red);
                result += "Last name is required.\n";
                success = false;
            }

            if (this.TxtEmail.Text.Length > 0)
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(this.TxtEmail.Text);
                    if (addr.Address != this.TxtEmail.Text)
                    {
                        this.TxtEmail.Foreground = new SolidColorBrush(Colors.Red);
                        result += "Please specify a valid email address, or none.\n";
                        success = false;
                    }
                }
                catch
                {
                    this.TxtEmail.Foreground = new SolidColorBrush(Colors.Red);
                    result += "Please specify a valid email address, or none.\n";
                    success = false;
                }

            }
            else
            {
                this.TxtEmail.Foreground = new SolidColorBrush(Colors.Red);
                result += "Email is required.\n";
                success = false;
            }

            if (PsbPassword.Password != PsbPasswordConfirm.Password)
            {
                this.PsbPasswordConfirm.Foreground = new SolidColorBrush(Colors.Red);
                result += "The password confirmation doesn't match.\n";
                success = false;
            }

            if (result.Length > 0)
            {
                this.TxtBottomError.Text = result;
            }
            return success;
        }

        private async void BtnRegister_OnClick(object sender, RoutedEventArgs e)
        {
            this.TxtFirstName.Foreground = new SolidColorBrush(Colors.Black);
            this.TxtLastName.Foreground = new SolidColorBrush(Colors.Black);
            this.TxtEmail.Foreground = new SolidColorBrush(Colors.Black);
            this.TxtUsername.Foreground = new SolidColorBrush(Colors.Black);
            this.PsbPassword.Foreground = new SolidColorBrush(Colors.Black);
            this.PsbPasswordConfirm.Foreground = new SolidColorBrush(Colors.Black);
            this.TxtBottomError.Text = "";
            await Register();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

    }
}
