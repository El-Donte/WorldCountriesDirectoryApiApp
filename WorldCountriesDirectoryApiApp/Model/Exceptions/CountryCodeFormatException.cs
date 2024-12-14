namespace WorldCountriesDirectoryApiApp.Model.Exceptions
{
    public class CountryCodeFormatException : ApplicationException
    {
        public CountryCodeFormatException() : base($"country code format error") { }
        public CountryCodeFormatException(string details) : base($"country code format error: {details}") { }
    }
}
