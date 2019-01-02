using Client.Service;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Client.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GeneralInformation : Page
    {

        public GeneralInformation()
        {
            this.InitializeComponent();
            Get_General_Infor();

        }
        public async void Get_General_Infor()
        {
            var file = await ApplicationData.Current.LocalFolder.TryGetItemAsync("token.txt");
            if (file != null)
            {
                var response = await APIHandle.Get_Member_Infor();
                var responseContent = await response.Content.ReadAsStringAsync();
                GeneralInformation genInfo = JsonConvert.DeserializeObject<GeneralInformation>(responseContent.ToString());
                this.Email.Text = genInfo.Email.ToString();
                this.FirstName.Text = genInfo.FirstName.ToString();
                this.LastName.Text = genInfo.LastName.ToString();
                this.Phone.Text = genInfo.Phone.ToString();
                this.Birthday.Text = genInfo.Birthday.ToString();
                this.Gender.SelectedValue = genInfo.Gender.ToString();
                Debug.WriteLine(genInfo.Gender);
            }
        }
    }
}
