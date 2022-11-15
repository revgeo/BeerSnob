﻿namespace BeerSnob.Models
{
    public class Beer
    {
        public long ID { get; set; }
        public string? Name { get; set; }
        public string? Brewery { get; set; }
        public float ABV { get; set; }
        public string? Style { get; set; }
        public bool IsGood { get; set; }
        public string? DTOtest { get; set; }

    }
}
