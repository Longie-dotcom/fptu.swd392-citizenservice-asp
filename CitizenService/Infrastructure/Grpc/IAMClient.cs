using Application.Interface.IGrpcClient;
using Grpc.Core;
using Grpc.Net.Client;
using IAMServer.gRPC;
using Infrastructure.InfrastructureException;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Grpc
{
    public class IAMClient : IIAMClient
    {
        private readonly IAMGrpc.IAMGrpcClient client;

        public IAMClient(IConfiguration configuration)
        {
            var grpcServerUrl = configuration["GRPC_IAM_SERVICE"];
            if (string.IsNullOrEmpty(grpcServerUrl))
                throw new GrpcCommunicationException("gRPC server URL is missing.");

            var channel = GrpcChannel.ForAddress(grpcServerUrl);
            client = new IAMGrpc.IAMGrpcClient(channel);
        }

        public async Task<GetUserResponse> GetUser(GetUserRequest request)
        {
            try
            {
                var user = await client.GetUserAsync(request);
                return user;
            }
            catch (RpcException ex)
            {
                throw new RPCConflict(ex.Message);
            }
        }

        public async Task<CreateUserResponse> CreateUser(CreateUserRequest request)
        {
            try
            {
                var user = await client.CreateUserAsync(request);
                return user;
            }
            catch (RpcException ex)
            {
                throw new RPCConflict(ex.Message);
            }
        }
    }
}
