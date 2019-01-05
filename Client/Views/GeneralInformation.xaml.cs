using Client.Service;
using Client.Entities;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.ViewManagement;
using Windows.Foundation;

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
            ApplicationView.PreferredLaunchViewSize = new Size(3000, 2000);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            Get_General_Infor();
            

        }
        public async void Get_General_Infor()
        {
            var file = await ApplicationData.Current.LocalFolder.TryGetItemAsync("token.txt");
            if (file != null)
            {
                var response = await APIHandle.Get_Member_Infor();
                var responseContent = await response.Content.ReadAsStringAsync();
                
                Entities.GeneralInformation genInfo = JsonConvert.DeserializeObject<Entities.GeneralInformation>(responseContent);
                this.Email.Text = genInfo.account.email;
                this.FirstName.Text = genInfo.firstName;
                this.LastName.Text = genInfo.lastName;
                this.Phone.Text = genInfo.phone;
                this.Birthday.Text = genInfo.birthday.ToString();
                this.Gender.Text = genInfo.gender.ToString();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBlock_SelectionChanged(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        private void Phone_SelectionChanged(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
    }
}
