using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Unipack_FE.Model;

namespace Unipack_FE.ViewModels
{
    class AuthenticationViewModel : BindableBase
    {
        public string Token { get; set; }
        public async Task Register(Register register)
        {

        }

        public async Task Login(Login login)
        {
            HttpClient client = new HttpClient();
            var loginJson = JsonConvert.SerializeObject(login);
            var res = await client.PostAsync("http://localhost:5001/api/account/login", new StringContent(loginJson, System.Text.Encoding.UTF8, "application/json"));
        }
    }
}
