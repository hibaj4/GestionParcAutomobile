

namespace SharedLibrary.Entities
{
    public class DocumentAdministratif
    {
        public int Id { get; set; }
        public DateTime DateExpirationAssurance { get; set; }
        public DateTime DateAssurance { get; set; }

        public DateTime DateExpirationTaxe { get; set; }
        public DateTime DateTaxe { get; set; }

        public DateTime DateProchaineVisite { get; set; }
        public DateTime DateDerniereVisite { get; set; }

        public double MontantAssurance { get; set; }
        public double MontantTaxe { get; set; }
        public double MontantVisiteTechnique { get; set; }
        public int VoitureId { get; set; }
        public Voiture? Voiture { get; set; }

        public int FournisseurId { get; set; }
        public Fournisseur? Fournisseur { get; set; }
    }
}
