using Microsoft.EntityFrameworkCore;

namespace APIService.Models
{
    [Keyless]
    public class UserViewModel
    {
        public int user_id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string role { get; set; }
    }
}
