namespace WorldCountriesDirectoryApiApp.Model
{
    public interface ICountryStorage
    {
        Task<List<Country>> SelectAllAsync();

        Task<Country?> SelectByCodeAsync(string isoAlpha2);

        Task<Country?> SelectByNameAsync(string name);

        Task InsertAsync(Country country);

        Task RemoveByCodeAsync(string isoAlpha2);

        Task UpdateAsync(string isoAlpha2,Country country);
    }
}
