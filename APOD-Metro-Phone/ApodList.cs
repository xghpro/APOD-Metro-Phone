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
using HtmlAgilityPack;

namespace APOD_Metro_Phone
{
  public class ApodItem
  {
    public string DateString { get; set; }
    public string ThumbUrl { get; set; }
    public string Title { get; set; }
    public string PageUrl { get; set; }
    public string PicUrl { get; set; }
  }

  public class ApodList : ObservableCollection<ApodItem>
  {
    public ApodList()
    {

      //thumb http://apod.nasa.gov/apod/calendar/S_120427.jpg
      //page http://apod.nasa.gov/apod/ap120429.html

      var thumbtmpl = "http://apod.nasa.gov/apod/calendar/S_{0:00}{1:00}{2:00}.jpg";
      var pagetmpl = "http://apod.nasa.gov/apod/ap{0:00}{1:00}{2:00}.html";
      var date = DateTime.Now;

      for (int day = 0; day < 13; day++)
      {
        var today = date.AddDays(-day);
        var thumb = string.Format(thumbtmpl, today.Year - 2000, today.Month, today.Day);
        var page = string.Format(pagetmpl, today.Year - 2000, today.Month, today.Day);
        ApodItem item = new ApodItem { ThumbUrl = thumb, DateString = today.ToLongDateString(), Title = "title", PageUrl = page };
        Add(item);
        LoadPicture(page,item);

      }
    }

    private void LoadPicture(string pageurl, ApodItem item)
    {
      WebClient client = new WebClient();
      client.DownloadStringCompleted += LoadPictureCompleted;
      client.DownloadStringAsync(new Uri(pageurl),item);

    }

    private void LoadPictureCompleted(object sender, DownloadStringCompletedEventArgs e)
    {
      string html = e.Result;
      ApodItem item = e.UserState as ApodItem;
      string imgroot = "http://apod.nasa.gov/apod/";

      string one = html.Substring(html.IndexOf("<IMG SRC=")+10);
      string url = one.Substring(0, one.IndexOf('"'));
      item.PicUrl = imgroot+url;

    }

  }
}
