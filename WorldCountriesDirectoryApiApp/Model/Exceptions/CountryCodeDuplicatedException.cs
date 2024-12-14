namespace WorldCountriesDirectoryApiApp.Model.Exceptions
{
    public class CountryCodeDuplicatedException : ApplicationException
    {
        public CountryCodeDuplicatedException(string code) : base($"code '{code}' is duplicated") { }
    }
}
