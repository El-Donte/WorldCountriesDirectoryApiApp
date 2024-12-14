using System.Text.RegularExpressions;
using WorldCountriesDirectoryApiApp.Model.Exceptions;

namespace WorldCountriesDirectoryApiApp.Model
{
    public class CountryScenarios
    {
        private readonly ICountryStorage _storage;

        public CountryScenarios(ICountryStorage storage)
        {
            _storage = storage;
        }

        //GetAll - получение списка всех стран.
        public async Task<List<Country>> GetAllAsync()
        {
            return await _storage.SelectAllAsync();
        }

        //Get(string isoAlpha2) - получение списка страны по коду alpha2.
        public async Task<Country> GetAsync(string isoAlpha2) 
        {
            ValidateCode(isoAlpha2);
            Country? country = await _storage.SelectByCodeAsync(isoAlpha2);
            if (country == null) 
            {
                throw new CountryNotFoundException(isoAlpha2);
            }
            return country;
        }

        //Add(Country country) - сохранение новой страны.Можно сохранять страну с уникальным валидным кодом,
        //непустыми уникальными названиями(могут совпадать полное и короткое).
        public async Task AddAsync(Country country)
        {
            // проверка кода
            ValidateCode(country.IsoAlpha2);
            ValidateName(country);

            // проверка дублирования кода
            Country? clone = await _storage.SelectByCodeAsync(country.IsoAlpha2);
            if (clone != null)
            {
                throw new CountryCodeDuplicatedException(country.IsoAlpha2);//добавить норм ошибку
            }

            clone = await _storage.SelectByNameAsync(country.FullName);
            if (clone != null) 
            { 
                throw new CountryNameDuplicatedException(country.FullName);
            }

            clone = await _storage.SelectByNameAsync(country.ShortName);
            if (clone != null) 
            {
                throw new CountryNameDuplicatedException(country.ShortName);
            }

            await _storage.InsertAsync(country);
        }

        //Update(string isoAlpha2, Country country) - редактирование страны по коду, можно редактировать
        //все поля кроме полей кодов,также проверять чтобы новые значения полей
        //не противоречили ограничениям из предыдущего метода.
        public async Task UpdateAsync(string isoAlpha2, Country country)
        {
            ValidateCode(isoAlpha2);
            ValidateName(country);

            Country? _country = await _storage.SelectByCodeAsync(isoAlpha2);
            if (_country == null)
            {
                throw new CountryNotFoundException(isoAlpha2);
            }

            _country = await _storage.SelectByCodeAsync(country.IsoAlpha2);
            if (_country != null)
            {
                throw new CountryCodeDuplicatedException(country.IsoAlpha2);
            }

            _country = await _storage.SelectByNameAsync(country.FullName);
            if (_country != null)
            {
                throw new CountryNameDuplicatedException(country.FullName);
            }

            _country = await _storage.SelectByNameAsync(country.ShortName);
            if (_country != null)
            {
                throw new CountryNameDuplicatedException(country.ShortName);
            }

            await _storage.UpdateAsync(isoAlpha2,country);
        }

        //DeleteAsync(string isoAlpha2) - удаление страны по коду.
        public async Task DeleteAsync(string isoAlpha2)
        {
            ValidateCode(isoAlpha2);
            Country? country = await _storage.SelectByCodeAsync(isoAlpha2);
            if (country == null)
            {
                throw new CountryNotFoundException(isoAlpha2);
            }
            await _storage.RemoveByCodeAsync(isoAlpha2);
        }

        private readonly Regex codeRegex = new Regex("^[A-Z]{2}$");

        private void ValidateCode(string code)
        {
            if (code == null)
            {
                throw new CountryCodeFormatException("the code is null");
            }
            if (!codeRegex.IsMatch(code))
            {
                throw new CountryCodeFormatException($"the code must contains two uppercase english letters, received '{code}'");
            }
        }

        private void ValidateName(Country country) 
        {
            if (country.FullName == null || country.ShortName == null)
            {
                throw new CountryNameFormatException("some name of country is null");
            }
            if (country.FullName == string.Empty || country.ShortName == string.Empty)
            {
                throw new CountryNameFormatException("some name of country is empty string");
            }
        }
    }
}
