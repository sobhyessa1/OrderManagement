using OrderManagement.Application.Interfaces;
using OrderManagement.Infrastructure.Protos;

namespace OrderManagement.Infrastructure.Gateways
{
    public class UserGateway : IUserGateway
    {
        private readonly UserGrpc.UserGrpcClient _grpcClient;

        public UserGateway(UserGrpc.UserGrpcClient grpcClient)
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
