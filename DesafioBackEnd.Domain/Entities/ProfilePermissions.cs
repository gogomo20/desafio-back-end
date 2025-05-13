using StockManager.Domain.Basis;

namespace StockManager.Domain.Entities;

public class ProfilePermissions : BaseTable
{
    public long ProfileEntityId { get; set; }
    public long PermissionId { get; set; }
}