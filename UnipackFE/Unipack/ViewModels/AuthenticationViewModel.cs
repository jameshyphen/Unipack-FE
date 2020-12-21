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

        public async Task Register(Register register)
        {

        }

        public async Task Login(Login login)
        {
            try
            {

                HttpClient client = new HttpClient();
                var loginJson = JsonConvert.SerializeObject(login);
                Console.WriteLine(loginJson);
                var res = await client.PostAsync("http://www.hyphen-solutions.be/unipack/api/account/login",
                    new StringContent(loginJson, System.Text.Encoding.UTF8, "application/json"));
                var result = res.Content.ReadAsStringAsync().Result;
                var auth = JsonConvert.DeserializeObject<AuthenticationDto>(result);

                if (res.IsSuccessStatusCode)
                {
                    this.Token = auth.Token;
                    this.User = new User
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
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong: {e}");
            }
        }
    }
}
