using SharedLibrary.Entities;
using SharedLibrary.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Services.Contracts
{
    public interface IVoitureService : IGenericServiceInterface<Voiture>
    {
        Task<GeneralResponse> ImporterDepuisExcelAsync(MultipartFormDataContent fileContent, string baseUrl);
    }

}
