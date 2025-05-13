using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StockManager.Aplication.DefaultRequest;
using StockManager.Aplication.JWTRepository;
using StockManager.Aplication.Responses;
using StockManager.Aplication.UseCases.Wallets.Responses;
using StockManager.Domain.Basis;
using StockManager.Domain.Entities;
using StockManager.Repositories;

namespace StockManager.Aplication.UseCases.Wallets.Queries;

public class ListTransference : DefaultQueryRequest<DefaultApiResponse<ICollection<TransferenceResponse>>>
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public class ListTransferenceQueryHandler : IRequestHandler<ListTransference, DefaultApiResponse<ICollection<TransferenceResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserContext _userContext;

        public ListTransferenceQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, UserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<DefaultApiResponse<ICollection<TransferenceResponse>>> Handle(ListTransference request,
            CancellationToken cancellationToken)
        {
            #region Filters
            var filters = new Filter<Transference>();
            filters.And(x => x.SenderId == _userContext.Id);
            if(request.StartDate != null)
                filters.And(x => x.CreatedAt >= request.StartDate);
            if(request.EndDate != null)
                filters.And(x => x.CreatedAt <= request.EndDate);
            #endregion

            var consulta = await _unitOfWork.GetRepositoryAsync<Transference>().ListPaginateAsync(
                predicate: filters.BuildExpression(),
                orderBy: request.OrderBy,
                ascending: request.Ascending,
                includes: x => x.Include(_ => _.Reciever),
                page: request.Page,
                size: request.Size);
            var data = _mapper.Map<ICollection<TransferenceResponse>>(consulta.Data);
            var response = new DefaultApiResponse<ICollection<TransferenceResponse>>
            {
                Message = "Transferences listed successfully",
                Data = data,
                Totals = consulta.TotalItems,
                Pages = consulta.TotalPages,
                CurrentPage = request.Page,
                NextPage = request.Page < consulta.TotalPages ? null : request.Page + 1,
                PreviousPage = request.Page > 0 ? null : request.Page - 1
            };
            return response;  
        }
    }
}