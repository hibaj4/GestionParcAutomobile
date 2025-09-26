

using System.Text.Json.Serialization;

namespace SharedLibrary.Entities
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? CIN { get; set; }
        public string? Adresse { get; set; }
        public string? Tel { get; set; }
        public string? Email { get; set; }
        public string? Grade { get; set; }
        public string? Fonction { get; set; }
        public double SoldeCarburant { get; set; } 
        public string? Departement { get; set; }
        [JsonIgnore]
        public List<Affectation>? Affectations { get; set; } = [];
        [JsonIgnore]
        public List<Trajet>? Trajets { get; set; } = [];
       



    }
}
