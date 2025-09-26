using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;
using SharedLibrary.Entities;

namespace ParcAutomobile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class EntretienController(IGenericRepositoryInterface<Entretien> genericRepositoryInterface) : GenericController<Entretien>(genericRepositoryInterface)
    {
    }
}
