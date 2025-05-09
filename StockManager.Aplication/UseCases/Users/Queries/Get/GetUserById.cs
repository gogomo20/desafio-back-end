using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StockManager.Aplication.Responses;
using StockManager.Domain.Entities;
using StockManager.Repositories;
using StockManager.UseCases.UseCases.Users.Responses;

namespace StockManager.UseCases.UseCases.Users.Queries.Get;

public class GetUserById : IRequest<GenericResponse<UserResponse>>
{
    public long Id { get; set; }

    public class GetUserByIdHandler : IRequestHandler<GetUserById, GenericResponse<UserResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GenericResponse<UserResponse>> Handle(GetUserById request, CancellationToken cancellationToken)
        {
            var response = new GenericResponse<UserResponse>();
            try
            {
                var user = await _unitOfWork.GetRepositoryAsync<User>()
                    .SingleOrDefaultAsync(x => x.Id == request.Id);
                if (user == null)
                {
                    throw new KeyNotFoundException($"User with id {request.Id} not found");
                }
                response.Data = _mapper.Map<UserResponse>(user);
                response.Message = "User found successfully";
            }
            catch (Exception e)
            {
                response.Errors = ["An error occurred while getting the user"];
            }
            return response;
        }
    }
}