namespace RestApiCountries.Models
{
    public class Country
    {
        public string? Name { get; init; }
        public double Area { get; init; }
        public int Population { get; init; }
        public List<string>? TopLevelDomain { get; init; }
        public string? NativeName { get; init; }
        public bool Independent { get; init; }
    }
}
