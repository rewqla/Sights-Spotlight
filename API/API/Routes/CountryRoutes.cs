namespace API.Routes
{
    public static class CountryRoutes
    {
        private const string BaseRoute = "api/countries";

        public const string GetAll = $"{BaseRoute}/";
        public const string GetById = $"{BaseRoute}/{{id}}";
        public const string Create = $"{BaseRoute}";
        public const string Update = $"{BaseRoute}/{{id}}";
        public const string Delete = $"{BaseRoute}/{{id}}";
    }
}
