namespace OrderManagement.Application.Services
{
    public interface ITokenService
    {
        string GenerateToken(int userId, string userName, string email);
    }
}
