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
  public partial class MainPage : PhoneApplicationPage
  {

    private string _todayUrl = "";

    // Constructor
    public MainPage()
    {
      InitializeComponent();

      // Set the data context of the listbox control to the sample data
      DataContext = App.ViewModel;
      this.Loaded += new RoutedEventHandler(MainPage_Loaded);
    }

    // Load data for the ViewModel Items
    private void MainPage_Loaded(object sender, RoutedEventArgs e) {
      if (!App.ViewModel.IsDataLoaded) {
        App.ViewModel.LoadData();
        LoadDescription();
        TodayButton.Click += TodayButton_Click; 
       
      }
    }

    void TodayButton_Click(object sender, RoutedEventArgs e)
    {
      string urlTmpl = @"/TodayPage.xaml?url={0}";
      NavigationService.Navigate(new Uri(string.Format(urlTmpl, _todayUrl), UriKind.Relative));
    }

    private void LoadDescription()
    {
      WebClient client = new WebClient();
      client.DownloadStringCompleted += LoadDescriptionCompleted;
      client.DownloadStringAsync(new Uri("http://apod.nasa.gov/apod/astropix.html"));
    }

    private void LoadDescriptionCompleted(object sender, DownloadStringCompletedEventArgs e)
    {
      string html = e.Result;
      try
      {
        
        string one = html.Substring(html.IndexOf("Explanation:"));
        string two = one.Substring(one.IndexOf(">") + 1);
        string three = two.Substring(0, two.IndexOf(@"<p>"));
        Explanation.Text = three;
      }
      catch
      {
        Explanation.Text = "Visit the NASA web site for a description.";
      }

      string imgroot = "http://apod.nasa.gov/apod/";

      try
      {
        string four = html.Substring(html.IndexOf("<IMG SRC=") + 10);
        string url = four.Substring(0, four.IndexOf('"'));
        _todayUrl = imgroot + url; 
      }
      catch
      {
        _todayUrl = "http://apod.nasa.gov/apod/calendar/today.jpg"; 
      }

    }

 
    private void OlderButton_Click(object sender, RoutedEventArgs e)
    {
      Button b = sender as Button;
      string urlTmpl = @"/TodayPage.xaml?url={0}";
      ApodItem item = b.Tag as ApodItem;
      NavigationService.Navigate(new Uri(string.Format(urlTmpl, item.PicUrl), UriKind.Relative));

    }
  }
}