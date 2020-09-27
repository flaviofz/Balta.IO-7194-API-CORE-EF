using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _7194SHOP.Models
{
    public class User
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter entre 3 e 20 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 20 caracteres")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter entre 3 e 20 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 20 caracteres")]
        public string Passaword { get; set; }

        public string Role { get; set; }
    }
}