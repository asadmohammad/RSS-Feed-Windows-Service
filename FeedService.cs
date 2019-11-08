using CodeHollow.FeedReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Serialization;

namespace k163825_Q4
{
    public partial class FeedService : ServiceBase
    {
        System.Timers.Timer timer;
        string xmlPath = ConfigurationManager.AppSettings["Path"];
        
        public FeedService()
        {
            InitializeComponent();
            timer = new System.Timers.Timer();
            timer.Interval = 5 * 60 * 1000;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(feedWorking);
        }

        protected override void OnStart(string[] args)
        {
            timer.AutoReset = true;
            timer.Enabled = true;
            LogService("Service Started");
        }
        private void feedWorking(object sender, ElapsedEventArgs e)
        {
            RSS_FEED_1 nation = new RSS_FEED_1();
            RSS_FEED_2 samaa = new RSS_FEED_2();
            FileStream file = new FileStream(xmlPath, FileMode.Create, FileAccess.Write);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<NewsItem>));
            List<NewsItem> nation_feed = nation.getFeed();
            List<NewsItem> samaa_feed = samaa.getFeed();

            nation_feed.AddRange(samaa_feed);
            nation_feed.Sort((news1, news2) => news1.PublishedDate.CompareTo(news2.PublishedDate));
            
                
            xmlSerializer.Serialize(file, nation_feed);
            
            

            
            
        }
        

        
        protected override void OnStop()
        {
            timer.AutoReset = false;
            timer.Enabled = false;
            LogService("Service Stopped");
        }

        private void LogService(string content)
        {
            string logPath = ConfigurationManager.AppSettings["LogPath"];
            //Folder Must Exists
            FileStream fs = new FileStream(logPath, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine(content);
            sw.Flush();
            sw.Close();
        }
    }
}
