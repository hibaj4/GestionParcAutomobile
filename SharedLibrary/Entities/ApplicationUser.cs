
namespace SharedLibrary.Entities
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string? NomComplet { get; set; }

        public string? Email { get; set; }
        public string? MotDePasse { get; set; }
    }
}
