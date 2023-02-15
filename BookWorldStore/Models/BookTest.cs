using System.ComponentModel.DataAnnotations;

namespace BookWorldStore.Models
{
    public class BookTest
    {
        [Required]
        public string title { get; set; }
        public string? imageFile { get; set; }
    }
}
    