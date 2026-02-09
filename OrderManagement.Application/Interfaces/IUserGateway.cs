namespace OrderManagement.Application.Interfaces
{
    public interface IUserGateway
    {
        bool IsUserExists(int userId);
    }
}
