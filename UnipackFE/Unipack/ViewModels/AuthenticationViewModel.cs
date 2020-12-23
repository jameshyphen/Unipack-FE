using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Unipack.DTOs;
using Unipack.Models;

namespace Unipack.ViewModels
{
    public class AuthenticationViewModel : BindableBase
    {
        public string Token { get; set; }
        public User User { get; set; }

        public void LogOut()
        {
            this.User = null;
            this.Token = null;
        }
    }
}
