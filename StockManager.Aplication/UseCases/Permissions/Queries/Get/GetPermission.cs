using System.Globalization;
using AutoMapper;
using MediatR;
using StockManager.Aplication.Responses;
using StockManager.Aplication.UseCases.Permissions.Responses;
using StockManager.Domain.Entities;
using StockManager.Repositories;
using KeyNotFoundException = System.Collections.Generic.KeyNotFoundException;

namespace StockManager.Aplication.UseCases.Permissions.Queries.Get;

public class GetPermission : IRequest<GenericResponse<PermissionResponse>>
{   
    public long Id { get; set; }

    public class GetPermissionHandler : IRequestHandler<GetPermission, GenericResponse<PermissionResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPermissionHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GenericResponse<PermissionResponse>> Handle(GetPermission query,
            CancellationToken cancellationToken)
        {
            var response = new GenericResponse<PermissionResponse>();
            try
            {
                var permission = await _unitOfWork.GetRepositoryAsync<Permission>().FindAsync(query.Id);
                if (permission == null)
                {
                    throw new KeyNotFoundException($"Permission with id {query.Id} not found");
                }
                response.Data = _mapper.Map<PermissionResponse>(permission);
                response.Message = "Permission found successfully";
            }
            catch (Exception e)
            {
                response.Errors = ["An error occurred while getting the permission"];
                throw;
            }
            return response;
        }
    }
}