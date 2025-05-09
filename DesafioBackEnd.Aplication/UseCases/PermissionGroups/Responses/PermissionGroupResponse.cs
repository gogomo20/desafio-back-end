namespace StockManager.Aplication.UseCases.PermissionGroups.Responses
{
    public class PermissionGroupResponse
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public required string Status { get; set; }
    }
}
