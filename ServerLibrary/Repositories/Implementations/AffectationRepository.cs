using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using SharedLibrary.DTOs;
using SharedLibrary.Entities;
using SharedLibrary.Responses;
using System;
using System.Diagnostics;


namespace ServerLibrary.Repositories.Implementations
{
    public class AffectationRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Affectation>, IAffectationRepository
    {
    
      
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var affectation = await appDbContext.Affectations.FindAsync(id);
            if (affectation is null) return NotFound();
            appDbContext.Affectations.Remove(affectation);
            await Commit();
            return Success();
        }

        public async Task<List<Affectation>> GetAll() => await appDbContext.
            Affectations
             .Include(v => v.Voiture)
            .Include(u => u.Utilisateur)
            .ToListAsync();
        public async Task<Affectation> GetById(int id) => await appDbContext.Affectations.FindAsync(id);

        public async Task<GeneralResponse> Insert(Affectation item)
        {
            appDbContext.Affectations.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Affectation item)
        {
            var affectation = await appDbContext.Affectations.FindAsync(item.Id);
            if (affectation is null) return NotFound();

            affectation.DateDebut = item.DateDebut;
            affectation.DateFin = item.DateFin;
            affectation.UtilisateurId = item.UtilisateurId;
            affectation.VoitureId = item.VoitureId;




            await Commit();
            return Success();
        }

        public async Task<Affectation?> GetAffectationActuelleByUtilisateurIdAsync(int utilisateurId)
        {
            var today = DateTime.Today;

            return await appDbContext.Affectations
                .Include(a => a.Voiture)
                .Where(a => a.UtilisateurId == utilisateurId &&
                            a.DateDebut <= today &&
                            a.DateFin >= today)
                .OrderByDescending(a => a.DateDebut)
                .FirstOrDefaultAsync();
        }



        private static GeneralResponse NotFound() => new(false, "Désolé,  affectation non trouvée");
        private static GeneralResponse Success() => new(true, "Processus terminé");
        private async Task Commit() => await appDbContext.SaveChangesAsync();

       


    }

}



