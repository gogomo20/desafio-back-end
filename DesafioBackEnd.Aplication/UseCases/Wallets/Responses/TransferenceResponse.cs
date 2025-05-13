namespace StockManager.Aplication.UseCases.Wallets.Responses;

public class TransferenceResponse
{
    public long Id { get; set; }
    public required long ReceiverId { get; set; }
    public required String UserReciever { get; set; }
    public decimal Value { get; set; }
    public DateTime CreatedAt { get; set; }
}