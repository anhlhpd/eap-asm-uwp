using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    }
}
