
using System.Text.Json.Serialization;

namespace SharedLibrary.Entities
{
    public class Voiture
    {
        public int Id { get; set; }
        public string? Immatriculation { get; set; }
        public string? Marque { get; set; }
        public string? Modele { get; set; }
        public string? Couleur { get; set; }
        public string? TypeVehicule { get; set; }
        public int? AnneeMiseCirculation { get; set; }
        public string? TypeCarburant { get; set; }
        public double? Kilometrage { get; set; }
        public string? Origine { get; set; } 

        public bool AvecChauffeur { get; set; }
       


        [JsonIgnore]

        public List<Affectation>? Affectations { get; set; } = [];
        [JsonIgnore]
        public List<Entretien>? Entretiens { get; set; } = [];
        [JsonIgnore]
        public List<DocumentAdministratif>? DocumentAdministratifs { get; set; } = [];
   



    }
}
