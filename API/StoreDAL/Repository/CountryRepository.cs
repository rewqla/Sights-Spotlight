using Microsoft.EntityFrameworkCore;
using StoreDAL.Data;
using StoreDAL.Entities;
using StoreDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDAL.Repository
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        private readonly DbSet<Country> _countryDbSet;
        public CountryRepository(StoreContext context) : base(context)
        {
            _countryDbSet = context.Set<Country>();
        }

        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            return await _countryDbSet.Include(s => s.Sights).ThenInclude(p => p.SightPhotos).ToListAsync();
        }

        public async Task<Country> GetCountryById(int id)
        {
            return await _countryDbSet.Include(s => s.Sights).ThenInclude(p => p.SightPhotos).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
