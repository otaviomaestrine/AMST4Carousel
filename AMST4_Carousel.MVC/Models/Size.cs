namespace AMST4_Carousel.MVC.Models
{
    public class Size
    {
        public Guid id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateData { get; set; } = DateTime.Now;

    }
}
