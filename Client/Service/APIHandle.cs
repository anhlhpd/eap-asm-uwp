using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;

namespace Client.Service
{
    class APIHandle
    {
        private static string API_GENERAL_INFOR = "https://eap-asm.azurewebsites.net/api/authentication/tokenLogin?accessToken=";
        private static string API_SUBJECTS = "https://localhost:44134/api/AccountSubject/";
        private static string API_CLAZZ = "https://localhost:44134/api/AccountClazz/";
        private static string API_LOGIN = "https://eap-asm.azurewebsites.net/api/Authentication/StudentLogin";
        public async static Task<HttpResponseMessage> Get_Member_Infor()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await storageFolder.GetFileAsync("token.txt");
            string json = await FileIO.ReadTextAsync(file);
            //JsonValue jsonValue = JsonValue.Parse(json);
            //string token = jsonValue.GetObject().GetNamedString("token");
            //Debug.WriteLine(token);

            HttpClient httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            var content = new StringContent("");
            var response = httpClient.PostAsync(API_GENERAL_INFOR + json, content);
            //Debug.WriteLine(response.Result);
            return response.Result;
        }

        public async static Task<HttpResponseMessage> Get_Subjects()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await storageFolder.GetFileAsync("token.txt");
            string json = await FileIO.ReadTextAsync(file);
            JsonValue jsonValue = JsonValue.Parse(json);
            string token = jsonValue.GetObject().GetNamedString("token");
            Debug.WriteLine(token);

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            var content = new StringContent("");
            var response = httpClient.PostAsync(API_SUBJECTS, content);
            return response.Result;
        }
        public async static Task<HttpResponseMessage> Get_Clazzes()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await storageFolder.GetFileAsync("token.txt");
            string json = await FileIO.ReadTextAsync(file);
            JsonValue jsonValue = JsonValue.Parse(json);
            string token = jsonValue.GetObject().GetNamedString("token");
            Debug.WriteLine(token);

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            var content = new StringContent("");
            var response = httpClient.PostAsync(API_CLAZZ, content);
            return response.Result;
        }
        public async static Task<HttpResponseMessage> Sign_In(string username, string password)
        {
            Dictionary<String, String> memberLogin = new Dictionary<string, string>();
            memberLogin.Add("username", username);
            memberLogin.Add("password", password);
            memberLogin.Add("clientId", "STU");
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(memberLogin), Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(API_LOGIN, content);
            return response.Result;
        }
    }
}
