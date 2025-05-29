namespace WebApplication1.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Contents { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int CategoryId { get; set; }
    }
}
