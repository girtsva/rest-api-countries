using Refit;
using RestApiCountries.Models;

namespace RestApiCountries.DataSource
{
    public interface IRestCountriesApi
    {
        [Get("/v2/regionalbloc/eu")]
        Task<List<Country>> GetEuBlocCountries();

        [Get("/v2/name/{name}")]
        Task<List<Country>> GetCountryByCountryName(string name);
    }
}
