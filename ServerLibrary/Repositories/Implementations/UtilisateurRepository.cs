using Microsoft.EntityFrameworkCore;
using ServerLibrary.Repositories.Contracts;
using SharedLibrary.Entities;
using SharedLibrary.Responses;
using ServerLibrary.Data;



namespace ServerLibrary.Repositories.Implementations
{
    public class UtilisateurRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Utilisateur>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var utilisateur = await appDbContext.Utilisateurs.FindAsync(id);
            if (utilisateur is null) return NotFound();
            appDbContext.Utilisateurs.Remove(utilisateur);
            await Commit();
            return Success();
        }
     
        public async Task<List<Utilisateur>> GetAll() => await appDbContext.Utilisateurs.ToListAsync();
        public async Task<Utilisateur> GetById(int id) => await appDbContext.Utilisateurs.FindAsync(id);

        public async Task<GeneralResponse> Insert(Utilisateur item)
        {
            appDbContext.Utilisateurs.Add(item);
            await Commit();
            return Success();
        }



        public async Task<GeneralResponse> Update(Utilisateur item)
        {
            var utilisateur = await appDbContext.Utilisateurs.FindAsync(item.Id);
            if (utilisateur is null) return NotFound();
            utilisateur.Nom = item.Nom;
            utilisateur.Prenom = item.Prenom;
            utilisateur.CIN = item.CIN;
            utilisateur.Adresse = item.Adresse;
            utilisateur.Tel = item.Tel;
            utilisateur.Email = item.Email;
            utilisateur.Grade = item.Grade;
            utilisateur.Fonction = item.Fonction;
            utilisateur.SoldeCarburant = item.SoldeCarburant;
            utilisateur.Departement = item.Departement;


            await Commit();
            return Success();
        }
        private static GeneralResponse NotFound() => new(false, "Désolé, utilisateur non trouvée");
        private static GeneralResponse Success() => new(true, "Processus terminé");
        private async Task Commit() => await appDbContext.SaveChangesAsync();


       


    }




}


