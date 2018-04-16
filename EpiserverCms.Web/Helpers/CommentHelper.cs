using EPiServer.Data.Dynamic;
using EpiserverCms.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpiserverCms.Web.Helpers
{
    public class CommentHelper
    {
        public static string GetCommentStore(int pageId)
        {
            return $"{Models.Constant.DynamicDataStoreList.COMMENT_STORE}_{pageId}";
        }

        public static IEnumerable<UserCommentViewModel> GetCommentByPage(string pageCommentStore)
        {
            var store = GetStoreByName(pageCommentStore);
            return store != null ? store.LoadAll<UserCommentViewModel>() : new List<UserCommentViewModel>();
        }

        private static DynamicDataStore GetStoreByName(string name)
        {
            return DynamicDataStoreFactory.Instance.GetStore(name);
        }
    }
}