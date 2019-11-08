using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using CodeHollow.FeedReader;
using System.Xml.Serialization;
using System.Globalization;

namespace k163825_Q4
{
    class RSS_FEED_1 : IFeed
    {
        public List<NewsItem> getFeed()
        {
            var nationRF = ConfigurationManager.AppSettings["Nation_RSS_FEED"];
            var nation_RssFeed = FeedReader.Read(nationRF);
            List<NewsItem> newsItems = new List<NewsItem>();
            

            foreach (var item in nation_RssFeed.Items)
            {
                NewsItem newsItem = new NewsItem();
                newsItem.Title = item.Title;
                newsItem.Description = item.Description;
                newsItem.NewsChannel = nation_RssFeed.Description;
                var date = DateTime.ParseExact(
   item.PublishingDateString,
   "ddd, dd MMM yyyy HH:mm:ss K",
   CultureInfo.InvariantCulture);
                newsItem.PublishedDate = date;

                newsItems.Add(newsItem);
                newsItem = null; 
            }

            return newsItems;
        }
    }
}
