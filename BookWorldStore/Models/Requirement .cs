using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookWorldStore.Models
{
    public class Requirement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int req_id { get; set; }

        [ForeignKey("cate_id")]
        public Category category { get; set; }
        public string status { get; set; }
    }
}
