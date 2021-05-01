using Grpc.Core;
using gRPCTest.Services.Protos;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace gRPCTest.Services.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;

        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"{nameof(SayHello)} started.");
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
