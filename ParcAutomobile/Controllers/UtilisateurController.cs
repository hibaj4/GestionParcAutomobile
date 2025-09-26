using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;
using SharedLibrary.Entities;

namespace ParcAutomobile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateurController(IGenericRepositoryInterface<Utilisateur> genericRepositoryInterface) : GenericController<Utilisateur>(genericRepositoryInterface)    {
    }
}
