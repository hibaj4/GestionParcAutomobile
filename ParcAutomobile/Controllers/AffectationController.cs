using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;
using SharedLibrary.DTOs;
using SharedLibrary.Entities;

namespace ParcAutomobile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AffectationController(IGenericRepositoryInterface<Affectation> genericRepositoryInterface) : GenericController<Affectation>(genericRepositoryInterface)
    {
    }
}




