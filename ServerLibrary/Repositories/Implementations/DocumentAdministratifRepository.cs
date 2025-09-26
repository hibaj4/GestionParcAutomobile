using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using SharedLibrary.Entities;
using SharedLibrary.Responses;
using System.Reflection.Metadata;

namespace ServerLibrary.Repositories.Implementations
{
    

    public class DocumentAdministratifRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<DocumentAdministratif>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var documentAdministratif = await appDbContext.DocumentAdministratifs.FindAsync(id);
            if (documentAdministratif is null) return NotFound();
            appDbContext.DocumentAdministratifs.Remove(documentAdministratif);
            await Commit();
            return Success();
        }

        public async Task<List<DocumentAdministratif>> GetAll() => await appDbContext.

             DocumentAdministratifs.AsNoTracking()
             .Include(v => v.Voiture)
            .Include(f => f.Fournisseur)
            .ToListAsync();
        public async Task<DocumentAdministratif> GetById(int id) => await appDbContext.DocumentAdministratifs.FindAsync(id);

        public async Task<GeneralResponse> Insert(DocumentAdministratif item)
        {
            appDbContext.DocumentAdministratifs.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(DocumentAdministratif item)
        {
            var documentAdministratif = await appDbContext.DocumentAdministratifs.FindAsync(item.Id);
            if (documentAdministratif is null) return NotFound();
            documentAdministratif.DateExpirationAssurance = item.DateExpirationAssurance;
            documentAdministratif.DateAssurance = item.DateAssurance;
            documentAdministratif.DateExpirationTaxe = item.DateExpirationTaxe;
            documentAdministratif.DateTaxe = item.DateTaxe;
            documentAdministratif.DateProchaineVisite = item.DateProchaineVisite;
            documentAdministratif.DateDerniereVisite = item.DateDerniereVisite;
            documentAdministratif.MontantAssurance = item.MontantAssurance;
            documentAdministratif.MontantTaxe = item.MontantTaxe;
            documentAdministratif.MontantVisiteTechnique = item.MontantVisiteTechnique;
            documentAdministratif.FournisseurId = item.FournisseurId;
            documentAdministratif.VoitureId = item.VoitureId;



            await Commit();
            return Success();
        }
        private static GeneralResponse NotFound() => new(false, "Désolé, documentAdministratif non trouvé");
        private static GeneralResponse Success() => new(true, "Processus terminé");
        private async Task Commit() => await appDbContext.SaveChangesAsync();

    }
}
