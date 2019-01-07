using Client.Entities;
using Client.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Client.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Clazz : Page
    {
        private ObservableCollection<Account> listStudents;
        internal ObservableCollection<Account> ListStudents { get => listStudents; set => listStudents = value; }
        public Clazz()
        {
            this.InitializeComponent();
        }
        private async void Get_List_Students()
        {
            this.listStudents = new ObservableCollection<Entities.Account>();
            var response = await APIHandle.Get_Clazz_Infor();
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var array = JArray.Parse(responseContent);
                foreach (var obj in array)
                {
                    Entities.Account student = obj.ToObject<Entities.Account>();
                    this.listStudents.Add(student);
                }
            }
            else
            {
                var dialog = new ContentDialog()
                {
                    Title = "Error!",
                    MaxWidth = this.ActualWidth,
                    Content = "There's an error! Please try later!",
                    CloseButtonText = "OK!"
                };
                ErrorResponse errorObject = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);

                if (errorObject != null)
                {
                    Debug.WriteLine(errorObject.message);
                }
            }
        }
    }
}
