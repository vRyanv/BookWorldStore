using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookWorldStore.Models
{
    public class Supplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int sup_id { get; set; }

        [Required(ErrorMessage = "Supplier name is required")]
        public string name { get; set; }
        public string address { get; set; }
        public int status { get; set; }

        public virtual ICollection<Book> books { get; set; }
    }
}
