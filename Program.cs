using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using SimpleFeedReader;
using Telegram.Bot;
using Telegram.Bot.Args;


namespace TAG_TGM_Messaging_App
{
    /// To learn more about Microsoft Azure WebJobs SDK, please see https://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Add the api token got from @Botfather from telegram
        private static readonly TelegramBotClient TAGbot = new TelegramBotClient("Get the api key from @botfather");
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            var config = new JobHostConfiguration();

            if (config.IsDevelopment)
            {
                config.UseDevelopmentSettings();
            }
            TAGbot.OnMessage += TAGbot_ONMessage;
            TAGbot.OnMessageEdited += TAGbot_ONMessage;

            // RUN this code to test whether u r able to post message to BOT and BOT Is able to reply
            TAGbot.StartReceiving();
            Console.WriteLine("TAG007_bot is listening..... to user requests");
            Console.ReadLine();
            Console.WriteLine("TAG007_bot is Winding up.....");
            TAGbot.StopReceiving();
            //

            var host = new JobHost(config);
            // The following code ensures that the WebJob will be running continuously
            host.RunAndBlock();
        }
        private static void TAGbot_ONMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.TextMessage)
            {
                if (e.Message.Text == "/Azure_Link_Please")
                    TAGbot.SendTextMessageAsync(e.Message.Chat.Id,"Hi"+" "+ e.Message.Chat.Username + ":" + "  Here is the Azure Website link: https://azure.microsoft.com");
                else if (e.Message.Text == "/AWS_Link_Please")
                {
                    TAGbot.SendTextMessageAsync(e.Message.Chat.Id, "Hi" + " " + e.Message.Chat.Username + ":" + "  Here is the Amazon Web Services Website link: https://aws.amazon.com/");
                    //TAGbot.SendTextMessageAsync(e.Message.Chat.Id, "https://aws.amazon.com/");
                }
                else if (e.Message.Text == "/Give_Me_Recent_Azure_Updates")
                {
                    var reader = new FeedReader();
                    var items = reader.RetrieveFeed("https://azure.microsoft.com/en-us/updates/feed/");

                    foreach (var i in items)
                    {
                        TAGbot.SendTextMessageAsync(e.Message.Chat.Id, string.Format("{0}\t{1}",
                                i.Date.ToString("g"),
                                i.Title));
                    }
                }
                else if (e.Message.Text == "/Give_Me_Recent_AWS_Updates")
                {
                    var reader = new FeedReader();
                    var items = reader.RetrieveFeed("https://aws.amazon.com/new/feed/");

                    foreach (var i in items)
                    {
                        TAGbot.SendTextMessageAsync(e.Message.Chat.Id, string.Format("{0}\t{1}",
                                i.Date.ToString("g"),
                                i.Title));
                    }
                }
                else if (e.Message.Text == "/Give_Me_AWS_Status")
                {
                    var reader = new FeedReader();
                    var items = reader.RetrieveFeed("http://status.aws.amazon.com/rss/all.rss");

                    foreach (var i in items)
                    {
                        TAGbot.SendTextMessageAsync(e.Message.Chat.Id, string.Format("{0}\t{1}",
                                i.Date.ToString("g"),
                                i.Title));
                    }
                }
                else if (e.Message.Text == "/Give_Me_Azure_Status")
                {
                    var reader = new FeedReader();
                    var items = reader.RetrieveFeed("http://azure.microsoft.com/en-us/status");

                    if (items.Count().Equals(0))
                    {
                        TAGbot.SendTextMessageAsync(e.Message.Chat.Id, "Hi" + " " + e.Message.Chat.Username + ":" + @"  Looks like all the services in azure are up and runing without issues :" +
                            " Click the link to manually check http://azure.microsoft.com/en-us/status ");
                    }
                    else
                    {
                        foreach (var i in items)
                        {
                            TAGbot.SendTextMessageAsync(e.Message.Chat.Id, string.Format("{0}\t{1}",
                                    i.Date.ToString("g"),
                                    i.Title));
                        }
                    }
                    
                }
                else if (e.Message.Text == "/Give_Me_New_Blogs_List_On_Azure")
                {
                    var reader = new FeedReader();
                    var items = reader.RetrieveFeed("https://azure.microsoft.com/en-us/blog/feed/");

                    foreach (var i in items)
                    {
                        TAGbot.SendTextMessageAsync(e.Message.Chat.Id, string.Format("{0}\t{1}",
                                i.Date.ToString("g"),
                                i.Title));
                    }
                }
                else if (e.Message.Text == "/Who_is_your_father")
                {
                    TAGbot.SendTextMessageAsync(e.Message.Chat.Id, "My Father is :  " + @"GirishKalamati");
                }
                else
                {
                    TAGbot.SendTextMessageAsync(e.Message.Chat.Id, "Hi" + " " + e.Message.Chat.Username +" ! "+ @" 
                    Hope u r doing great today <(*v*)>
You can ask any of the below questions (Just Click on questions i will respond you back)
                    /Azure_Link_Please
                    /AWS_Link_Please
                    /Give_Me_Recent_Azure_Updates
                    /Give_Me_Recent_AWS_Updates
                    /Give_Me_AWS_Status
                    /Give_Me_Azure_Status
                    /Give_Me_New_Blogs_List_On_Azure
                    /Who_is_your_father                    
                    ");
                }
            }
        }
    }
}
