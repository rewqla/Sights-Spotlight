namespace API.Routes
{
    public static class CountryRoutes
    {
        private const string BaseRoute = "api/countries";

        public const string GetAll = $"{BaseRoute}/";
        public const string GetById = $"{BaseRoute}/{{id}}";
        public const string Create = $"{BaseRoute}/create";
        public const string Update = $"{BaseRoute}/update/{{id}}";
    }
}
