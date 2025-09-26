using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;
using SharedLibrary.Entities;

namespace ParcAutomobile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class FournisseurController(IGenericRepositoryInterface<Fournisseur> genericRepositoryInterface) : GenericController<Fournisseur>(genericRepositoryInterface)
    {
    }
}
