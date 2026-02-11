using Ardalis.Result;
using MediatR;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Repositories;

namespace OrderManagement.Application.Features.Auth.Commands.Register
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, Result<RegisterResponse>>
    {
        private readonly IGenericRepository<User> _userRepository;

        public RegisterHandler(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<RegisterResponse>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            var validator = new RegisterValidator();
            var validationResult = validator.Validate(command);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => new ValidationError(e.ErrorMessage))
                    .ToList();
                return await Task.FromResult(Result<RegisterResponse>.Invalid(errors));
            }

            var existingUser = _userRepository.GetAll()
                .FirstOrDefault(u => u.Email == command.Request.Email);
            if (existingUser != null)
                return await Task.FromResult(Result<RegisterResponse>.Error(new ErrorList(new[] { "Email already exists" }, (string?)null)));

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(command.Request.Password);

            var user = new User
            {
                UserName = command.Request.UserName,
                Email = command.Request.Email,
                PasswordHash = hashedPassword
            };

            var createdUser = _userRepository.Add(user);

            var response = new RegisterResponse
            {
                User = new RegisterDto
                {
                    Id = createdUser.Id,
                    Name = createdUser.UserName,
                    Email = createdUser.Email
                }
            };

            return await Task.FromResult(Result.Success(response));
        }
    }
}
