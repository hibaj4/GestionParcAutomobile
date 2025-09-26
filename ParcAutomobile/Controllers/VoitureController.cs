using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;
using SharedLibrary.DTOs;
using SharedLibrary.Entities;

namespace ParcAutomobile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoitureController : GenericController<Voiture>
    {
        private readonly IVoitureRepository _voitureRepository;

        public VoitureController(IVoitureRepository voitureRepository)
            : base(voitureRepository)
        {
            _voitureRepository = voitureRepository;
        }

        [HttpPost("importer")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ImporterVoitures([FromForm] ImporterVoitureRequest request)
        {
            if (request.File == null || request.File.Length == 0)
                return BadRequest("Fichier Excel non fourni.");

            var response = await _voitureRepository.ImporterDepuisExcel(request.File);
            if (!response.Flag)
                return BadRequest(response.Message);

            return Ok(response);
        }


    }
}





