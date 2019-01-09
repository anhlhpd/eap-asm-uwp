using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Client.Entities;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Client.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NavigationView : Page
    {
       public NavigationView()
        {
            this.InitializeComponent();
        }

        private void TogglePanelEvent(object sender, TappedRoutedEventArgs e)
        {
            PaneSplitView.IsPaneOpen = !this.PaneSplitView.IsPaneOpen;
        }

        private async void LogoutHandle(object sender, TappedRoutedEventArgs e)
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

                
                this.Frame.Navigate(typeof(Views.Login));
            }
            else
            {
                // The user clicked the CLoseButton, pressed ESC, Gamepad B, or the system back button.
                // Do nothing.
            }
            
        }

        private void InfomationHandle(object sender, RoutedEventArgs e)
        {
            PageContent.Navigate(typeof(Views.GeneralInformation));
        }

        private void SubjectHandle(object sender, RoutedEventArgs e)
        {
            PageContent.Navigate(typeof(Views.ListSubject));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalVariable.CurrentFrame == "Mark")
            {
                PageContent.Navigate(typeof(Views.ListSubject));
                GlobalVariable.CurrentFrame = null;
            }
            if (GlobalVariable.CurrentFrame == "Class")
            {
                PageContent.Navigate(typeof(Views.ListClass));
                GlobalVariable.CurrentFrame = null;
            }
        }

        private void ClassHandle(object sender, RoutedEventArgs e)
        {
            PageContent.Navigate(typeof(Views.ListClass));
        }
    }

}
