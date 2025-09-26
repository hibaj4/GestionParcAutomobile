using SharedLibrary.Entities;


namespace ServerLibrary.Repositories.Contracts
{
    public interface IAffectationRepository : IGenericRepositoryInterface<Affectation>
    {
        Task<Affectation?> GetAffectationActuelleByUtilisateurIdAsync(int utilisateurId);
    }

}


