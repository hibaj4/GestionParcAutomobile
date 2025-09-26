using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using SharedLibrary.Entities;
using SharedLibrary.Responses;
using System.Net.Http.Json;


namespace ClientLibrary.Services.Implementations
{
    public class TrajetService(GetHttpClient getHttpClient) : GenericServiceImplementation<Trajet>(getHttpClient), ITrajetService
    {
        public async Task<List<Trajet>> GetByUtilisateurId(int utilisateurId)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var trajets = await httpClient.GetFromJsonAsync<List<Trajet>>($"{MyConstants.TrajetBaseUrl}/utilisateur/{utilisateurId}");
            return trajets!;
        }
        public async Task<GeneralResponse> AjouterTrajetAsync(Trajet trajet, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var response = await httpClient.PostAsJsonAsync($"{baseUrl}/ajouter", trajet);

            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                return new GeneralResponse(false, message);
            }

            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return result ?? new GeneralResponse(false, "Erreur inconnue lors de l'ajout du trajet");
        }


        public async Task<GeneralResponse> ModifierTrajetAsync(Trajet trajet, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var response = await httpClient.PutAsJsonAsync($"{baseUrl}/modifier", trajet);

            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                return new GeneralResponse(false, message);
            }

            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return result ?? new GeneralResponse(false, "Erreur inconnue lors de la modification du trajet");
        }
        public async Task<GeneralResponse> SupprimerTrajetAsync(int id, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var response = await httpClient.DeleteAsync($"{baseUrl}/supprimer/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                return new GeneralResponse(false, message);
            }

            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return result ?? new GeneralResponse(false, "Erreur inconnue lors de la suppression du trajet");
        }

    }
}
