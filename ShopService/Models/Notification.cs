namespace ShopService.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public virtual User? Recipient { get; set; }
        public DateTime EventTime { get; set; }

        public Notification()
        {
            Id = 1;
            EventTime = DateTime.Now;
        }
    }
}
