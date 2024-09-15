namespace StoreBLL.Models
{
    public record GetAllSightsOptions
    {
        public string? Country { get; init; }
        public int? YearOfFoundationFrom { get; init; }
        public int? YearOfFoundationTo { get; init; }
        public string? SortField { get; init; }
    }
}
