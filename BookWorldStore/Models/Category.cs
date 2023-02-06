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
        public int name { get; set; }
        public int status { get; set; }

        public List<Book> books { get; set; }
        //something

    }
}
