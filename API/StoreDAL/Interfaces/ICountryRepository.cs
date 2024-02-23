using StoreDAL.Entities;
using StoreDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDAL.Interfaces
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
    }
}
