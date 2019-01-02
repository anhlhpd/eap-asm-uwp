using Client.Entities;
using Client.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Client.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
        }

        public bool Validate_Login()
        {
            var val = true;
            var usernameText = this.Username.Text;
            var passwordText = this.Password.Password;
            if (usernameText == "")
            {
                this.username.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                this.username.Text = "Email can't be empty";
                val = false;
            }
            else
            {
                this.username.Text = "";
            }
            if (passwordText == "")
            {
                this.password.Text = "Password can't be empty";
                this.password.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                val = false;
            }
            else
            {
                this.password.Text = "";
            }

            return val;
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("alert");
            
            var response = await APIHandle.Login(this.Username.Text, this.Password.Password);
            var responseContent = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(responseContent);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

                StorageFolder folder = ApplicationData.Current.LocalFolder;
                StorageFile file = await folder.CreateFileAsync("token.txt", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(file, responseContent);
                Debug.WriteLine("Success");

                var rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(NavigationView), null, new EntranceNavigationTransitionInfo());
            }
            else
            {
                Debug.WriteLine("Fail");
            }
        }
    }
}
