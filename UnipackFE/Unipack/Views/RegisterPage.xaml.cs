using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class RegisterPage : Page
    {
        public RegisterPage()
        {
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
        public string GetPasswordConfirmation()
        {
            return this.PsbPasswordConfirmation.Password;
        }

        public bool Validate()
        {
            if (this.PsbPassword.Password.Length == 0)
            {
                this.TxtPasswordValidation.Text = "Password is required.";
                return false;
            }
            if (this.PsbPassword.Password.Length < 6)
            {
                this.TxtPasswordValidation.Text = "Password must be minimum 6 characters.";
                return false;
            }
            if (this.PsbPasswordConfirmation.Password.Length == 0)
            {
                this.TxtPasswordConfirmationValidation.Text = "Password confirmation is required.";
                return false;
            }
            if (this.PsbPasswordConfirmation.Password.Length < 6)
            {
                this.TxtPasswordConfirmationValidation.Text = "Password confirmation must be minimum 6 characters.";
                return false;
            }
            if (!this.PsbPasswordConfirmation.Password.Equals(this.PsbPassword.Password))
            {
                this.TxtPasswordConfirmationValidation.Text = "Password and password validation must be the same.";
                this.TxtPasswordValidation.Text = "Password and password validation must be the same.";
                return false;
            }
            if (this.TxtUsername.Text.Length == 0)
            {
                this.TxtUsernameValidation.Text = "Username is required.";
                return false;
            }

            if (this.TxtPasswordValidation.Text.Length < 6)
            {
                this.TxtUsernameValidation.Text = "Username must be minimum 6 characters.";
                return false;
            }

            return true;
        }
    }
}
