using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpiserverCms.Web.Models.ViewModels
{
    public class PluginCommentViewModel
    {
        public IEnumerable<UserCommentViewModel> ListComment { get; set; }
        public  IEnumerable<PageData> ListPages { get; set; }

        public PageData SelectedPage => ListPages?.FirstOrDefault();

        public int SelectedPagedId { get; set; }
    }
}