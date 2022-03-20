using Newtonsoft.Json;
using NUnit.Framework;
using RestApiCountries.Models;
using System.Collections.Generic;
using System.Linq;
using RestApiCountries.Filters;

namespace RestApiCountries.Tests
{
    public class CountryFilterTests
    {
        private readonly List<Country> _testCountries = new()
        {
            new Country
            {
                Name = "�land Islands",
                Area = 1580,
                Population = 28875,
                TopLevelDomain = new List<string> { ".ax" },
                NativeName = "�land",
                Independent = false
            },
            new Country
            {
                Name = "Austria",
                Area = 83871,
                Population = 8917205,
                TopLevelDomain = new List<string> { ".at" },
                NativeName = "�sterreich",
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
                    NativeName = "�sterreich",
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
            var returnedCountries = CountryFilters.GetIndependentEuCountries(_testCountries).ToList();

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
                    NativeName = "�sterreich",
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
                    Name = "�land Islands",
                    Area = 1580,
                    Population = 28875,
                    TopLevelDomain = new List<string> { ".ax" },
                    NativeName = "�land",
                    Independent = false
                }
            };

            //Act
            var returnedCountries = CountryFilters.ExtractTopTenCountriesByPopulation(_testCountries).ToList();

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
                    NativeName = "�sterreich",
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
                    Name = "�land Islands",
                    Area = 1580,
                    Population = 28875,
                    TopLevelDomain = new List<string> { ".ax" },
                    NativeName = "�land",
                    Independent = false
                }
            };

            //Act
            var returnedCountries = CountryFilters.ExtractTopTenCountriesByPopulationDensity(_testCountries).ToList();

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
            var isEuCountryNameValid = CountryFilters.IsValidEuCountryName(_testCountries, euCountryName);
            var isNonEuCountryNameValid = CountryFilters.IsValidEuCountryName(_testCountries, nonEuCountryName);

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
                NativeName = "�sterreich",
                Independent = true
            };

            //Act
            var countryWithoutName = CountryFilters.GetSingleCountryWithoutName(testCountry);

            //Assert
            Assert.AreEqual(JsonConvert.SerializeObject(expectedCountry), JsonConvert.SerializeObject(countryWithoutName));
        }
    }
}