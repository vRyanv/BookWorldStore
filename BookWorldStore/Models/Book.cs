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
        [Required(ErrorMessage = "Category is required")]
        public Category category { get; set; }

        [ForeignKey("sup_id")]
        [Required(ErrorMessage = "Supplier is required")]
        public Supplier supplier { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string des { get; set; }
        [Required(ErrorMessage = "Author is required")]
        public string author { get; set; }

        [Required(ErrorMessage = "quantity is required")]
        public int inventory_num { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public string image { get; set; }

        [Required(ErrorMessage = "Publishing year is required")]
        public string publishing_year { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public float price { get; set; }
    }
}
