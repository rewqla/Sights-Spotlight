namespace API.Authorization
{
    public static class PolicyClaims
    {
        public const string Admin = "Admin";
        public const string Member = "Member";
        public const string Viewer = "Viewer";
        public const string ClaimPath = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
    }
}
