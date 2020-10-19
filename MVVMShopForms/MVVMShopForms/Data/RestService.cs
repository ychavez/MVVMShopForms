using MVVMShopForms.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using ModernHttpClient;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Net.Http.Headers;

namespace MVVMShopForms.Data
{
    public class RestService
    {
        HttpClient _client;
        Uri _UrlBase;


        public RestService(string urlbase)
        {
            _UrlBase = new Uri(urlbase);
            _client = new HttpClient(new NativeMessageHandler());
            _client.BaseAddress = _UrlBase;

        }

        public async Task<List<T>> GetDataAsync<T>(string uri)
        {
            List<T> TData = null;
            try
            {
                var response = _client.GetAsync(uri).ConfigureAwait(false).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    TData = JsonConvert.DeserializeObject<List<T>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }

            return TData;
        }
        public async Task<string> PostDataAsync<T>(T Data, string uri)
        {
            var json = JsonConvert.SerializeObject(Data);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            _client.BaseAddress = _UrlBase;
            var response = await _client.PostAsync(uri, data).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return content;
            }
            return "";
        }

        public async Task PutDataAsync<T>(T Data, string uri, int id)
        {
            var json = JsonConvert.SerializeObject(Data);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            _client.BaseAddress = _UrlBase;
            var response = await _client.PutAsync(String.Concat(uri, "/", id.ToString()), data);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
            }
        }

        public async Task DeleteDataAsync(string uri, int id)
        {
            _client.BaseAddress = _UrlBase;
            var response = await _client.DeleteAsync(String.Concat(uri, "/", id.ToString()));
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
            }
        }

        public bool CheckToken(string token)
        {
            _client.DefaultRequestHeaders.Authorization =
                      new AuthenticationHeaderValue("Bearer", token);
            _client.BaseAddress = _UrlBase;
            var response = _client.GetAsync("Account/Check").ConfigureAwait(false).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }
    }
}
