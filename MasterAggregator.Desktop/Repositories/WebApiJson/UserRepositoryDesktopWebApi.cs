using MasterAggregator.Desktop.Models;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MasterAggregator.Desktop.Repositories.WebApiJson
{
    internal class UserRepositoryDesktopWebApi : IUserRepository
    { 
        static ObservableCollection<User>? Users { get; set; }

        /// <summary>
        /// урл сервера к которому обращаемс¤
        /// </summary>
        public static readonly string BaseUri = "https://localhost:7004";
         
        static HttpClient NewMyHttpClient(string Authorization)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(System.Uri.UnescapeDataString(BaseUri));// базовый адрес / доменное им¤
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));// ”становлеваем формат передачи в json 
            client.DefaultRequestHeaders.TryAddWithoutValidation("ApiKey", Authorization);// ”становлеваем apikey 

            return client;
        }
         

        public async Task<ObservableCollection<User>?> DeleteAsync(User user, string AuthorizationPassword)
        {
            using HttpClient client = NewMyHttpClient(AuthorizationPassword);
             
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(@"/api/User/DeleteUser?id=" + user.Id);
                //если ответ 200 
                if (response.IsSuccessStatusCode)
                {
                    Users = await GetAllAsync(AuthorizationPassword);
                    return Users;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public async Task<ObservableCollection<User>?> EditAsync(User user, string AuthorizationPassword)
        {
            using HttpClient client = NewMyHttpClient(AuthorizationPassword);
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(@"/api/User/UpdateUser", user);
                //если ответ 200 
                if (response.IsSuccessStatusCode)
                {
                    Users = await GetAllAsync(AuthorizationPassword);
                    return Users;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public async Task<ObservableCollection<User>?> GetAllAsync(string AuthorizationPassword)
        {
            using HttpClient client = NewMyHttpClient(AuthorizationPassword);
            try
            {
                HttpResponseMessage response = await client.GetAsync(@"/api/User/GetAllUsers");
                //если ответ 200 
                if (response.IsSuccessStatusCode)
                {
                    Users = await response.Content.ReadFromJsonAsync<ObservableCollection<User>>();
                    return Users;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public async Task<ObservableCollection<User>?> SaveAsync(User user, string AuthorizationPassword)
        {
            using HttpClient client = NewMyHttpClient(AuthorizationPassword);
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(@"/api/User/CreateUser", user);
                //если ответ 200 
                if (response.IsSuccessStatusCode)
                {
                    Users = await GetAllAsync(AuthorizationPassword);
                    return Users;
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
}
