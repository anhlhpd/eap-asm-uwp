using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;

namespace Client.Service
{
    class APIHandle
    {
        private static string API_LOGIN = "https://localhost:44314/api/authentication/StudentLogin";
        private static string API_GENERAL_INFOR = "https://localhost:44134/api/GeneralInformations/";

        public async static Task<HttpResponseMessage> Get_Member_Infor()
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
            var response = httpClient.PostAsync(API_GENERAL_INFOR, content);
            return response.Result;
        }

        public async static Task<HttpResponseMessage> Login(string username, string password)
        {
            Dictionary<String, String> memberLogin = new Dictionary<string, string>();
            memberLogin.Add("username", username);
            memberLogin.Add("password", password);
            memberLogin.Add("clientId", "STU");
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(memberLogin), Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(API_LOGIN, content);
            Debug.WriteLine(response.Result);
            return response.Result;
        }
    }
}
