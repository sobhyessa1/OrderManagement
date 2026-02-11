namespace OrderManagement.Application.Features.Auth.Commands.Login
{
    public class LoginResponse
    {
        public LoginResponseDto User { get; set; }
    }

    public class LoginResponseDto
    {
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
