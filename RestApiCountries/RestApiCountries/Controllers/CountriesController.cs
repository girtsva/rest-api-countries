using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refit;
using RestApiCountries.DataSource;
using RestApiCountries.Services;

namespace RestApiCountries.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IRestCountriesApi _countries;
        //private readonly CountryService _service;

        public CountriesController(IRestCountriesApi countries)
        {
            _countries = countries;
            //_service = service;
        }

        /// <summary>
        /// Gets the top 10 EU countries with the biggest population.
        /// </summary>
        [HttpGet]
        [Route("population/top10")]
        public async Task<IActionResult> GetTopTenCountriesByPopulation()
        {
            var allEuCountries = await _countries.GetEuCountries();
            var independentEuCountries = CountryService.GetIndependentEuCountries(allEuCountries);
            var topTenEuCountriesByPopulation =
                independentEuCountries.OrderByDescending(country => country.Population).Take(10);

            return Ok(topTenEuCountriesByPopulation);
        }

        /// <summary>
        /// Gets the top 10 EU countries with the biggest population density.
        /// </summary>
        [HttpGet]
        [Route("population-density/top10")]
        public async Task<IActionResult> GetTopTenCountriesByPopulationDensity()
        {
            var allEuCountries = await _countries.GetEuCountries();
            var independentEuCountries = CountryService.GetIndependentEuCountries(allEuCountries);
            var topTenEuCountriesByPopulationDensity =
                independentEuCountries.OrderByDescending(country => country.Population / country.Area).Take(10);

            return Ok(topTenEuCountriesByPopulationDensity);
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
            var allEuCountries = await _countries.GetEuCountries();
            try
            {
                var foundCountriesByName = await _countries.GetCountryByCountryName(name);
                var specifiedCountryName = foundCountriesByName.FirstOrDefault();
                var independentEuCountries = CountryService.GetIndependentEuCountries(allEuCountries);

                if (specifiedCountryName != null &&
                    CountryService.IsValidEuCountryName(independentEuCountries, specifiedCountryName))
                {
                    var country = CountryService.GetSingleCountry(specifiedCountryName);
                    return Ok(country);
                }
                    
            }
            catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return BadRequest($"There is no such EU country with the name \"{name}\"!");
            }


            return BadRequest($"There is no such EU country with the name \"{name}\"!");
        }
    }
}
