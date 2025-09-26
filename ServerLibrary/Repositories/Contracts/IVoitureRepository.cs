using Microsoft.AspNetCore.Http;
using ServerLibrary.Repositories.Contracts;
using SharedLibrary.Entities;
using SharedLibrary.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Contracts
{
    public interface IVoitureRepository : IGenericRepositoryInterface<Voiture>
    {
        Task<GeneralResponse> ImporterDepuisExcel(IFormFile file);
    }

}

