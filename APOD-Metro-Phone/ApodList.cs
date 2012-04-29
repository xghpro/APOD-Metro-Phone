using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace APOD_Metro_Phone
{
  public class ApodItem
  {
    public string photo { get; set; }
    public string title { get; set; }
  }

  public class ApodList : ObservableCollection<ApodItem>
  {
    public ApodList()
    {

      // http://apod.nasa.gov/apod/calendar/S_120427.jpg
      var urltmpl = "http://apod.nasa.gov/apod/calendar/S_{0:00}{1:00}{2:00}.jpg";
      var date = DateTime.Now;
      
      for (int day = 0; day < 13; day++)
      {
        var today = date.AddDays(-day);
        var url = string.Format(urltmpl, today.Year-2000, today.Month, today.Day);
        Add(new ApodItem { photo = url, title = date.ToString()});

      }
      
    }

  }
}
