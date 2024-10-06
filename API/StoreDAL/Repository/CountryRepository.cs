using Microsoft.EntityFrameworkCore;
using StoreDAL.Data;
using StoreDAL.Entities;
using StoreDAL.Interfaces;

namespace StoreDAL.Repository
{
    public class CountryRepository(StoreContext context) : GenericRepository<Country>(context), ICountryRepository
    {
        private readonly DbSet<Country> _countryDbSet = context.Set<Country>();

        public async Task<IEnumerable<Country>> GetAllCountriesWithSights(CancellationToken cancellationToken = default)
        {
            return await _countryDbSet
                .AsSplitQuery()
                .Include(s => s.Sights)
                .ThenInclude(sp => sp.SightPhotos)
                .ToListAsync(cancellationToken);
        }
        public  async Task<Country?> GetCountryByIdWithSights(int id, CancellationToken cancellationToken = default)
        {
            return await _countryDbSet
                .Include(s => s.Sights)
                .ThenInclude(sp => sp.SightPhotos)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        public override async Task<Country?> FindById(int id, CancellationToken cancellationToken = default)
        {
            return await _countryDbSet
                .Include(s => s.Sights)
                .ThenInclude(sp => sp.SightPhotos)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}