using MediatR;
using StockManager.Aplication.JWTRepository;
using StockManager.Aplication.Responses;
using StockManager.Domain.Entities;
using StockManager.Repositories;

namespace StockManager.Aplication.UseCases.Wallets.Commands.TransferValue;

public class TransferValueCommand : IRequest<GenericResponse<decimal>>
{
    public decimal Value { get; set; }
    public long UserIdDestiny { get; set; }

    public class TransferValueCommandHandler : IRequestHandler<TransferValueCommand, GenericResponse<decimal>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserContext _userContext;

        public TransferValueCommandHandler(IUnitOfWork unitOfWork, UserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<GenericResponse<decimal>> Handle(TransferValueCommand request,
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
                var walletDestiny = await _unitOfWork.GetRepositoryAsync<Wallet>()
                    .SingleOrDefaultAsync(x => x.UserId == request.UserIdDestiny);
                if (walletDestiny == null)
                {
                    throw new KeyNotFoundException("Wallet destiny not found");
                }
                wallet.Balance -= request.Value;
                walletDestiny.Balance += request.Value;
                _unitOfWork.GetRepositoryAsync<Wallet>().Update(wallet);
                _unitOfWork.GetRepositoryAsync<Wallet>().Update(walletDestiny);
                var transference = new Transference
                {
                    ReceiverId = request.UserIdDestiny,
                    SenderId = userId,
                    Value = request.Value
                };
                await _unitOfWork.GetRepositoryAsync<Transference>().InsertAsync(transference);
                await _unitOfWork.CommitAsync();
                response.Data = wallet.Balance;
                response.Message = "Treansfer value successfully";
            }
            catch (Exception ex)
            {
                switch (ex)
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