using EPiServer.Core;
using EpiserverCms.Web.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpiserverCms.Web.Models.ViewModels
{
    public class HomePageViewModel : PageViewModel<HomePageData>
    {
        public HomePageViewModel(HomePageData currentPage)
            : base(currentPage)
        {

        }
    }
}