using Client.Service;
using Client.Entities;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.ViewManagement;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using System.Net;

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
            //ApplicationView.PreferredLaunchViewSize = new Size(3000, 2000);
            //ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
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
                GlobalVariable.AccountId = genInfo.accountId;

                this.EditEmail.Text = genInfo.account.email;
                this.EditFirstName.Text = genInfo.firstName;
                this.EditLastName.Text = genInfo.lastName;
                this.EditPhone.Text = genInfo.phone;
            }
        }

        private async void BtnLogout(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ContentDialog deleteFileDialog = new ContentDialog
            {
                Title = "Warning!",
                Content = "Do you want to log out?",
                PrimaryButtonText = "Log out",
                CloseButtonText = "Cancel"
            };

            ContentDialogResult result = await deleteFileDialog.ShowAsync();

            // Delete the file if the user clicked the primary button.
            /// Otherwise, do nothing.
            if (result == ContentDialogResult.Primary)
            {
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                StorageFile file = await storageFolder.GetFileAsync("token.txt");
                await file.DeleteAsync(StorageDeleteOption.PermanentDelete);

                var rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(Login), null, new EntranceNavigationTransitionInfo());
            }
            else
            {
                // The user clicked the CLoseButton, pressed ESC, Gamepad B, or the system back button.
                // Do nothing.
            }

        }
        public bool Validate_Edit()
        {
            var val = true;
            var firstname = this.EditFirstName.Text;
            var lastname = this.EditLastName.Text;
            var email = this.EditEmail.Text;
            var phone = this.EditPhone.Text;
            var password = this.EditPassword.Password;
            if (email == "" || firstname == "" || lastname == "" || phone == "" || password == "")
            {
                this.Error.Text = "You have to fill in all the fields.";
                val = false;
            }
            return val;
        }

        public async void BtnSave(object sender, RoutedEventArgs e)
        {
            if (Validate_Edit())
            {
                Entities.Account account = new Entities.Account()
                {
                    id = GlobalVariable.AccountId,
                    email = this.EditEmail.Text,
                    password = this.EditPassword.Password,
                    generalInformation = new Entities.GeneralInformation()
                    {
                        firstName = this.EditFirstName.Text,
                        lastName = this.EditLastName.Text,
                        phone = this.EditPhone.Text,
                    }
                };
                var response = await APIHandle.Edit_General_Infor(account);
                var responseContent = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(responseContent);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    this.Error.Text = "";
                    var dialog = new ContentDialog()
                    {
                        Title = "Success!",
                        MaxWidth = this.ActualWidth,
                        Content = "You have edited your general Information",
                        CloseButtonText = "OK!"
                    };
                }
                else
                {
                    ErrorResponse errorObject = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);

                    if (errorObject != null)
                    {
                        this.Error.Text = errorObject.message;
                    }                    
                }
            }            
        }
    }
}
