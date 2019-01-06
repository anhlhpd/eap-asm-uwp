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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Client.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DemoMark : Page
    {
        private ObservableCollection<Mark> listAllMarks;
        internal ObservableCollection<Mark> ListAllMarks { get => listAllMarks; set => listAllMarks = value; }
        private ICollection<Mark> listTheoryMarks;
        private ObservableCollection<Mark> listPracticeMarks;
        private ObservableCollection<Mark> listAsmMarks;
        public ICollection<Mark> ListTheoryMarks { get => listTheoryMarks; set => listTheoryMarks = value; }
        public ObservableCollection<Mark> ListPracticeMarks { get => listPracticeMarks; set => listPracticeMarks = value; }
        public ObservableCollection<Mark> ListAsmMarks { get => listAsmMarks; set => listAsmMarks = value; }
        public DemoMark()
        {
            this.InitializeComponent();
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<Entities.Mark> marks = new List<Entities.Mark>();
            var response = await APIHandle.Get_Member_Mark();
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Debug.WriteLine("1");
                Debug.WriteLine(responseContent);
                var list = JsonConvert.DeserializeObject<List<Entities.Mark>>(responseContent);
                foreach (var obj in list)
                {

                    //Subject subject = obj.ToObject<Subject>();
                    //this.listAllSubjects.Add(subject);
                    Debug.WriteLine(obj.AccountId.ToString());
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
    }
}
