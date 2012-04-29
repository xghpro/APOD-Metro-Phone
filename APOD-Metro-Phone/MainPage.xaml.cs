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
    public MainPage() {
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


    void bitmap2_ImageOpened(object sender, RoutedEventArgs e)
    {
      var bitmap2 = (BitmapImage) sender;
      var x = 1024f / bitmap2.PixelWidth;
      var y = 800f / bitmap2.PixelHeight;

      var lcBrush2 = new ImageBrush()
      {
        Stretch = Stretch.UniformToFill,
        ImageSource = ResizeImage(bitmap2, x, y)
      };

      panoMain.Background = lcBrush2;
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