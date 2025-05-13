using MediatR;
using StockManager.Aplication.JWTRepository;
using StockManager.Aplication.Responses;
using StockManager.Domain.Entities;
using StockManager.Repositories;

namespace StockManager.Aplication.UseCases.Wallets.Queries;

public class GetBalance : IRequest<GenericResponse<decimal>>
{
    public class GetBalanceHandler : IRequestHandler<GetBalance, GenericResponse<decimal>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserContext _userContext;

        public GetBalanceHandler(IUnitOfWork unitOfWork, UserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<GenericResponse<decimal>> Handle(GetBalance request, CancellationToken cancellationToken)
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
                response.Data = wallet.Balance;
                response.Message = "Balance retrieved successfully";
                return response;
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case KeyNotFoundException:
                        throw;
                    default:
                        response.Errors = ["An error occurred while getting the balance"];
                        break;
                }
            }
            return response;
        }
    }
}