using System.ComponentModel.DataAnnotations;

namespace StoreDAL.Entities;

public class AuditEntry
{
    [Key] public Guid Id { get; set; }
    public string Metadata { get; set; } = String.Empty;
    public string EntityName { get; set; } = String.Empty;
    public string TrailType { get; set; } = String.Empty;
    public DateTime StartTimeUTC { get; set; }
    public DateTime EndTimeUTC { get; set; }
    public bool Succeed { get; set; }
    public string ErrorMessage { get; set; } = String.Empty;
}