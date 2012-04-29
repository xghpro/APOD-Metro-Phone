using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace APOD_Metro_Phone {
  public partial class MainPage : PhoneApplicationPage {
    // Constructor
    public MainPage()
    {
      //LoadList();
      InitializeComponent();

      // Set the data context of the listbox control to the sample data
      DataContext = App.ViewModel;
      this.Loaded += new RoutedEventHandler(MainPage_Loaded);
    }

    // Load data for the ViewModel Items
    private void MainPage_Loaded(object sender, RoutedEventArgs e) {
      if (!App.ViewModel.IsDataLoaded) {
        App.ViewModel.LoadData();
        Uri uri = new Uri("http://apod.nasa.gov/apod/calendar/today.jpg");
        var bitmap2 = new BitmapImage(uri);
        bitmap2.CreateOptions = BitmapCreateOptions.None;
        //bitmap2.ImageOpened += bitmap2_ImageOpened;
        //http://apod.nasa.gov/apod/ap120427.html
        LoadDescription();
       
      }
    }

    private void LoadDescription()
    {
      WebClient client = new WebClient();
      client.DownloadStringCompleted += LoadDescriptionCompleted;
      client.DownloadStringAsync(new Uri("http://apod.nasa.gov/apod/ap120427.html"));

    }

    private void LoadDescriptionCompleted(object sender, DownloadStringCompletedEventArgs e)
    {
      string html = e.Result;
      string one = html.Substring(html.IndexOf("Explanation:"));
      string two = one.Substring(one.IndexOf(">"));
      string three = two.Substring(0, two.IndexOf(@"<p>"));
      Explanation.Text = three;
    }

  

    private void LoadListCompleted(object sender, DownloadStringCompletedEventArgs e)
    {
     
      ApodItem item = new ApodItem();
      UIList.Items.Add(item);
    }

    public ImageSource ResizeImage(ImageSource biInput, double DeltaX, double DeltaY)
    {
      WriteableBitmap wbOutput;
      ScaleTransform stTemp = new System.Windows.Media.ScaleTransform();

      stTemp.ScaleX = DeltaX;
      stTemp.ScaleY = DeltaY;

      Image imgTemp = new Image();
      imgTemp.Source = biInput;

      wbOutput = new WriteableBitmap(imgTemp, stTemp);
      var x = wbOutput.PixelWidth;
      var y = wbOutput.PixelHeight;

      return wbOutput;
    }
  }
}