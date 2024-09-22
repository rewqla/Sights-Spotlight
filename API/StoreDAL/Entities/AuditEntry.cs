using System.ComponentModel.DataAnnotations;

namespace StoreDAL.Entities;

public class AuditEntry
{
    [Key] public Guid Id { get; set; }
    public string Metadata { get; set; } = String.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool Succeed { get; set; }
    public string ErrorMessage { get; set; }= String.Empty;
}