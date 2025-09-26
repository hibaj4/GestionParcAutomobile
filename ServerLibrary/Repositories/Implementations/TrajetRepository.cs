using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using SharedLibrary.Entities;
using SharedLibrary.Responses;
using Microsoft.EntityFrameworkCore;



namespace ServerLibrary.Repositories.Implementations
{
    
    public class TrajetRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Trajet>, ITrajetRepository
    {
        

       
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var trajet = await appDbContext.Trajets.FindAsync(id);
            if (trajet is null)
                return NotFound();

            var affectation = await appDbContext.Affectations
                .Include(a => a.Voiture)
                .FirstOrDefaultAsync(a =>
                    a.UtilisateurId == trajet.UtilisateurId &&
                    a.DateDebut <= trajet.Date &&
                    a.DateFin >= trajet.Date);

            if (affectation?.Voiture != null)
            {
                affectation.Voiture.Kilometrage -= trajet.DistanceParcourue;
                if (affectation.Voiture.Kilometrage < 0)
                    affectation.Voiture.Kilometrage = 0; 
            }

            var utilisateur = await appDbContext.Utilisateurs
       .FirstOrDefaultAsync(u => u.Id == trajet.UtilisateurId);

            if (utilisateur != null)
            {
                utilisateur.SoldeCarburant += trajet.DistanceParcourue;
            }

            appDbContext.Trajets.Remove(trajet);
            await Commit();

            return Success();
        }

        public async Task<List<Trajet>> GetAll()
        {
            return await appDbContext.Trajets
                .Include(t => t.Utilisateur)  
                .ToListAsync();
        }

        public async Task<Trajet> GetById(int id) => await appDbContext.Trajets.FindAsync(id);

       
       

       






        public async Task<GeneralResponse> Insert(Trajet item)
        {
            // Vérifier si l'utilisateur existe
            var utilisateur = await appDbContext.Utilisateurs
                .Include(u => u.Trajets)
                .FirstOrDefaultAsync(u => u.Id == item.UtilisateurId);

            if (utilisateur == null)
                return new GeneralResponse(false, "Utilisateur introuvable.");

            // Vérifier si l'utilisateur a assez de carburant
            if (utilisateur.SoldeCarburant < item.DistanceParcourue)
                return new GeneralResponse(false, "Solde carburant insuffisant pour ce trajet.");

            // Récupérer l'affectation active à la date du trajet
            var affectation = await appDbContext.Affectations
                .Include(a => a.Voiture)
                .FirstOrDefaultAsync(a =>
                    a.UtilisateurId == item.UtilisateurId &&
                    a.DateDebut <= item.Date &&
                    a.DateFin >= item.Date);

            // Vérifier si l'affectation existe
            if (affectation == null|| affectation.Voiture == null)
                return new GeneralResponse(false, "Affectation de véhicule introuvable.");

 

            // Ajouter le trajet et mettre à jour les données
            appDbContext.Trajets.Add(item);
            utilisateur.Trajets?.Add(item);
            utilisateur.SoldeCarburant -= item.DistanceParcourue;
            affectation.Voiture.Kilometrage += item.DistanceParcourue;

            await Commit();
            return Success();
        }






        public async Task<GeneralResponse> Update(Trajet item)
        {
            var trajet = await appDbContext.Trajets.FindAsync(item.Id);
            if (trajet == null)
                return new GeneralResponse(false, "Trajet non trouvé.");

            // Garder l'ancienne distance et date pour ajuster le kilométrage
            double ancienneDistance = trajet.DistanceParcourue;
            DateTime ancienneDate = trajet.Date;
            int ancienneUtilisateurId = trajet.UtilisateurId;


            var ancienUtilisateur = await appDbContext.Utilisateurs
       .FirstOrDefaultAsync(u => u.Id == ancienneUtilisateurId);

            // Rendre l'ancienne distance au solde carburant
            if (ancienUtilisateur != null)
                ancienUtilisateur.SoldeCarburant += ancienneDistance;

            // Récupérer nouvel utilisateur
            var nouvelUtilisateur = await appDbContext.Utilisateurs
                .FirstOrDefaultAsync(u => u.Id == item.UtilisateurId);
            if (nouvelUtilisateur == null)
                return new GeneralResponse(false, "Nouvel utilisateur introuvable.");

            // Vérifier si le nouvel utilisateur a assez de carburant
           
                if (nouvelUtilisateur.SoldeCarburant < item.DistanceParcourue)
                {
                    // Annuler et renvoyer message
                    return new GeneralResponse(false, "Solde carburant insuffisant pour ce trajet.");
                }

                // Soustraire la nouvelle distance
           

            var nouvelleAffectation = await appDbContext.Affectations
                .Include(a => a.Voiture)
                .FirstOrDefaultAsync(a =>
                    a.UtilisateurId == item.UtilisateurId &&
                    a.DateDebut <= item.Date &&
                    a.DateFin >= item.Date);


            if (nouvelleAffectation == null)
                return new GeneralResponse(false, "La date du trajet ne correspond à aucune affectation de véhicule.");

            if (nouvelleAffectation.Voiture == null)
                return new GeneralResponse(false, "Aucune véhicule n'est affectée à cet utilisateur pour cette période.");

            // Mettre à jour les propriétés du trajet
            trajet.Date = item.Date;
            trajet.LieuDepart = item.LieuDepart;
            trajet.Destination = item.Destination;
            trajet.IsMission = item.IsMission;
            trajet.DistanceParcourue = item.DistanceParcourue;
            trajet.UtilisateurId = item.UtilisateurId;

            // Récupérer l'affectation (avec voiture) liée à l'ancienne date
            var ancienneAffectation = await appDbContext.Affectations
                .Include(a => a.Voiture)
                .FirstOrDefaultAsync(a =>
                    a.UtilisateurId == ancienneUtilisateurId &&
                    a.DateDebut <= ancienneDate &&
                    a.DateFin >= ancienneDate);

            // Récupérer l'affectation (avec voiture) liée à la nouvelle date
            

            // Ajuster le kilométrage si l'ancienne affectation a une voiture
            if (ancienneAffectation?.Voiture != null)
            {
                ancienneAffectation.Voiture.Kilometrage -= ancienneDistance;
                if (ancienneAffectation.Voiture.Kilometrage < 0)
                    ancienneAffectation.Voiture.Kilometrage = 0; // éviter les valeurs négatives
            }

            // Ajuster le kilométrage si la nouvelle affectation a une voiture
            if (nouvelleAffectation?.Voiture != null)
            {
                if (nouvelleAffectation.Voiture.Kilometrage == null)
                    nouvelleAffectation.Voiture.Kilometrage = 0;

            }
            nouvelleAffectation.Voiture.Kilometrage += item.DistanceParcourue;
            nouvelUtilisateur.SoldeCarburant -= item.DistanceParcourue;


            await appDbContext.SaveChangesAsync();

            return new GeneralResponse(true, "Trajet mis à jour avec succès.");
        }



        public async Task<List<Trajet>> GetByUtilisateurId(int utilisateurId)
        {
            return await appDbContext.Trajets
                .Where(t => t.UtilisateurId == utilisateurId)
                .AsNoTracking() 
                .Include(t => t.Utilisateur) 
                .ToListAsync();
        }




        private static GeneralResponse NotFound() => new(false, "Désolé, trajet non trouvé");
        private static GeneralResponse Success() => new(true, "Processus terminé");
        private async Task Commit() => await appDbContext.SaveChangesAsync();

    }
    
}
