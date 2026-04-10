using Grpc.Net.Client;
using GrpcGreeterClient;
using GrpcToDoServiceClient;

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("https://localhost:5001");
//var client = new Greeter.GreeterClient(channel);
//var reply = await client.SayHelloAsync(
//    new HelloRequest { Name = "GreeterClient" });

var client = new ToDoTaskService.ToDoTaskServiceClient(channel);
var reply = await client.AddAsync(
    new AddRequest
    {
       Title = "Test Task",
       Description = "This is a test task",
       Status = Status.Todo
    });

Console.WriteLine("Task was created with Id: " + reply.Id);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();