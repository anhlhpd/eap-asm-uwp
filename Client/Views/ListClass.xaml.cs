using Client.Entities;
using Client.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Client.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListClass : Page
    {
        private ObservableCollection<Entities.Clazz> listAllClazzes;
        internal ObservableCollection<Entities.Clazz> ListAllClazzes { get => listAllClazzes; set => listAllClazzes = value; }
        public ListClass()
        {
            this.InitializeComponent();
            Get_List_Clazz();

        }
        private async void Get_List_Clazz()
        {
            this.listAllClazzes = new ObservableCollection<Entities.Clazz>();
            var response = await APIHandle.Get_Clazzes();
            var responseContent = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(response);
            Debug.WriteLine("1");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var array = JArray.Parse(responseContent);
                foreach (var obj in array)
                {
                    Entities.Clazz clazz = obj.ToObject<Entities.Clazz>();
                    this.listAllClazzes.Add(clazz);
                    Debug.WriteLine(clazz.id);
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

        private void StackPanel_Tap(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            StackPanel sp = sender as StackPanel;
            Entities.Clazz clazz = sp.Tag as Entities.Clazz;
            GlobalVariable.CurrentClazzId = clazz.id;
            Debug.WriteLine(GlobalVariable.CurrentClazzId);
            this.Frame.Navigate(typeof(Clazz));
        }
    }
}