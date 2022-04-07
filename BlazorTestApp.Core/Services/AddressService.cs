using BlazorTestApp.Model.Address;
using System.Net.Http.Json;

namespace BlazorTestApp.Core.Services
{
    public interface IAddressService
    {
        Task<IpAddressData> FetchCurrentAddressAsync();

    }

    public class AddressService : IAddressService
    {
        private const string CurrentAddressApiUrl = "https://ipinfo.io/json"; // mock "sample-data/address.json

        private readonly HttpClient _httpClient;
        public AddressService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IpAddressData> FetchCurrentAddressAsync()
        {
            return await _httpClient.GetFromJsonAsync<IpAddressData>(CurrentAddressApiUrl);
        }
    }
}
