using Ardalis.Result;
using MediatR;

namespace OrderManagement.Application.Features.Auth.Commands.Login
{
    public class LoginCommand : IRequest<Result<LoginResponse>>
    {
        public LoginRequest Request { get; set; }
        public LoginCommand(LoginRequest request)
        {
            Request = request;
        }
    }
}
