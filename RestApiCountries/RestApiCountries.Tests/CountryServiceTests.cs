using Newtonsoft.Json;
using NUnit.Framework;
using RestApiCountries.Models;
using RestApiCountries.Services;
using System.Collections.Generic;
using System.Linq;

namespace RestApiCountries.Tests
{
    public class CountryServiceTests
    {
        private readonly List<Country> _testCountries = new()
        {
            new Country
            {
                Name = "Åland Islands",
                Area = 1580,
                Population = 28875,
                TopLevelDomain = new List<string> { ".ax" },
                NativeName = "Åland",
                Independent = false
            },
            new Country
            {
                Name = "Austria",
                Area = 83871,
                Population = 8917205,
                TopLevelDomain = new List<string> { ".at" },
                NativeName = "Österreich",
                Independent = true
            },
            new Country
            {
                Name = "Croatia",
                Area = 56594,
                Population = 4047200,
                TopLevelDomain = new List<string> { ".hr" },
                NativeName = "Hrvatska",
                Independent = true
            }
        };

        [Test]
        public void GetIndependentEuCountries_ShouldReturnIndependentCountries()
        {
            //Arrange
            var expectedCountries = new List<Country>
            {
                new Country
                {
                    Name = "Austria",
                    Area = 83871,
                    Population = 8917205,
                    TopLevelDomain = new List<string> {".at"},
                    NativeName = "Österreich",
                    Independent = true
                },
                new Country
                {
                    Name = "Croatia",
                    Area = 56594,
                    Population = 4047200,
                    TopLevelDomain = new List<string> {".hr"},
                    NativeName = "Hrvatska",
                    Independent = true
                }
            };

            //Act
            var returnedCountries = CountryService.GetIndependentEuCountries(_testCountries).ToList();

            //Assert
            Assert.AreEqual(JsonConvert.SerializeObject(expectedCountries), JsonConvert.SerializeObject(returnedCountries));
        }

        [Test]
        public void ExtractTopTenCountriesByPopulation_ShouldReturnRankedCountriesByPopulation()
        {
            //Arrange
            var expectedCountries = new List<Country>
            {
                new Country
                {
                    Name = "Austria",
                    Area = 83871,
                    Population = 8917205,
                    TopLevelDomain = new List<string> {".at"},
                    NativeName = "Österreich",
                    Independent = true
                },
                new Country
                {
                    Name = "Croatia",
                    Area = 56594,
                    Population = 4047200,
                    TopLevelDomain = new List<string> {".hr"},
                    NativeName = "Hrvatska",
                    Independent = true
                },
                new Country
                {
                    Name = "Åland Islands",
                    Area = 1580,
                    Population = 28875,
                    TopLevelDomain = new List<string> { ".ax" },
                    NativeName = "Åland",
                    Independent = false
                }
            };

            //Act
            var returnedCountries = CountryService.ExtractTopTenCountriesByPopulation(_testCountries).ToList();

            //Assert
            Assert.AreEqual(JsonConvert.SerializeObject(expectedCountries), JsonConvert.SerializeObject(returnedCountries));
        }

        [Test]
        public void ExtractTopTenCountriesByPopulationDensity_ShouldReturnRankedCountriesByPopulationDensity()
        {
            //Arrange
            var expectedCountries = new List<Country>
            {
                new Country
                {
                    Name = "Austria",
                    Area = 83871,
                    Population = 8917205,
                    TopLevelDomain = new List<string> {".at"},
                    NativeName = "Österreich",
                    Independent = true
                },
                new Country
                {
                    Name = "Croatia",
                    Area = 56594,
                    Population = 4047200,
                    TopLevelDomain = new List<string> {".hr"},
                    NativeName = "Hrvatska",
                    Independent = true
                },
                new Country
                {
                    Name = "Åland Islands",
                    Area = 1580,
                    Population = 28875,
                    TopLevelDomain = new List<string> { ".ax" },
                    NativeName = "Åland",
                    Independent = false
                }
            };

            //Act
            var returnedCountries = CountryService.ExtractTopTenCountriesByPopulationDensity(_testCountries).ToList();

            //Assert
            Assert.AreEqual(JsonConvert.SerializeObject(expectedCountries), JsonConvert.SerializeObject(returnedCountries));
        }

        [Test]
        public void IsValidEuCountryName_ShouldReturnTrueForEuCountryNameAndFalseForNonEuCountryName()
        {
            //Arrange
            var euCountryName = "croatia";
            var nonEuCountryName = "morocco";

            //Act
            var isEuCountryNameValid = CountryService.IsValidEuCountryName(_testCountries, euCountryName);
            var isNonEuCountryNameValid = CountryService.IsValidEuCountryName(_testCountries, nonEuCountryName);

            //Assert
            Assert.True(isEuCountryNameValid);
            Assert.False(isNonEuCountryNameValid);
        }

        [Test]
        public void GetSingleCountryWithoutName_ShouldReturnCountryWithAllDataExceptName()
        {
            //Arrange
            var testCountry = _testCountries[1];
            var expectedCountry = new CountryWithoutName()
            {
                Area = 83871,
                Population = 8917205,
                TopLevelDomain = new List<string> {".at"},
                NativeName = "Österreich",
                Independent = true
            };

            //Act
            var countryWithoutName = CountryService.GetSingleCountryWithoutName(testCountry);

            //Assert
            Assert.AreEqual(JsonConvert.SerializeObject(expectedCountry), JsonConvert.SerializeObject(countryWithoutName));
        }
    }
}