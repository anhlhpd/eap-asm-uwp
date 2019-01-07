using Client.Entities;
using Client.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Client.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListSubject : Page
    {
        private ObservableCollection<Subject> listAllSubjects;
        internal ObservableCollection<Subject> ListAllSubjects { get => listAllSubjects; set => listAllSubjects = value; }
        public ListSubject()
        {
            this.InitializeComponent();
            Get_List_Subject();
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
        }

        private async void Get_List_Subject()
        {
            this.listAllSubjects = new ObservableCollection<Subject>();
            
            var response = await APIHandle.Get_Subjects();
            var responseContent = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(response);
            Debug.WriteLine(responseContent);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var array = JArray.Parse(responseContent);
                foreach (var obj in array)
                {
                    Subject subject = obj.ToObject<Subject>();
                    this.listAllSubjects.Add(subject);
                    Debug.WriteLine(subject.name);
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

        private void StackPanel_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Debug.WriteLine("tap");
            StackPanel sp = sender as StackPanel;
            Subject chosseSubject = sp.Tag as Subject;
            GlobalVariable.CurrentSubjectName = chosseSubject.name;
            GlobalVariable.CurrentSubectId = chosseSubject.id;
            Debug.WriteLine("end tap");
            this.Frame.Navigate(typeof(Mark));
            Debug.WriteLine("end nav");
        }

        private void App_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            e.Handled = On_BackRequested();
        }

        private void On_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            bool isXButton1Pressed =
                e.GetCurrentPoint(sender as UIElement).Properties.PointerUpdateKind == PointerUpdateKind.XButton1Pressed;

            if (isXButton1Pressed)
            {
                e.Handled = On_BackRequested();
            }
        }

        private bool On_BackRequested()
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
                return true;
            }
            return false;
        }
    }
}
