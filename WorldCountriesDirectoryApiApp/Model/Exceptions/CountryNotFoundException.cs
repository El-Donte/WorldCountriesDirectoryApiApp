namespace WorldCountriesDirectoryApiApp.Model.Exceptions
{
    public class CountryNotFoundException : ApplicationException
    {
        public CountryNotFoundException() : base("country is not found") { }
        public CountryNotFoundException(string code) : base($"country '{code}' is not found") { }
    }
}
