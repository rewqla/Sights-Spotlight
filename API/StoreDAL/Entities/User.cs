using Microsoft.AspNetCore.Identity;

namespace StoreDAL.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName {  get; set; }
        public string LastName {  get; set; }
    }
}
