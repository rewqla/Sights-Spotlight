namespace API.Routes
{
    public static class CountryRoutes
    {
        private const string BaseRoute = "api/country";

        public const string GetAll = $"{BaseRoute}/";
        public const string GetById = $"{BaseRoute}/{{id}}";
    }
}
