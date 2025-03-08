using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WPF_FE.Controls;
using WPF_FE.Models;
using static WPF_FE.Controls.DangKyMonHocWindowMVVM;

namespace WPF_FE.Services
{
    public class HocVienService
    {
        private readonly HttpClient _httpClient;

        public HocVienService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5044/api/HocVien/"); // Replace with actual API URL
        }

        // Fetch all subjects
        public async Task<List<Lylich>> GetHocViensAsync(string msmh)
        {
            return await _httpClient.GetFromJsonAsync<List<Lylich>>($"list/{msmh}");
        }

        public async Task<List<Lylich>> GetHocViensRegistedAsync(string msmh)
        {

            var response = await _httpClient.GetFromJsonAsync<List<Lylich>>($"list/registed/{msmh}");
            return response;

        }
        public async Task<bool> HuyDangKyAsync(object hv)
        {
            var param = hv as UnsubscribeParams;
            var response = await _httpClient.PostAsJsonAsync<object>($"unsubscribe/{param.Msmh}/{param.Mshv}", null);
            return response.IsSuccessStatusCode;

        }
        public async Task<bool> DangKyAsync(object hv)
        {
            var param = hv as UnsubscribeParams;
            var response = await _httpClient.PostAsJsonAsync<object>($"subscribe/{param.Msmh}/{param.Mshv}", null);
            return response.IsSuccessStatusCode;

        }
    }
}
