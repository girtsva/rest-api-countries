using RestApiCountries.DataSource;
using RestApiCountries.Models;

namespace RestApiCountries.Services
{
    public static class CountryService
    {
        //private readonly IRestCountriesApi _countries;
        public static IEnumerable<Country> GetIndependentEuCountries(List<Country> countries)
        {
            return countries.Where(country => country.Independent);
        }

        //public static IEnumerable<Country> 

        public static bool IsValidEuCountryName(IEnumerable<Country> euCountries, Country name)
        {
            return euCountries.Any(country => country.Name == name.Name);
        }

        public static TruncatedCountry GetSingleCountry(Country country)
        {
            var singleCountry = new TruncatedCountry()
            {
                Area = country.Area,
                Population = country.Population,
                TopLevelDomain = country.TopLevelDomain,
                NativeName = country.NativeName,
                Independent = country.Independent
            };

            return singleCountry;
        }
    }
}
