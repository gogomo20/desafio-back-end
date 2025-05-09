using System.Text.Json.Serialization;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StockManager.Aplication.Responses;
using StockManager.Aplication.Utils;
using StockManager.Domain.Entities;
using StockManager.Repositories;

namespace StockManager.UseCases.UseCases.Users.Commands.Update;

public class UpdateUserCommand : UserCommandBase
{
    [JsonIgnore]
    public long Id { get; set; }
    public string? NewPassword { get; set; }
    public ICollection<string>? NewPermissions { get; set; }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, GenericResponse<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GenericResponse<long>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new GenericResponse<long>();
            try
            {
                var user = await _unitOfWork.GetRepositoryAsync<User>()
                                            .SingleOrDefaultAsync(x => x.Id == request.Id);
                if (user == null)
                {
                    throw new KeyNotFoundException($"User with id {request.Id} not found");
                }
                user = _mapper.Map(request, user);
                if (request.NewPassword != null)
                {
                    user.Password = StringUtils.GetBcryptHash(request.NewPassword);
                }
                _unitOfWork.GetRepositoryAsync<User>().Update(user);
                await _unitOfWork.CommitAsync();
                response.Data = user.Id;
                response.Message = "User updated successfully";
            }
            catch (Exception e)
            {
                response.Errors = ["An error occurred while updating the user"];
            }

            return response;
        }
        public async Task<ICollection<Permission>> GetUserPermissions(ICollection<string> permissions,
            CancellationToken cancellationToken)
        {
            return await _unitOfWork.GetRepositoryAsync<Permission>()
                .GetQueryable()
                .Where(x => permissions.Contains(x.Name))
                .ToListAsync(cancellationToken);
        }
    } 
}