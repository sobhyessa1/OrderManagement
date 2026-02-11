using OrderManagement.Application.Interfaces;
using OrderManagement.Infrastructure.Protos;

namespace OrderManagement.Infrastructure.GrpcClient
{
    public class UserClient : IUserClient
    {
        private readonly UserGrpc.UserGrpcClient _grpcClient;

        public UserClient(UserGrpc.UserGrpcClient grpcClient)
        {
            _grpcClient = grpcClient;
        }

        public bool IsUserExists(int userId)
        {
            var request = new GetUserRequest { Id = userId };
            var response = _grpcClient.IsUserExists(request);
            return response.Exists;
        }
    }
}
