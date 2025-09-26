using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using SharedLibrary.Entities;
using SharedLibrary.Responses;
using System.Net.Http.Json;


namespace ClientLibrary.Services.Implementations
{
    public class VoitureService(GetHttpClient getHttpClient)
      : GenericServiceImplementation<Voiture>(getHttpClient), IVoitureService
    {
        public async Task<GeneralResponse> ImporterDepuisExcelAsync(MultipartFormDataContent fileContent, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var response = await httpClient.PostAsync($"{baseUrl}/importer", fileContent);

            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                return new GeneralResponse(false, message);
            }

            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return result ?? new GeneralResponse(false, "Erreur inconnue lors de l'import Excel");
        }
    }
}
