namespace StockManager.Aplication.Responses;

public class GenericResponseNoData
{
    public bool Success => !Errors.Any();
    public string? Message { get; set; }
    public string[] Errors { get; set; } = [];
}