

namespace SharedLibrary.Entities
{
    public class Affectation 
    {
        public int Id { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public int UtilisateurId { get; set; }
        public Utilisateur? Utilisateur { get; set; }

        public int VoitureId { get; set; }
        public Voiture? Voiture { get; set; }
   

    }
}
