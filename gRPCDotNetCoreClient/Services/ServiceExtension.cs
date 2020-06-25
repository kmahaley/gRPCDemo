using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Org.Example.NMVnext.Sevices;
using System;

namespace Book.Services
{
    public static class ServiceExtension
    {
        public static void AddGRPCPNClient(this IServiceCollection services, IConfiguration configuration) {

            var url = configuration["grpc:serverAddress"];
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            var channel = GrpcChannel.ForAddress(url);
            var client = new PNAPIBookService.PNAPIBookServiceClient(channel);

            services.AddSingleton(client);
        }

        public static void AddGRPCNMClient(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["grpc:serverAddress"];
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            var channel = GrpcChannel.ForAddress(url);
            var client = new NMAPIBookService.NMAPIBookServiceClient(channel);

            services.AddSingleton(client);
        }
    }
}
