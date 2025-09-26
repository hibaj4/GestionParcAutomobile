

namespace SharedLibrary.Entities
{
    public class Trajet
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? LieuDepart { get; set; }
        public string? Destination { get; set; }
        public bool IsMission { get; set; }
        public double DistanceParcourue { get; set; }
        public int UtilisateurId { get; set; }
        public Utilisateur? Utilisateur { get; set; }


       
    }
}
