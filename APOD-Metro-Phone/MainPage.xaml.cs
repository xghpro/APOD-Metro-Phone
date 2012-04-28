using System;
using System.Collections.Generic;
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
        bitmap2.ImageOpened += new EventHandler<RoutedEventArgs>(bitmap2_ImageOpened);
        bitmap2.ImageFailed += new EventHandler<ExceptionRoutedEventArgs>(bitmap2_ImageFailed);
        
      }
    }

    void bitmap2_ImageFailed(object sender, ExceptionRoutedEventArgs e)
    {
      throw new NotImplementedException();
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