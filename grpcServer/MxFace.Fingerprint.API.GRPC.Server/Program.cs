using MxFace.Fingerprint.API.GRPC.Server.Services;
//using MxFace.SDK.Fingerprint;
//using MxFace.SDK.Fingerprint.Extensions.Configuration;

namespace MxFace.Fingerprint.API.GRPC.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddGrpc();

            //builder.UseMxFaceFingerprintSDK(configure: config =>
            //{
            //    config.Settings = new BiometricConfigurationSettings();
            //});
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<FingerprintService>();
            app.MapGet(
                "/",
                () =>
                    "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909"
            );

            app.Run();
        }
    }
}
