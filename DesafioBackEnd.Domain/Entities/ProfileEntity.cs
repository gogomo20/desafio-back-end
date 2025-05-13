using System.ComponentModel.DataAnnotations.Schema;
using StockManager.Domain.Basis;

namespace StockManager.Domain.Entities;

[Table("profile")]
public class ProfileEntity : BaseTable
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public virtual ICollection<Permission> Permissions { get; set; } = [];
}