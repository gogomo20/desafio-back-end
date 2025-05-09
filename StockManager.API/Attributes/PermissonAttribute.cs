namespace StockManager.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class PermissionAttribute : Attribute
{
    public string Permission { get; set; }
    public PermissionAttribute(string permission) => Permission = permission;
}