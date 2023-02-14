using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookWorldStore.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int cate_id { get; set; }

        [Required(ErrorMessage ="Category name is required")]
        [StringLength(20, ErrorMessage ="Category name can't over 20 character")]
        public string name { get; set; }
        public int status { get; set; }

        public virtual ICollection<Book> books { get; set; }

    }
}
