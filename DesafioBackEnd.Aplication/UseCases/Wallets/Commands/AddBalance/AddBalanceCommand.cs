using MediatR;
using StockManager.Aplication.JWTRepository;
using StockManager.Aplication.Responses;
using StockManager.Domain.Entities;
using StockManager.Repositories;

namespace StockManager.Aplication.UseCases.Wallets.Commands.AddBalance;

public class AddBalanceCommand : IRequest<GenericResponse<decimal>>
{
    public decimal Value { get; set; }

    public class AddBalanceCommandHandler : IRequestHandler<AddBalanceCommand, GenericResponse<decimal>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserContext _context;

        public AddBalanceCommandHandler(IUnitOfWork unitOfWork, UserContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<GenericResponse<decimal>> Handle(AddBalanceCommand request, CancellationToken cancellationToken)
        {
            var response = new GenericResponse<decimal>();
            try
            {
                var userId = _context.Id;
                var wallet = await _unitOfWork.GetRepositoryAsync<Wallet>()
                    .SingleOrDefaultAsync(x => x.UserId == userId);
                if (wallet == null)
                {
                    throw new KeyNotFoundException("Wallet not found");
                }
                wallet.Balance += request.Value;
                _unitOfWork.GetRepositoryAsync<Wallet>().Update(wallet);
                await _unitOfWork.CommitAsync();
                response.Data = wallet.Balance;
                response.Message = "Balance added successfully";
                return response;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case KeyNotFoundException:
                        throw;
                    default:
                        response.Errors = ["An error occurred while adding balance"];
                        break;
                }
            }

            return response;
        }
    }
}