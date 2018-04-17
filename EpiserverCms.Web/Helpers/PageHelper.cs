using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpiserverCms.Web.Helpers
{
    public class PageHelper
    {
        public static IContent GetCurrentPageDataOfBlock()
        {
            var routerHelper = ServiceLocator.Current.GetInstance<IContentRouteHelper>();
           return routerHelper.Content;
        }

        public static int GetPageId(PageData page)
        {
            return page.ContentLink.ID;
        }

        public static IEnumerable<PageData> GetAllChildrenPages(ContentReference parentPage)
        {
            var repository = ServiceLocator.Current.GetInstance<IContentRepository>();
            var allChildren = repository.GetChildren<PageData>(parentPage);
            return allChildren;
        }

        public static IEnumerable<PageData> FilterPagesByExistedProperty(IEnumerable<PageData> listPages, string property)
        {
            var filteredPages = listPages != null ? listPages.Where(x => x.Property.Contains(property)) : new List<PageData>();
            return filteredPages;
        }
    }
}