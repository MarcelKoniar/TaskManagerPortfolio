using Application.DTO;
using Application.Interfaces;
using Application.Services;
using Domain.EntityModels;
using Domain.Enums;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;


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
                // Map proto ToDoTaskStatus -> application ToDoTaskStatus by numeric cast (keeps mapping stable if enums share values)
                //ToDoTaskStatus = (Domain.Enums.ToDoTaskStatus)request.ToDoTaskStatus
            };

            var createdId = await _appService.AddAsync(dto);

            return new AddResponse { Id = createdId.ToString() };
            
        }
    }
}