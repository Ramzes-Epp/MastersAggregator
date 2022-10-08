using MasterAggregator.Desktop.Models;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MasterAggregator.Desktop.Repositories.WebApiJson;

internal class MasterRepositoryDesktopWebApi : IMasterRepository
{
    static ObservableCollection<Master>? Masters { get; set; }

    /// <summary>
    /// урл сервера к которому обращаемся
    /// </summary>
    static readonly string BaseUri = "https://localhost:7004";

    static HttpClient NewMyHttpClient(string AuthorizationPassword)
    {
        HttpClient client = new HttpClient();

        client.BaseAddress = new Uri(System.Uri.UnescapeDataString(BaseUri));// базовый адрес / доменное имя
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));// Установлеваем формат передачи в json 
        client.DefaultRequestHeaders.TryAddWithoutValidation("ApiKey", AuthorizationPassword);// Установлеваем apikey 

        return client;
    }
     

    public async Task<ObservableCollection<Master>?> DeleteAsync(Master master, string AuthorizationPassword)
    {
        using HttpClient client = NewMyHttpClient(AuthorizationPassword);
        try
        {
            HttpResponseMessage response = await client.DeleteAsync(@"/api/Master/DeleteMaster/" + master.Id);
            //если ответ 200 
            if (response.IsSuccessStatusCode)
            {
                Masters = await GetAllAsync(AuthorizationPassword);
                return Masters;
            }
            else
                return null;
        }
        catch (Exception)
        {
            return null;
        }
    }
     

    public async Task<ObservableCollection<Master>?> EditAsync(Master master, string AuthorizationPassword)
    {
        using HttpClient client = NewMyHttpClient(AuthorizationPassword);
        try
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(BaseUri + @"/api/Master/UpdateMaster", master);
            //если ответ 200 
            if (response.IsSuccessStatusCode)
            {
                Masters = await GetAllAsync(AuthorizationPassword);
                return Masters;
            }
            else
                return null;
        }
        catch (Exception)
        {
            return null;
        }
    }
     

    public async Task<ObservableCollection<Master>?> GetAllAsync(string AuthorizationPassword)
    {
        using HttpClient client = NewMyHttpClient(AuthorizationPassword);
        try
        {
            HttpResponseMessage response = await client.GetAsync(@"/api/Master/GetAll");
            //если ответ 200 
            if (response.IsSuccessStatusCode)
            {
                Masters = await response.Content.ReadFromJsonAsync<ObservableCollection<Master>>();
                return Masters;
            }
            else
                return null;
        }
        catch (Exception)
        {
            return null;
        }
    }


    public async Task<ObservableCollection<Master>?> SaveAsync(Master master, string AuthorizationPassword)
    {
        using HttpClient client = NewMyHttpClient(AuthorizationPassword);
        try
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(BaseUri + @"/api/Master/CreateMaster", master);
            //если ответ 200 
            if (response.IsSuccessStatusCode)
            {
                Masters = await GetAllAsync(AuthorizationPassword);
                return Masters;
            }
            else
                return null;
        }
        catch (Exception)
        {
            return null;
        }
    }
}

