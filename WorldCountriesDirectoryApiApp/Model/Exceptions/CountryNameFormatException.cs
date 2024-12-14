namespace WorldCountriesDirectoryApiApp.Model.Exceptions
{
    public class CountryNameFormatException : ApplicationException
    {
        public CountryNameFormatException() : base($"country name format error") { }
        public CountryNameFormatException(string details) : base($"country name format error: {details}") { }
    }
}
