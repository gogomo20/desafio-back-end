using AutoMapper;
using MediatR;
using StockManager.Aplication.Responses;
using StockManager.Domain.Entities;
using StockManager.Repositories;

namespace StockManager.Aplication.UseCases.PermissionGroups.Commands.Create
{
    public class CreatePermissionGroupCommand : PermissionGroupBaseCommand
    {
        public class CreatePermissionGroupCommandHandler : IRequestHandler<CreatePermissionGroupCommand, GenericResponse<long>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public CreatePermissionGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<GenericResponse<long>> Handle(CreatePermissionGroupCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<long>();
                try
                {
                    var permissionGroup = _mapper.Map<PermissionGroup>(request);
                    await _unitOfWork.GetRepositoryAsync<PermissionGroup>().InsertAsync(permissionGroup);
                    await _unitOfWork.CommitAsync();
                    response.Message = "Permission group created successfully!";
                    response.Data = permissionGroup.Id;
                }
                catch (Exception ex)
                {
                    response.Errors = ["An error occurred while creating permission group"];
                }
                return response;
            }
        }
    }
}
