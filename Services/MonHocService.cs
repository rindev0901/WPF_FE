using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WPF_FE.DTOs;
using WPF_FE.Models;

namespace WPF_FE.Services
{
    public class MonHocService
    {
        private readonly HttpClient _httpClient;

        public MonHocService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5044/api/MonHoc/"); // Replace with actual API URL
        }

        // Fetch all subjects
        public async Task<List<Monhoc>> GetMonHocsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Monhoc>>("list");
        }

        // Update subject
        public async Task<bool> UpdateMonHocAsync(string id, Monhoc monHoc)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{id}/update", monHoc);
                if (!response.IsSuccessStatusCode)
                {
                    // Read the response content if available
                    string errorMessage = await response.Content.ReadAsStringAsync();

                    var error = JsonSerializer.Deserialize<ErrorResponse>(errorMessage);

                    Console.WriteLine($"API Error: {errorMessage}");

                    throw new HttpRequestException(error.message, null, (HttpStatusCode)error.statusCode);
                }

                return true; // Success
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                throw; // Rethrow so ViewModel can catch it
            }
        }
        public async Task<bool> DeleteMonHocByIdAsync(string id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{id}/delete");

                if (!response.IsSuccessStatusCode)
                {
                    // Read the response content if available
                    string errorMessage = await response.Content.ReadAsStringAsync();

                    var error = JsonSerializer.Deserialize<ErrorResponse>(errorMessage);

                    Console.WriteLine($"API Error: {errorMessage}");

                    throw new HttpRequestException(error.message, null, (HttpStatusCode)error.statusCode);
                }

                return true; // Success
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                throw; // Rethrow so ViewModel can catch it
            }
        }
        public async Task<bool> AddMonHocAsync(Monhoc monHoc)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("create", monHoc);

                if (!response.IsSuccessStatusCode)
                {
                    // Read the response content if available
                    string errorMessage = await response.Content.ReadAsStringAsync();

                    var error = JsonSerializer.Deserialize<ErrorResponse>(errorMessage);

                    Console.WriteLine($"API Error: {errorMessage}");

                    throw new HttpRequestException(error.message, null, (HttpStatusCode)error.statusCode);
                }

                return true; // Success
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                throw; // Rethrow so ViewModel can catch it
            }
        }

    }
}
