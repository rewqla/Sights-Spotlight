namespace API.Routes
{
    public static class AccountEndpoints
    {
        private const string BaseRoute = "api/accounts";

        public const string Register = $"{BaseRoute}/register";
        public const string Login = $"{BaseRoute}/login";
        public const string CurrentUser = $"{BaseRoute}/current-user";
    }
}
