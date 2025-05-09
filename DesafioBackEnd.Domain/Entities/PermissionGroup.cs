using StockManager.Domain.Basis;

namespace StockManager.Domain.Entities
{
    public class PermissionGroup : BaseTable
    {
        public required string Name { get; set; }
        public required string Status { get; set; }
    }
}
