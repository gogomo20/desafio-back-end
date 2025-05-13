using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using StockManager.Aplication.DefaultRequest;
using StockManager.Aplication.Responses;
using StockManager.Domain.Basis;
using StockManager.Domain.Entities;
using StockManager.Repositories;
using StockManager.UseCases.UseCases.Users.Responses;

namespace StockManager.Aplication.UseCases.Users.Queries.List;

public class ListUserQuery : DefaultQueryRequest<DefaultApiResponse<ICollection<UserListResponse>>>
{
    public string? FilterName { get; set; }
    public string? FilterUsername { get; set; }

    public class ListUserQueryHandler : IRequestHandler<ListUserQuery, DefaultApiResponse<ICollection<UserListResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DefaultApiResponse<ICollection<UserListResponse>>> Handle(ListUserQuery request,
            CancellationToken cancellationToken)
        {
            #region Filters

            var filters = new Filter<User>();
            if (!string.IsNullOrEmpty(request.FilterName))
                filters.And(x => x.Name.Contains(request.FilterName));
            if(!string.IsNullOrEmpty(request.FilterUsername))
                filters.And(x => x.UserName.Contains(request.FilterUsername));
            #endregion

            var query = await _unitOfWork.GetRepositoryAsync<User>().ListPaginateAsync(
                predicate: filters.BuildExpression(),
                orderBy: request.OrderBy,
                ascending: request.Ascending,
                page: request.Page,
                size: request.Size,
                cancellationToken: cancellationToken);
            var data = _mapper.Map<ICollection<UserListResponse>>(query.Data);
            var response = new DefaultApiResponse<ICollection<UserListResponse>>
            {
                Message = "Users listed successfully",
                Data = data,
                Totals = query.TotalItems,
                Pages = query.TotalPages,
                CurrentPage = request.Page,
                NextPage = request.Page < query.TotalPages ? null : request.Page + 1,
                PreviousPage = request.Page > 0 ? null : request.Page - 1
            };
            return response;
        }
    }
}