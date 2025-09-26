using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;
using SharedLibrary.Entities;

namespace ParcAutomobile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TrajetController : GenericController<Trajet>
    {
        private readonly ITrajetRepository _trajetRepository;




        public TrajetController(ITrajetRepository trajetRepository)
            : base(trajetRepository)
        {
           _trajetRepository = trajetRepository;
        
        }

        [HttpGet("utilisateur/{utilisateurId}")]
        public async Task<IActionResult> GetByUtilisateurId(int utilisateurId)
        {
            var trajets = await _trajetRepository.GetByUtilisateurId(utilisateurId);
            return Ok(trajets);
        }



        //[HttpPost("ajouter")]
        //public async Task<IActionResult> AjouterTrajet([FromBody] Trajet trajet)
        //{
        //    var affectation = await _affectationRepository.GetAffectationActuelleByUtilisateurIdAsync(trajet.UtilisateurId);

        //    if (affectation is null || affectation.Voiture is null)
        //        return BadRequest("Affectation ou voiture introuvable pour cet utilisateur.");

        //    // Mise à jour du kilométrage
        //    if (affectation.Voiture.Kilometrage is null)
        //        affectation.Voiture.Kilometrage = 0;

        //    affectation.Voiture.Kilometrage += trajet.DistanceParcourue;

        //    // Sauvegarder le trajet
        //    await _trajetRepository.Insert(trajet);

        //    // Sauvegarder la voiture mise à jour
        //    await _voitureRepository.Update(affectation.Voiture);

        //    return Ok(new { success = true, message = "Trajet ajouté et kilométrage mis à jour." });
        //}
     

        [HttpPost("ajouter")]
        public async Task<IActionResult> AjouterTrajet([FromBody] Trajet trajet)
        {
            var response = await _trajetRepository.Insert(trajet);
            if (!response.Flag)
                return BadRequest(response.Message);

            return Ok(response);
        }



        [HttpPut("modifier")]
        public async Task<IActionResult> ModifierTrajet([FromBody] Trajet trajet)
        {
            var response = await _trajetRepository.Update(trajet);
            if (!response.Flag)
                return BadRequest(response.Message);

            return Ok(response);
        }
       
        [HttpDelete("supprimer/{id}")]

        public async Task<IActionResult> SupprimerTrajet( int id)
        {
            var response = await _trajetRepository.DeleteById(id);
            if (!response.Flag)
                return BadRequest(response.Message);

            return Ok(response);
        }






    }



}
