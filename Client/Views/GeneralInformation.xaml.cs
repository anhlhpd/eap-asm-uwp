using Client.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
                this.Email.Text = genInfo.email;
                this.FirstName.Text = genInfo.firstName;
                this.LastName.Text = genInfo.lastName;
                this.Phone.Text = genInfo.phone;
                this.Birthday.Text = genInfo.address;
                this.Gender.SelectedValue = genInfo.gender.ToString();
                Debug.WriteLine(genInfo.gender);
            }
        }
}
