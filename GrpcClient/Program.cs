using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcService;
using static GrpcService.ToDoTaskService;

namespace GrpcClientSample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Ensure the server is running (GrpcService project)
            var address = args.Length > 0 ? args[0] : "https://localhost:5001";

            // Create a channel
            using var channel = GrpcChannel.ForAddress(address);

            // Generated client type may be top-level `ToDoTaskServiceClient` depending on proto generation settings
            var client = new ToDoTaskServiceClient(channel);

            // Unary GetAll
            var getAllRequest = new GetAllRequest { Title = "" };
            var response = await client.GetAllAsync(getAllRequest);

            Console.WriteLine($"Received {response.Tasks.Count} tasks from gRPC");
            foreach (var t in response.Tasks)
            {
                Console.WriteLine($"{t.Id} {t.Title} {t.Status} {t.CompletedAt}");
            }

            // Streaming example
            using var call = client.StreamAll(new GetAllRequest());
            await foreach (var task in call.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine($"stream: {task.Id} {task.Title}");
            }
        }
    }
}
