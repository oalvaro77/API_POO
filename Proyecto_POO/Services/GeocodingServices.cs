using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Proyecto_POO.Services
{
    public class GeocodingServices : IGeocodingService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "AIzaSyCq3AcDOFGE2ZtYX4pV_evmr-_5AFdHjr8";

        public GeocodingServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(double latitude, double longitude)> ObtenerCoordenadas(string direccion)
        {
            var url = $"https://maps.googleapis.com/maps/api/geocode/json?address=1600+Amphitheatre+Parkway,+Mountain+View,+CA&key={_apiKey}";
            var response = await _httpClient.GetStringAsync(url);

            var json = JObject.Parse(response);
            var result = json["result"];
            if (result == null)
            {
                throw new Exception("No se encontraron coordenadas para la direccion");
            }

            var location = result[0]["geometry"]["location"];
            double lat = (double)location["lat"];
            double lng = (double)location["lng"];
            return (lat, lng);
        }
    }
}
