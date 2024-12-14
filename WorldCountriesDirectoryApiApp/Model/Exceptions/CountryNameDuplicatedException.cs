namespace WorldCountriesDirectoryApiApp.Model.Exceptions
{
    public class CountryNameDuplicatedException : ApplicationException
    {
        public CountryNameDuplicatedException(string name) : base($"name '{name}' is duplicated") { }
    }
}
