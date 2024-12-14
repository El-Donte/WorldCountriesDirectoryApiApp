using Microsoft.EntityFrameworkCore;

namespace WorldCountriesDirectoryApiApp.Storage
{
    [Index(nameof(IsoAlpha2), IsUnique = true)]
    public class DbCountry
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string IsoAlpha2 { get; set; }

    }
}
