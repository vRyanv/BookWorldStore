namespace BookWorldStore.Repository
{
    public class OldOrderViewModel
    {
        public int id { get; set; }
        public string image { get; set; }
        public string title { get; set; }
        public int quantity { get; set; }
        public float total { get; set; }
        public DateTime date { get; set; }
        public float price { get; set; }

        public int status { get; set; }

        public int? orderid { get; set; }
        
        public int user_id { get; set; }
    }
}
