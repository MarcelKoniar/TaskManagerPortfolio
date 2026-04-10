using System;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;
using Application.Interfaces;
using Grpc.Core;
using GrpcService.Protos;
using Microsoft.Extensions.Logging;
using Domain.Enums;

namespace GrpcService.Services
{
    

    public class WorkTaskGrpcService : WorkTaskService.WorkTaskServiceBase
    {
        private readonly IWorkTaskService _appService;
        private readonly ILogger<WorkTaskGrpcService> _logger;

        public WorkTaskGrpcService(IWorkTaskService appService, ILogger<WorkTaskGrpcService> logger)
        {
            _appService = appService;
            _logger = logger;
        }

        private static WorkTask MapDtoToProto(WorkTaskDTO dto)
        {
            return new WorkTask
            {
                Id = dto.Id.ToString(),
                Title = dto.Title ?? string.Empty,
                Description = dto.Description ?? string.Empty,
                Status = dto.Status.ToString(),
                CompletedAt = dto.CompletedAt.HasValue ? dto.CompletedAt.Value.ToString("o") : string.Empty
            };
        }

        private static Application.DTO.GetWorkTaskRequest MapProtoToAppRequest(GetAllRequest proto)
        {
            var req = new Application.DTO.GetWorkTaskRequest
            {
                Title = string.IsNullOrWhiteSpace(proto.Title) ? null : proto.Title,
                Description = string.IsNullOrWhiteSpace(proto.Description) ? null : proto.Description,
            };

            if (Enum.TryParse<Domain.Enums.Status>(proto.Status, true, out var status))
                req.Status = status;

            if (DateTime.TryParse(proto.CompletedAtFrom, out var from))
                req.CompletedAtFrom = from;

            if (DateTime.TryParse(proto.CompletedAtTo, out var to))
                req.CompletedAtTo = to;

            return req;
        }

        public override async Task<GetAllResponse> GetAll(GetAllRequest request, ServerCallContext context)
        {
            var appRequest = MapProtoToAppRequest(request);
            var dtos = await _appService.GetAllAsync(appRequest);

            var response = new GetAllResponse();
            response.Tasks.AddRange(dtos.Select(d => MapDtoToProto(d)));

            return response;
        }

        // optional server-stream implementation
        public override async Task StreamAll(GetAllRequest request, IServerStreamWriter<WorkTask> responseStream, ServerCallContext context)
        {
            var appRequest = MapProtoToAppRequest(request);
            var dtos = await _appService.GetAllAsync(appRequest);

            foreach (var d in dtos)
            {
                if (context.CancellationToken.IsCancellationRequested) break;

                var msg = MapDtoToProto(d);

                await responseStream.WriteAsync(msg);
            }
        }
    }
}
