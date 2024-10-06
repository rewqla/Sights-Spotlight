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
    public class SightsRepository(StoreContext context) : GenericRepository<Sight>(context), ISightRepository
    {
        private readonly DbSet<Sight> _sightDbSet = context.Set<Sight>();

        public async Task<IEnumerable<Sight>> GetAllSightsWithCountry(CancellationToken cancellationToken = default)
        {
            return await _sightDbSet
                .Include(s => s.Country)
                .Include(s => s.SightPhotos)
                .AsSplitQuery()
                .ToListAsync(cancellationToken);
        }

        public async Task<int> GetCountAsync(string? country, int? yearOfFoundation, CancellationToken token = default)
        {
            var query = _sightDbSet.AsQueryable();

            if (!string.IsNullOrEmpty(country))
            {
                query = query.Where(s => s.Country.Name.Contains(country, StringComparison.OrdinalIgnoreCase));
            }

            if (yearOfFoundation.HasValue)
            {
                query = query.Where(s => s.YearOfFoundation == yearOfFoundation.Value);
            }

            return await query.CountAsync(token);
        }
    }
}
