namespace Unipack.DTOs
{
    public class AuthenticationDto
    {
        public string Token { get; set; }
        public UserDto UserDto { get; set; }
        public string Message { get; set; }
    }
}
