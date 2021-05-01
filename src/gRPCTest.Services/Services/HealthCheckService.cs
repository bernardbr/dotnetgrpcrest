using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Health.V1;
using Grpc.HealthCheck;
using gRPCTest.Services.Protos;

namespace gRPCTest.Services.Services
{
    public class HealthCheckService : HealthCheck.HealthCheckBase
    {
        private readonly HealthServiceImpl _healthServiceImpl;

        public HealthCheckService(HealthServiceImpl healthServiceImpl)
        {
            _healthServiceImpl = healthServiceImpl;
        }

        public override Task<HealthCheckResponse> Check(HealthCheckRequest request, ServerCallContext context)
        {
            request.Service = request.Service.ToUpperInvariant();
            return _healthServiceImpl.Check(request, context);
        }

        public override Task Watch(HealthCheckRequest request, IServerStreamWriter<HealthCheckResponse> responseStream, ServerCallContext context)
        {
            request.Service = request.Service.ToUpperInvariant();
            return _healthServiceImpl.Watch(request, responseStream, context);
        }
    }
}
