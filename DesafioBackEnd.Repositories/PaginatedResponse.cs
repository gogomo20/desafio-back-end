namespace StockManager.Repositories;

public class PaginatedResponse<T>
{
    public int Page { get; set; }
    public int Size { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public ICollection<T> Data { get; set; } = [];
}