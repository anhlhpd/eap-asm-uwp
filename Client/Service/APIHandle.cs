using Client.Entities;
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
        private static string API_SUBJECTS = "https://eap-asm.azurewebsites.net/api/Subjects/Student";
        private static string API_CLAZZ = "https://eap-asm.azurewebsites.net/api/AccountClazz/";
        private static string API_LOGIN = "https://eap-asm.azurewebsites.net/api/Authentication/StudentLogin";
        private static string API_MARK = "https://eap-asm.azurewebsites.net/api/marks/student";
        private static string API_EDIT_GENERAL_INFOR = "https://eap-asm.azurewebsites.net/api/Accounts/";
        public async static Task<HttpResponseMessage> Get_Member_Infor()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await storageFolder.GetFileAsync("token.txt");
            string token = await FileIO.ReadTextAsync(file);

            HttpClient httpClient = new HttpClient();
            var content = new StringContent("");
            var response = httpClient.PostAsync(API_GENERAL_INFOR + token, content);
            return response.Result;
        }
        public async static Task<HttpResponseMessage> Get_Member_Mark()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await storageFolder.GetFileAsync("token.txt");
            string token = await FileIO.ReadTextAsync(file);

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            var response = await httpClient.GetAsync(API_MARK);
            return response;
        }

        public async static Task<HttpResponseMessage> Get_Subjects()
        {
            //StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            //StorageFile file = await storageFolder.GetFileAsync("token.txt");
            //string token = await FileIO.ReadTextAsync(file);
            string token = await GlobalHandle.checkToken();

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            var response = httpClient.GetAsync(API_SUBJECTS);
            return response.Result;
        }
        public async static Task<HttpResponseMessage> Get_Clazzes()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await storageFolder.GetFileAsync("token.txt");
            string token = await FileIO.ReadTextAsync(file);

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            var response = httpClient.GetAsync(API_CLAZZ);
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
        public async static Task<HttpResponseMessage> Edit_General_Infor(Account account)
        {
            string token = await GlobalHandle.checkToken();

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            var content = new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8, "application/json");
            var response = httpClient.PutAsync(API_EDIT_GENERAL_INFOR + account.id, content);
            var con = await response.Result.Content.ReadAsStringAsync();
            Debug.WriteLine(con);
            return response.Result;
        }
    }
}
