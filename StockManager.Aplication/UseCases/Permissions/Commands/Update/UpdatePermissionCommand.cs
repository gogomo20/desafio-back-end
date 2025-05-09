using System.Text.Json.Serialization;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using StockManager.Aplication.Responses;
using StockManager.Domain.Entities;
using StockManager.Repositories;

namespace StockManager.Aplication.UseCases.Permissions.Commands.Update;

public class UpdatePermissionCommand : PermissionCommandBase
{
    [JsonIgnore]
    public long Id { get; set; }

    public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand, GenericResponse<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePermissionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GenericResponse<long>> Handle(UpdatePermissionCommand request,
            CancellationToken cancellationToken)
        {
            var response = new GenericResponse<long>();
            try
            {
                var permission = await _unitOfWork.GetRepositoryAsync<Permission>().FindAsync(request.Id);
                if (permission == null)
                {
                    throw new KeyNotFoundException($"Permission with id {request.Id} not found");
                }

                permission = _mapper.Map(request, permission);
                _unitOfWork.GetRepositoryAsync<Permission>().Update(permission);
                await _unitOfWork.CommitAsync();
                response.Data = permission.Id;
                response.Message = "Permission updated successfully";
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}