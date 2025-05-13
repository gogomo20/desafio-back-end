using MediatR;

namespace StockManager.Aplication.DefaultRequest;

public class DefaultQueryRequest<TResponse> : IRequest<TResponse>
{
    public string OrderBy { get; set; } = "Id";
    public bool Ascending { get; set; } = true;
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
}