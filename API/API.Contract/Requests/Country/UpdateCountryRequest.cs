namespace API.Contract.Requests.Country
{
    public record UpdateCountryRequest : CreateCountryRequest
    {
        public required int Id { get; init; }
    }
}
