

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using SharedLibrary.Entities;
using SharedLibrary.Responses;


namespace ServerLibrary.Repositories.Implementations
{
    public class VoitureRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Voiture>, IVoitureRepository
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var voiture = await appDbContext.Voitures.FindAsync(id);
            if (voiture is null) return NotFound();
            appDbContext.Voitures.Remove(voiture);
            await Commit();
            return Success();
        }

        public async Task<List<Voiture>> GetAll() => await appDbContext.Voitures.ToListAsync();
        public async Task<Voiture> GetById(int id) => await appDbContext.Voitures.FindAsync(id);

        public async Task<GeneralResponse> Insert(Voiture item)
        {
            var checkIfnull = await CheckName(item.Immatriculation!);
            if (!checkIfnull)
                return new GeneralResponse(false, "Véhicule existe déjà avec cette immatriculation");
            appDbContext.Voitures.Add(item);
            await Commit();
            return Success();
        }

      
        public async Task<GeneralResponse> Update(Voiture item)
        {
            var voitureExistante = await appDbContext.Voitures.FindAsync(item.Id);
            if (voitureExistante is null)
                return NotFound();

            if (voitureExistante.Immatriculation != item.Immatriculation)
            {
                bool immatriculationDisponible = await CheckName(item.Immatriculation!);
                if (!immatriculationDisponible)
                    return new GeneralResponse(false, "Un véhicule avec cette immatriculation existe déjà");
            }

            voitureExistante.Immatriculation = item.Immatriculation;
            voitureExistante.Marque = item.Marque;
            voitureExistante.Modele = item.Modele;
            voitureExistante.Couleur = item.Couleur;
            voitureExistante.TypeVehicule = item.TypeVehicule;
            voitureExistante.AnneeMiseCirculation = item.AnneeMiseCirculation;
            voitureExistante.TypeCarburant = item.TypeCarburant;
            voitureExistante.Kilometrage = item.Kilometrage;
            voitureExistante.AvecChauffeur = item.AvecChauffeur;
            voitureExistante.Origine= item.Origine;

            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> ImporterDepuisExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return new GeneralResponse(false, "Fichier vide");

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var stream = file.OpenReadStream();
            using var package = new ExcelPackage(stream);

            var worksheet = package.Workbook.Worksheets[0];
            if (worksheet == null)
                return new GeneralResponse(false, "Aucune feuille trouvée dans le fichier Excel");

            var voitures = new List<Voiture>();
            var doublons = new List<string>();
            var immatriculationsTraitees = new HashSet<string>();

            // Récupérer toutes les immatriculations existantes en une seule requête
            var immatriculationsExistantes = await appDbContext.Voitures
                .Select(v => v.Immatriculation)
                .ToListAsync();

            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                string immatriculation = worksheet.Cells[row, 1].Text?.Trim();

                // Vérifier si l'immatriculation est vide
                if (string.IsNullOrWhiteSpace(immatriculation))
                {
                    continue;
                }

                // Vérifier les doublons dans le fichier Excel
                if (immatriculationsTraitees.Contains(immatriculation))
                {
                    doublons.Add(immatriculation);
                    continue;
                }

                // Vérifier si l'immatriculation existe déjà en base de données
                bool existeDejaEnBase = immatriculationsExistantes.Contains(immatriculation);
                if (!existeDejaEnBase)
                {
                    var voiture = new Voiture
                    {
                        Immatriculation = immatriculation,
                        Marque = worksheet.Cells[row, 2].Text,
                        Modele = worksheet.Cells[row, 3].Text,
                        Couleur = worksheet.Cells[row, 4].Text,
                        TypeVehicule = worksheet.Cells[row, 5].Text,
                        AnneeMiseCirculation = int.TryParse(worksheet.Cells[row, 6].Text, out var a) ? a : 0,
                        TypeCarburant = worksheet.Cells[row, 7].Text,
                        Kilometrage = double.TryParse(worksheet.Cells[row, 8].Text, out var k) ? k : 0,
                        AvecChauffeur = bool.TryParse(worksheet.Cells[row, 9].Text, out var c) && c,
                        Origine = worksheet.Cells[row, 10].Text
                    };
                    voitures.Add(voiture);
                    immatriculationsTraitees.Add(immatriculation);
                }
                else
                {
                    doublons.Add(immatriculation);
                }
            }

            if (voitures.Count == 0)
            {
                if (doublons.Count > 0)
                    return new GeneralResponse(false, "Aucun nouveau véhicule à importer. Toutes les immatriculations existent déjà en base.");
                else
                    return new GeneralResponse(false, "Aucun véhicule valide à importer.");
            }

            appDbContext.Voitures.AddRange(voitures);
            await appDbContext.SaveChangesAsync();

            string message = $"{voitures.Count} véhicule(s) importé(s) avec succès !";
            if (doublons.Count > 0)
            {
                message += $" {doublons.Count} immatriculation(s) ignorée(s) (doublons ou existantes).";
            }

            return new GeneralResponse(true, message);
        }



        //public async Task<GeneralResponse> ImporterDepuisExcel(IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //        return new GeneralResponse(false, "Fichier vide");

        //    // Définir le contexte de licence non-commercial
        //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        //    using var stream = file.OpenReadStream();
        //    using var package = new ExcelPackage(stream);

        //    var worksheet = package.Workbook.Worksheets[0];
        //    if (worksheet == null)
        //        return new GeneralResponse(false, "Aucune feuille trouvée dans le fichier Excel");

        //    var voitures = new List<Voiture>();

        //    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
        //    {
        //        var voiture = new Voiture
        //        {
        //            Immatriculation = worksheet.Cells[row, 1].Text,
        //            Marque = worksheet.Cells[row, 2].Text,
        //            Modele = worksheet.Cells[row, 3].Text,
        //            Couleur = worksheet.Cells[row, 4].Text,
        //            TypeVehicule = worksheet.Cells[row, 5].Text,
        //            AnneeMiseCirculation = int.TryParse(worksheet.Cells[row, 6].Text, out var a) ? a : 0,
        //            TypeCarburant = worksheet.Cells[row, 7].Text,
        //            Kilometrage = double.TryParse(worksheet.Cells[row, 8].Text, out var k) ? k : 0,
        //            AvecChauffeur = bool.TryParse(worksheet.Cells[row, 9].Text, out var c) && c,
        //            Origine = worksheet.Cells[row, 10].Text
        //        };
        //        voitures.Add(voiture);
        //    }

        //    appDbContext.Voitures.AddRange(voitures);
        //    await appDbContext.SaveChangesAsync();

        //    return new GeneralResponse(true, $"{voitures.Count} véhicules importées avec succès !");
        //}

        private static GeneralResponse NotFound() => new(false, "Désolé, véhicule non trouvé");
        private static GeneralResponse Success() => new(true, "Processus terminé");
        private async Task Commit() => await appDbContext.SaveChangesAsync();
        private async Task<bool> CheckName(string immatriculation)
        {
            var item = await appDbContext.Voitures.FirstOrDefaultAsync(x => x.Immatriculation == immatriculation);
            return item is null;

        }
    }
}