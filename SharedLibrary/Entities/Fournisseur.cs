
using System.Text.Json.Serialization;

namespace SharedLibrary.Entities
{
    public class Fournisseur
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Tel { get; set; }
        public string? Specialite { get; set; }
        [JsonIgnore]
        public List<DocumentAdministratif>? DocumentAdministratifs { get; set; } = [];
        [JsonIgnore]
        public List<Entretien>? Entretiens { get; set; } = [];



    }
}
