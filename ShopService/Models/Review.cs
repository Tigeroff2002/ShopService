﻿namespace ShopService.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public DateTime EventTime { get; set; }
        public virtual User? Client { get; set; }
        public virtual Product? Product { get; set; }
        public float? Rate { get; set; }
    }
}
