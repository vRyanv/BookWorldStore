namespace APIService.Models
{
    public class StatisticalViewModel
    {
        public int Id { get; set; }
        public string title { get; set; }
        public float totalPrice { get; set; }

        public DateTime? start { get; set; }

        public DateTime? end { get; set; }
    }
}
