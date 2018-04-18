using EPiServer.Data;
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
        private static string _commentStoreName = Models.Constant.DynamicDataStoreList.COMMENT_STORE;

        public static string GetCommentStoreName()
        {
            return Models.Constant.DynamicDataStoreList.COMMENT_STORE;
        }

        public static IEnumerable<UserCommentViewModel> GetCommentByPage(string pageCommentStore)
        {
            var store = GetStoreByName(pageCommentStore);
            return store != null ? store.LoadAll<UserCommentViewModel>() : new List<UserCommentViewModel>();
        }

        public static IEnumerable<UserCommentViewModel> GetCommentByPageCondition(IDictionary<string, object> conditions)
        {
            var store = GetStoreByName(_commentStoreName);
            if (store != null)
            {
                return store.Find<UserCommentViewModel>(conditions);
            }

            return new List<UserCommentViewModel>();
        }

        public static Identity UpdateComment(string pageCommentStore, UserCommentViewModel comment)
        {
            var store = GetStoreByName(pageCommentStore);
            return store.Save(comment);
        }

        public static void Delete(string pageCommentStore, Identity id)
        {
            var store = GetStoreByName(pageCommentStore);
            store.Delete(id);
        }

        public static UserCommentViewModel GetCommentById(string pageCommentStore, Identity id)
        {
            var store = GetStoreByName(pageCommentStore);
            return store.Load<UserCommentViewModel>(id);
        }

        private static DynamicDataStore GetStoreByName(string name)
        {
            return DynamicDataStoreFactory.Instance.GetStore(name);
        }
    }
}