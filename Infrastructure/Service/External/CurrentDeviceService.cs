using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Interfaces.External;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Service.External
{
    public class CurrentDeviceService : ICurrentDeviceService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient _httpClient;
        public CurrentDeviceService(IHttpContextAccessor contextAccessor, HttpClient httpClient)
        {
            _contextAccessor = contextAccessor;
            _httpClient = httpClient;
        }

        public string GetClientIp()
        {
            var ipAddress = _contextAccessor.HttpContext.Connection.RemoteIpAddress;
            if (ipAddress == null) return string.Empty;

            return ipAddress.ToString();
        }

        public async Task<IpLocationResponse?> GetLocationAsync(string ip)
        {
            try
            {
                string url = @$"https://ipinfo.io/{ip}/json";
                var response = await _httpClient.GetAsync(url);
                if(!response.IsSuccessStatusCode) return null;

                var content = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(content);

                var root = doc.RootElement;
                if(root.GetPropertyCount() == 0) return null;
                return new IpLocationResponse
                {
                    City = root.GetProperty("city").GetString()!,
                    Country = root.GetProperty("country").GetString()!,
                    Region = root.GetProperty("region").GetString()!
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
