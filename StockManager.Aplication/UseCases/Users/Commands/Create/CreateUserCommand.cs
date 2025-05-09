using AutoMapper;
using




    MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using StockManager.Aplication.Responses;
using StockManager.Aplication.Utils;
using StockManager.Domain.Entities;
using StockManager.Repositories;
using StockManager.UseCases.UseCases.Users.Commands;

namespace StockManager.Aplication.UseCases.Users.Commands.Create;

public class CreateUserCommand : UserCommandBase
{   
    public string? Password { get; set; }
    public ICollection<string> Permissions { get; set; } = [];
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, GenericResponse<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GenericResponse<long>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new GenericResponse<long>();
            try
            {
                var user = _mapper.Map<User>(request);
                user.Password = StringUtils.GetBcryptHash(request.Password);
                await _unitOfWork.GetRepositoryAsync<User>().InsertAsync(user);
                await _unitOfWork.CommitAsync();
                response.Data = user.Id;
                response.Message = "User created successfully";
            }
            catch (Exception e)
            {
                response.Errors = ["An Error occurred while creating the user"];
            }
            return response;
        }
    }
}