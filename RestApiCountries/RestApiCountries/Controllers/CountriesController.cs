using Microsoft.AspNetCore.Mvc;
using RestApiCountries.DataSource;
using RestApiCountries.Filters;

namespace RestApiCountries.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IRestCountriesApi _countries;
        
        public CountriesController(IRestCountriesApi countries)
        {
            _countries = countries;
        }

        /// <summary>
        /// Gets the top 10 EU countries with the biggest population.
        /// </summary>
        [HttpGet]
        [Route("population/top10")]
        public async Task<IActionResult> GetTopTenEuCountriesByPopulation()
        {
            var allEuCountries = await _countries.GetEuBlocCountries();
            var independentEuCountries = CountryFilters.GetIndependentEuCountries(allEuCountries);

            return Ok(CountryFilters.ExtractTopTenCountriesByPopulation(independentEuCountries));
        }

        /// <summary>
        /// Gets the top 10 EU countries with the biggest population density.
        /// </summary>
        [HttpGet]
        [Route("population-density/top10")]
        public async Task<IActionResult> GetTopTenCountriesByPopulationDensity()
        {
            var allEuCountries = await _countries.GetEuBlocCountries();
            var independentEuCountries = CountryFilters.GetIndependentEuCountries(allEuCountries);
            
            return Ok(CountryFilters.ExtractTopTenCountriesByPopulationDensity(independentEuCountries));
        }

        /// <summary>
        /// Returns information on the specified country (except for the country name).
        /// </summary>
        /// <response code="200">Success: returns the information on the specified country</response>
        /// <response code="400">Bad Request: if there is no such EU country with the specified name</response>
        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> GetCountryByName(string name)
        {
            var allEuCountries = await _countries.GetEuBlocCountries();
            var independentEuCountries = CountryFilters.GetIndependentEuCountries(allEuCountries);

            var tidyCountryName = name.ToLower().Trim();

            if (CountryFilters.IsValidEuCountryName(independentEuCountries, tidyCountryName))
            {
                var countriesFoundByName = await _countries.GetCountryByCountryName(tidyCountryName);
                var specifiedCountryName = countriesFoundByName.FirstOrDefault();

                return Ok(CountryFilters.GetSingleCountryWithoutName(specifiedCountryName!));
            }

            return BadRequest($"There is no such EU country with the name \"{name}\"!");
        }
    }
}
