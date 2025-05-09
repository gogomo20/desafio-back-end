using AutoMapper;
using MediatR;
using StockManager.Aplication.Responses;
using StockManager.Domain.Entities;
using StockManager.Repositories;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

namespace StockManager.Aplication.UseCases.PermissionGroups.Commands.Update
{
    public class UpdatePermissionGroupCommand : PermissionGroupBaseCommand
    {
        [JsonIgnore]
        public long Id { get; set; }
        public class UpdatePermissionGroupCommandHandler : IRequestHandler<UpdatePermissionGroupCommand, GenericResponse<long>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public UpdatePermissionGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<GenericResponse<long>> Handle(UpdatePermissionGroupCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<long>();
                try
                {
                    var permissionGroup = await _unitOfWork.GetRepositoryAsync<PermissionGroup>().FindAsync(request.Id);
                    if (permissionGroup == null)
                    {
                        throw new KeyNotFoundException("Permission group not found");
                    }
                    permissionGroup = _mapper.Map(request, permissionGroup);
                    _unitOfWork.GetRepositoryAsync<PermissionGroup>().Update(permissionGroup);
                    await _unitOfWork.CommitAsync();
                }
                catch (Exception ex)
                {
                    response.Errors = ["An error ocurred while updating the permission group"];
                }
                return response;
            }
        }
    }
}
