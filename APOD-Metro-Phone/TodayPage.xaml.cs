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
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace APOD_Metro_Phone
{
  public partial class TodayPage : PhoneApplicationPage
  {
    public TodayPage()
    {
      InitializeComponent();
    }

    private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
    {
      string htmlTmpl = "<body bgcolor='black' marginheight='0' marginwidth='0'><img height='100%' src='{0}'></body>";
      string url = NavigationContext.QueryString["url"]; 
      Browser.NavigateToString(string.Format(htmlTmpl,url));
    }
  }
}