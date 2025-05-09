using AutoMapper;
using MediatR;
using StockManager.Aplication.Responses;
using StockManager.Domain.Entities;
using StockManager.Repositories;

namespace StockManager.Aplication.UseCases.Permissions.Commands.Create;

public class CreatePermissionCommand : PermissionCommandBase
{
    public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, GenericResponse<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePermissionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GenericResponse<long>> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            var response = new GenericResponse<long>();
            try
            {
                var permission = _mapper.Map<Permission>(request);
                await _unitOfWork.GetRepositoryAsync<Permission>().InsertAsync(permission);
                await _unitOfWork.CommitAsync();
                response.Data = permission.Id;
                response.Message = "Permission created successfully";
            }
            catch (Exception ex)
            {
                response.Errors = ["An error occurred while saving the permission"];
            }
            return response;
        }
    }
}