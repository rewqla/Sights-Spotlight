using StoreDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBLL.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(User user);
    }
}
