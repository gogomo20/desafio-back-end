using MediatR;
using StockManager.Aplication.JWTRepository;
using StockManager.Aplication.Responses;
using StockManager.Domain.Entities;
using StockManager.Repositories;

namespace StockManager.Aplication.UseCases.Wallets.Commands.RemoveBallance;

public class RemoveBalanceCommand : IRequest<GenericResponse<decimal>>
{
    public decimal Value { get; set; }

    public class RemoveBalanceCommandHandler : IRequestHandler<RemoveBalanceCommand, GenericResponse<decimal>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserContext _userContext;

        public RemoveBalanceCommandHandler(IUnitOfWork unitOfWork, UserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<GenericResponse<decimal>> Handle(RemoveBalanceCommand request,
            CancellationToken cancellationToken)
        {
            var response = new GenericResponse<decimal>();
            try
            {
                var userId = _userContext.Id;
                var wallet = await _unitOfWork.GetRepositoryAsync<Wallet>()
                    .SingleOrDefaultAsync(x => x.UserId == userId);
                if (wallet == null)
                {
                    throw new KeyNotFoundException("Wallet not found");
                }
                wallet.Balance -= request.Value;
                _unitOfWork.GetRepositoryAsync<Wallet>().Update(wallet);
                await _unitOfWork.CommitAsync();
                response.Data = wallet.Balance;
                response.Message = "Balance removed successfully";
                return response;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case KeyNotFoundException:
                        throw;
                    default:
                        response.Errors = ["An error occurred while removing balance"];
                        break;
                }
            }

            return response;
        }
    }
}