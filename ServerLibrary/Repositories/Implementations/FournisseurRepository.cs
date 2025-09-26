using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using SharedLibrary.Entities;
using SharedLibrary.Responses;


namespace ServerLibrary.Repositories.Implementations
{
   
    public class FournisseurRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Fournisseur>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var fournisseur = await appDbContext.Fournisseurs.FindAsync(id);
            if (fournisseur is null) return NotFound();
            appDbContext.Fournisseurs.Remove(fournisseur);
            await Commit();
            return Success();
        }

        public async Task<List<Fournisseur>> GetAll() => await appDbContext.Fournisseurs.ToListAsync();
        public async Task<Fournisseur> GetById(int id) => await appDbContext.Fournisseurs.FindAsync(id);

        public async Task<GeneralResponse> Insert(Fournisseur item)
        {
            var checkIfnull = await CheckName(item.Nom!);
            if (!checkIfnull)
                return new GeneralResponse(false, "Fournisseur existe déjà avec ce nom");
            appDbContext.Fournisseurs.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Fournisseur item)
        {
            var fournisseur = await appDbContext.Fournisseurs.FindAsync(item.Id);
            if (fournisseur is null) return NotFound();




            var fournisseurExistant = await appDbContext.Fournisseurs.FindAsync(item.Id);
            if (fournisseurExistant is null)
                return NotFound();

            if (fournisseurExistant.Nom != item.Nom)
            {
                bool nomDisponible = await CheckName(item.Nom!);
                if (!nomDisponible)
                    return new GeneralResponse(false, "Un fournisseur avec ce nom existe déjà");
            }



            fournisseur.Nom = item.Nom;
            fournisseur.Tel = item.Tel;
            fournisseur.Specialite = item.Specialite;
            await Commit();
            return Success();
        }
        private static GeneralResponse NotFound() => new(false, "Désolé, fournisseur non trouvé");
        private static GeneralResponse Success() => new(true, "Processus terminé");
        private async Task Commit() => await appDbContext.SaveChangesAsync();
        private async Task<bool> CheckName(string nom)
        {
            var item = await appDbContext.Fournisseurs.FirstOrDefaultAsync(x => x.Nom == nom);
            return item is null;

        }

    }

}

