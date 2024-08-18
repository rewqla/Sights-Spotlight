using API.Contract.Requests;
using API.Contract.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBLL.Interfaces
{
    public interface ISightService
    {
        Task<IEnumerable<SightsResponse>> GetAllSights(CancellationToken cancellationToken = default);
    }
}
