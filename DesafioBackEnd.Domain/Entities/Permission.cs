using StockManager.Domain.Basis;

namespace StockManager.Domain.Entities;

public class Permission : BaseTable
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public long? PermissionGroupId { get; set; }
    public PermissionGroup? PermissionGroup { get; set; }
    
}