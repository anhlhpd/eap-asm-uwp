using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json.Linq;

namespace Client.Service
{
    class GlobalHandle
    {
        public static async Task<string> checkToken()
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            try
            {
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                StorageFile file = await storageFolder.GetFileAsync("token.txt");
                string token = await FileIO.ReadTextAsync(file);
                return token;
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }
    }
}
