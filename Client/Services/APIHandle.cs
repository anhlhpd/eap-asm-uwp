using Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;

namespace Client.Services
{
    class APIHandle
    {
        private static string API_STUDENT_INFOR = "https://localhost:44314/api/PersonalInformations/";

        public async static Task<HttpResponseMessage> Get_Personal_Infor(int id)
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
            var response = httpClient.PostAsync(API_STUDENT_INFOR + id, content);
            return response.Result;
        }
    }
}
