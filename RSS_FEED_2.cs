using CodeHollow.FeedReader;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace k163825_Q4
{
    class RSS_FEED_2 : IFeed
    {
        public List<NewsItem> getFeed()
        {

            var samaa_RssFeed = FeedReader.Read(ConfigurationManager.AppSettings["Samaa_RSS_FEED"]);
            List<NewsItem> newsItems = new List<NewsItem>();
            

            foreach (var item in samaa_RssFeed.Items)
            {
                NewsItem newsItem = new NewsItem();
                newsItem.Title = item.Title;
                newsItem.Description = item.Description;
                newsItem.NewsChannel = "Samaa";
                var date = DateTime.ParseExact(
   item.PublishingDateString,
   "ddd, dd MMM yyyy HH:mm:ss K",
   CultureInfo.InvariantCulture);
                newsItem.PublishedDate = date;

                newsItems.Add(newsItem);
                newsItem = null;
               // send(newsItems);
               // newsItems.Remove(newsItem);
            }
            return newsItems;
        }
    }
}
