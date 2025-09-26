using SharedLibrary.Entities;


namespace ServerLibrary.Repositories.Contracts
{
    public interface ITrajetRepository : IGenericRepositoryInterface<Trajet>
    {
        Task<List<Trajet>> GetByUtilisateurId(int utilisateurId);
    }
}
