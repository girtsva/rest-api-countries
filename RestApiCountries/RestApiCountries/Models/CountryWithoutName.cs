namespace RestApiCountries.Models
{
    public class CountryWithoutName
    {
        public double Area { get; init; }
        public int Population { get; init; }
        public List<string>? TopLevelDomain { get; init; }
        public string? NativeName { get; init; }
        public bool Independent { get; init; }
    }
}
