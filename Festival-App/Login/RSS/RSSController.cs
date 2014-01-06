using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Festival.RSS;

namespace Festival.RSS
{
    public class RSSController : Controller
    {
        //
        // GET: /RSS/

        public RssActionResult Go()
        {
            RSSSQLRepository myRepo = new RSSSQLRepository();

            SyndicationFeedItemMapper<MyFeedItem> mapper = SetUpFeedMapper();
            SyndicationFeedOptions options = SetUpFeedOptions();
            IList<MyFeedItem> feedItems = myRepo.GetAllRSS();
            SyndicationFeedHelper<MyFeedItem> feedHelper = SetUpFeedHelper(mapper, options, feedItems);
            return new RssActionResult(feedHelper.GetFeed());
        }

        private static SyndicationFeedOptions SetUpFeedOptions()
        {
            SyndicationFeedOptions options = new SyndicationFeedOptions
                (
                    "Ultra Music Festival",
                    "De laatste nieuwtjes over het Ultra Music Festival",
                    "http://localhost/"
                );
            return options;
        }

        private static SyndicationFeedItemMapper<MyFeedItem> SetUpFeedMapper()
        {
            SyndicationFeedItemMapper<MyFeedItem> mapper = new SyndicationFeedItemMapper<MyFeedItem>
                (
                    f => f.Title,
                    f => f.Description,
                    "RSS",
                    "Articles",
                    f => f.Id.ToString(),
                    f => f.DateAdded
                );
            return mapper;
        }

        private SyndicationFeedHelper<MyFeedItem> SetUpFeedHelper(SyndicationFeedItemMapper<MyFeedItem> mapper,
            SyndicationFeedOptions options, IList<MyFeedItem> feedItems)
        {
            SyndicationFeedHelper<MyFeedItem> feedHelper = new SyndicationFeedHelper<MyFeedItem>
                (
                    this.ControllerContext,
                    feedItems,
                    mapper,
                    options
                );
            return feedHelper;
        }
    }
}
