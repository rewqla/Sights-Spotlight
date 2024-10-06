using StoreDAL.Entities;

namespace StoreBLL.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(User user);
    }
}
