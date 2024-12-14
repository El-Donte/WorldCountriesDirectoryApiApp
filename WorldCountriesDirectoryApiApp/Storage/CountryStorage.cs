using Microsoft.EntityFrameworkCore;
using WorldCountriesDirectoryApiApp.Model;

namespace WorldCountriesDirectoryApiApp.Storage
{
    public class CountryStorage : ICountryStorage
    {
        private readonly ApplicationDbContext _db;

        public CountryStorage(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Country>> SelectAllAsync()
        {
            return await _db.Countries
                .Select(dbCountry => new Country
                {
                    ShortName = dbCountry.ShortName,
                    FullName = dbCountry.FullName,
                    IsoAlpha2 = dbCountry.IsoAlpha2
                }).ToListAsync();
        }

        public async Task<Country?> SelectByCodeAsync(string isoAlpha2)
        {
            DbCountry? found = await _db.Countries.FirstOrDefaultAsync(a => a.IsoAlpha2 == isoAlpha2);
            if (found == null)
            {
                return null;
            }
            return DbToModel(found);
        }

        public async Task<Country?> SelectByNameAsync(string name)
        {
            DbCountry? found = await _db.Countries.FirstOrDefaultAsync(a => a.ShortName == name || a.FullName == name);
            if (found == null)
            {
                return null;
            }
            return DbToModel(found);
        }

        public async Task InsertAsync(Country country)
        {
            DbCountry dbCountry = ModelToDb(country);
            await _db.Countries.AddAsync(dbCountry);
            await _db.SaveChangesAsync();
        }

        public async Task RemoveByCodeAsync(string isoAlpha2)
        {
            DbCountry? removed = await _db.Countries.FirstOrDefaultAsync(a => a.IsoAlpha2 == isoAlpha2);
            if (removed != null)
            {
                _db.Countries.Remove(removed);
                await _db.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(string isoAlpha2, Country country)
        {
            DbCountry? updated = await _db.Countries.FirstOrDefaultAsync(a => a.IsoAlpha2 == isoAlpha2);
            if (updated == null)
            {
                return;
            }

            updated.ShortName = country.ShortName;
            updated.FullName = country.FullName;
            updated.IsoAlpha2 = country.IsoAlpha2;

            await _db.SaveChangesAsync();
        }

        private DbCountry ModelToDb(Country country)
        {
            DbCountry dbCountry = new DbCountry()
            {
                Id = 0,
                ShortName = country.ShortName,
                FullName = country.FullName,
                IsoAlpha2 = country.IsoAlpha2
            };
            return dbCountry;
        }

        private Country DbToModel(DbCountry dbCountry)
        {
            Country airport = new Country()
            {
                ShortName = dbCountry.ShortName,
                FullName = dbCountry.FullName,
                IsoAlpha2 = dbCountry.IsoAlpha2
            };
            return airport;
        }
    }
}
