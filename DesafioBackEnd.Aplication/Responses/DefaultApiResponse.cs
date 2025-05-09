namespace StockManager.Aplication.Responses;

public class DefaultApiResponse<T>
{
    public required string Message { get; set; }
    public required T Data { get; set; }
    public int Pages { get; set; }
    public int Totals { get; set; }
    public int CurrentPage { get; set; }
    public int NextPage { get; set; }
    public int PreviousPage { get; set; }
}