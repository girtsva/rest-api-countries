using RestApiCountries.Models;

namespace RestApiCountries.Filters
{
    public static class CountryFilters
    {
        public static IEnumerable<Country> GetIndependentEuCountries(List<Country> countries)
        {
            return countries.Where(country => country.Independent);
        }

        public static IEnumerable<Country> ExtractTopTenCountriesByPopulation(IEnumerable<Country> countries)
        {
            return countries.OrderByDescending(country => country.Population).Take(10);
        }

        public static IEnumerable<Country> ExtractTopTenCountriesByPopulationDensity(IEnumerable<Country> countries)
        {
            return countries.OrderByDescending(country => country.Population / country.Area).Take(10);
        }

        public static bool IsValidEuCountryName(IEnumerable<Country> euCountries, string name)
        {
            return euCountries.Any(country => country.Name?.ToLower() == name);
        }

        public static CountryWithoutName GetSingleCountryWithoutName(Country country)
        {
            var singleCountry = new CountryWithoutName()
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
