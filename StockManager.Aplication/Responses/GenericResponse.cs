namespace StockManager.Aplication.Responses;

public class GenericResponse<T>
{
    public bool Success => !Errors.Any();
    public T? Data { get; set; }
    public string? Message { get; set; }
    public string[] Errors { get; set; } = [];
}