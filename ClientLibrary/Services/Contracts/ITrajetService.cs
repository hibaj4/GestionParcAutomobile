using SharedLibrary.Entities;
using SharedLibrary.Responses;


namespace ClientLibrary.Services.Contracts
{
    public interface ITrajetService : IGenericServiceInterface<Trajet>
    {
        Task<List<Trajet>> GetByUtilisateurId(int utilisateurId);
        Task<GeneralResponse> AjouterTrajetAsync(Trajet trajet, string baseUrl);
        Task<GeneralResponse> ModifierTrajetAsync(Trajet trajet, string baseUrl);
        Task<GeneralResponse> SupprimerTrajetAsync(int id, string baseUrl);


    }
}
