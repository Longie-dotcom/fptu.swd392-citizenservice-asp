using IAMServer.gRPC;

namespace Application.Interface.IGrpcClient
{
    public interface IIAMClient
    {
        Task<GetUserResponse> GetUser(GetUserRequest request);
        Task<CreateUserResponse> CreateUser(CreateUserRequest request);
    }
}
