namespace BeerSnob.Models
{
    public class BeerDTO
    {
        public long ID { get; set; }
        public string? Name { get; set; }
        public string? Brewery { get; set; }
        public float ABV { get; set; }
        public string? Style { get; set; }
        public string? IsGood { get; set; }

    }
}
