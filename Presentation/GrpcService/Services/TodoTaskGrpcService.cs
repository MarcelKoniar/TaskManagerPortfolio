using Application.DTO;
using Application.Interfaces;
using Application.Services;
using Domain.EntityModels;
using Domain.Enums;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace GrpcService.Services
{
    public class ToDoTaskGrpcService: ToDoTaskService.ToDoTaskServiceBase
    {
        private readonly IToDoTaskService _appService;
        private readonly ILogger<ToDoTaskGrpcService> _logger;
        public ToDoTaskGrpcService(IToDoTaskService appService, ILogger<ToDoTaskGrpcService> logger )
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
            _logger = logger;
        }

        public override async Task<AddResponse> Add(AddRequest request, ServerCallContext context)
        {

            var dto = new ToDoTaskDTO
            {
                Title = request.Title ?? string.Empty,
                Description = request.Description ?? string.Empty,
                ToDoTaskStatus = (Domain.Enums.ToDoTaskStatus)request.ToDoTaskStatus
                //ToDoTaskStatus = (Domain.Enums.ToDoTaskStatus)request.ToDoTaskStatus
            };

            var createdId = await _appService.AddAsync(dto);

            return new AddResponse { Id = createdId.ToString() };
            
        }

        public override async Task<GetAllResponse> GetAll(GetAllRequest request, ServerCallContext context)
        {
            var filter = new GetToDoTaskRequest
            {
                Title = string.IsNullOrWhiteSpace(request.Title) ? null : request.Title,
                Description = string.IsNullOrWhiteSpace(request.Description) ? null : request.Description,
                ToDoTaskStatus = (Domain.Enums.ToDoTaskStatus)request.ToDoTaskStatus,
                //CompletedAtFrom = request.CompletedAtFrom == Timestamp.FromDateTime(DateTime.MinValue) ? null : request.CompletedAtFrom.ToDateTime(),
                //CompletedAtTo= request.CompletedAtTo == Timestamp.FromDateTime(DateTime.MinValue) ? null : request.CompletedAtTo.ToDateTime(),
            };
            var data = await _appService.GetAllAsync(filter);

            var response = new GetAllResponse();
            foreach (var item in data.ToList())
            {
                response.Tasks.Add(new ToDoTask {
                    Id = item.Id.ToString(),
                    Title = item.Title,
                    Description = item.Description,
                    ToDoTaskStatus = (ToDoTaskStatus)item.ToDoTaskStatus, // Map application ToDoTaskStatus -> proto ToDoTaskStatus by numeric cast
                    CompletedAt = item.CompletedAt.HasValue ? Timestamp.FromDateTime(item.CompletedAt.Value) : null
                });
            }
            return response;
            
        }

        // optional server-stream implementation
        //public override async Task StreamAll(GetAllRequest request, IServerStreamWriter<ToDoTask> responseStream, ServerCallContext context)
        //{
        //    var appRequest = MapProtoToAppRequest(request);
        //    var dtos = await _appService.GetAllAsync(appRequest);

        //    foreach (var d in dtos)
        //    {
        //        if (context.CancellationToken.IsCancellationRequested) break;

        //        var msg = MapDtoToProto(d);

        //        await responseStream.WriteAsync(msg);
        //    }
        //}
    }
}