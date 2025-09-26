
using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.DTOs
{
    public class Register:AccountBase
    {
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string? NomComplet { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(MotDePasse))]
        [Required]
        public string? ConfirmMotDePasse { get; set; }



    }
}
