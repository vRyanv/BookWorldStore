using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIService.Models
{
    [Keyless]
    public class User
    {
        public int user_id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string? role { get; set; }
        public string? token_reset_pass { get; set; }
        [NotMapped]public virtual ICollection<Order>? orders { get; set; }
    }
}
