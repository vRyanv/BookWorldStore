using BookWorldStore.Models;
namespace BookWorldStore.Models.Repository
{
    public class BookViewModel
    {
        public int book_id { get; set; }
        public string title { get; set; }
        public string des { get; set; }
        public string author { get; set; }
        public int inventory_num { get; set; }
        public string image { get; set; }
        public DateTime publishing_year { get; set; }
        public float price { get; set; }
        public int cate_id { get; set; }
        public string cate_name { get; set; }
        public int sup_id { get; set; }
        public string sup_name { get; set;}

        public BookViewModel() { }


    }
}
