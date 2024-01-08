using System.ComponentModel.DataAnnotations;

namespace InternetShop.Models
{
    public class User
    {
        public string? Name { get; set; }
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Password { get; set; }
        public double Balance { get; set; }
    }
}
