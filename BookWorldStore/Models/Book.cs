using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookWorldStore.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int book_id { get; set; }

        [ForeignKey("cate_id")]
        public Category category { get; set; }

        [ForeignKey("sup_id")]
        public Supplier supplier { get; set; }
        public int inventory_num { get; set; }
        public string image { get; set; }
        public string title { get; set; }
        public string des { get; set; }
        public string author { get; set; }
        public DateTime publishing_year { get; set; }
        public float price { get; set; }

    }
}
