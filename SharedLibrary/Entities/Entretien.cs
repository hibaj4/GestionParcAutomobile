
namespace SharedLibrary.Entities
{
    public class Entretien 
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public double? NbrKm { get; set; }
        public DateTime? DateProchain { get; set; }

        public double? Cout { get; set; }
        public int VoitureId { get; set; }
        public Voiture? Voiture { get; set; }

        public int FournisseurId { get; set; }
        public Fournisseur? Fournisseur { get; set; }
    }
}
