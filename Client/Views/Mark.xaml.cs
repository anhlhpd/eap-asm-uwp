using Client.Service;
using Newtonsoft.Json;
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
using Client.Entities;
using Windows.System;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Client.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Mark : Page
    {
        //private ObservableCollection<Mark> listAllMarks;
        //internal ObservableCollection<Mark> ListAllMarks { get => listAllMarks; set => listAllMarks = value; }
        //private ICollection<Mark> listTheoryMarks;
        //private ObservableCollection<Mark> listPracticeMarks;
        //private ObservableCollection<Mark> listAsmMarks;
        //public ICollection<Mark> ListTheoryMarks { get => listTheoryMarks; set => listTheoryMarks = value; }
        //public ObservableCollection<Mark> ListPracticeMarks { get => listPracticeMarks; set => listPracticeMarks = value; }
        //public ObservableCollection<Mark> ListAsmMarks { get => listAsmMarks; set => listAsmMarks = value; }
        public Mark()
        {
            this.InitializeComponent();
            KeyboardAccelerator GoBack = new KeyboardAccelerator();
            GoBack.Key = VirtualKey.GoBack;
            GoBack.Invoked += BackInvoked;
            KeyboardAccelerator AltLeft = new KeyboardAccelerator();
            AltLeft.Key = VirtualKey.Left;
            AltLeft.Invoked += BackInvoked;
            this.KeyboardAccelerators.Add(GoBack);
            this.KeyboardAccelerators.Add(AltLeft);
            // ALT routes here
            AltLeft.Modifiers = VirtualKeyModifiers.Menu;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("start");
            Header.Text = GlobalVariable.CurrentSubectId;
            List<Entities.Mark> marks = new List<Entities.Mark>();
            var response = await APIHandle.Get_Member_Mark();
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Debug.WriteLine("1");
                Debug.WriteLine(responseContent);
                var list = JsonConvert.DeserializeObject<List<Entities.Mark>>(responseContent);
                var listFiletBySubject = list.Where(m => m.SubjectId == GlobalVariable.CurrentSubectId).ToList();
                GridViewTheory.ItemsSource = listFiletBySubject.Where(m => m.MarkType == Entities.MarkType.Theory).ToList();
                GridViewTheoryStatus.ItemsSource = listFiletBySubject.Where(m => m.MarkType == Entities.MarkType.Theory).ToList();

                GridViewPractice.ItemsSource = listFiletBySubject.Where(m => m.MarkType == Entities.MarkType.Practice).ToList();
                GridViewPracticeStatus.ItemsSource = listFiletBySubject.Where(m => m.MarkType == Entities.MarkType.Practice).ToList();

                GridViewAssignment.ItemsSource = listFiletBySubject.Where(m => m.MarkType == Entities.MarkType.Assignment).ToList();
                GridViewAssignmentStatus.ItemsSource = listFiletBySubject.Where(m => m.MarkType == Entities.MarkType.Assignment).ToList();
                foreach (var item in list)
                {
                    Debug.WriteLine(item.AccountId);
                }
                
            }
            //else
            //{
            //    var dialog = new ContentDialog()
            //    {
            //        Title = "Error!",
            //        MaxWidth = this.ActualWidth,
            //        Content = "There's an error! Please try later!",
            //        CloseButtonText = "OK!"
            //    };
            //    ErrorResponse errorObject = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);

            //    if (errorObject != null)
            //    {
            //        Debug.WriteLine(errorObject.message);
            //    }
            //}
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            BackButton.IsEnabled = this.Frame.CanGoBack;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            On_BackRequested();
        }

        // Handles system-level BackRequested events and page-level back button Click events
        private bool On_BackRequested()
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
                return true;
            }
            return false;
        }

        private void BackInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            On_BackRequested();
            args.Handled = true;
        }

    }
}
