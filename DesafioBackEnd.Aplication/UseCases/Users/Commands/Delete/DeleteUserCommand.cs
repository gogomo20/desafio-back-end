using MediatR;
using StockManager.Aplication.Responses;
using StockManager.Domain.Entities;
using StockManager.Repositories;

namespace StockManager.UseCases.UseCases.Users.Commands.Delete;

public class DeleteUserCommand : IRequest<GenericResponseNoData>
{
    public long Id { get; set; }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, GenericResponseNoData>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GenericResponseNoData> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var response = new GenericResponseNoData();
            try
            {
                var user = await _unitOfWork.GetRepositoryAsync<User>().FindAsync(request.Id);
                if (user == null)
                {
                    throw new KeyNotFoundException($"User with id {request.Id} not found");
                }
                _unitOfWork.GetRepositoryAsync<User>().Delete(user);
                await _unitOfWork.CommitAsync();
                response.Message = "User deleted successfully";
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case KeyNotFoundException:
                        throw;
                    default:
                        response.Errors = ["An error occurred while deleting the user"];
                        break;
                }
            }

            return response;
        }
    }
}