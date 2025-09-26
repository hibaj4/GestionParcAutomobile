using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using SharedLibrary.Entities;
using SharedLibrary.Responses;


namespace ServerLibrary.Repositories.Implementations
{
   
    public class EntretienRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Entretien>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var entretien = await appDbContext.Entretiens.FindAsync(id);
            if (entretien is null) return NotFound();
            appDbContext.Entretiens.Remove(entretien);
            await Commit();
            return Success();
        }

        public async Task<List<Entretien>> GetAll() =>await appDbContext.
            Entretiens.AsNoTracking()
             .Include(v => v.Voiture)
            .Include(f => f.Fournisseur)
            .ToListAsync();
        public async Task<Entretien> GetById(int id) => await appDbContext.Entretiens.FindAsync(id);

        public async Task<GeneralResponse> Insert(Entretien item)
        {
            appDbContext.Entretiens.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Entretien item)
        {
            var entretien = await appDbContext.Entretiens.FindAsync(item.Id);
            if (entretien is null) return NotFound();

            entretien.Description = item.Description;
            entretien.Date = item.Date;
            entretien.NbrKm = item.NbrKm;
            entretien.Cout = item.Cout;
            entretien.DateProchain = item.DateProchain;
            entretien.FournisseurId = item.FournisseurId;
            entretien.VoitureId = item.VoitureId;




            await Commit();
            return Success();
        }
        private static GeneralResponse NotFound() => new(false, "Désolé, entretien non trouvé");
        private static GeneralResponse Success() => new(true, "Processus terminé");
        private async Task Commit() => await appDbContext.SaveChangesAsync();

    }
    
}



