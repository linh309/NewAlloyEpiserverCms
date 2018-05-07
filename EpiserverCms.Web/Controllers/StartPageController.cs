using System.Web.Mvc;
using EpiserverCms.Web.Models.Pages;
using EpiserverCms.Web.Models.ViewModels;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using EPiServer.ServiceLocation;
using EPiServer;
using EPiServer.Core;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Globalization;
using System.Text;
using EPiServer.Globalization;
using EPiServer.Web.Routing;

namespace EpiserverCms.Web.Controllers
{
    public class StartPageController : PageControllerBase<StartPage>
    {
        public ActionResult Index(StartPage currentPage, bool isDeleted = false)
        {
            //var masterLanguage = currentPage.MasterLanguage;
            //var repo = ServiceLocator.Current.GetInstance<IContentRepository>();            
            //var startPage= repo.Get<PageData>(PageReference.StartPage, masterLanguage);
            //var errorPages = CreatePagesForLanguage(startPage.ContentLink, "sv", repo);
            //var listAllChildren = GetAllChildren(startPage.ContentLink);

            var alloyPlanPage = new ContentReference(6);

            var urlAlloyPlan = UrlResolver.Current.GetUrl(alloyPlanPage);


            var model = PageViewModel.Create(currentPage);
            if (SiteDefinition.Current.StartPage.CompareToIgnoreWorkID(currentPage.ContentLink)) // Check if it is the StartPage or just a page of the StartPage type.
            {
                //Connect the view models logotype property to the start page's to make it editable
                var editHints = ViewData.GetEditHints<PageViewModel<StartPage>, StartPage>();
                editHints.AddConnection(m => m.Layout.Logotype, p => p.SiteLogotype);
                editHints.AddConnection(m => m.Layout.ProductPages, p => p.ProductPageLinks);
                editHints.AddConnection(m => m.Layout.CompanyInformationPages, p => p.CompanyInformationPageLinks);
                editHints.AddConnection(m => m.Layout.NewsPages, p => p.NewsPageLinks);
                editHints.AddConnection(m => m.Layout.CustomerZonePages, p => p.CustomerZonePageLinks);
            }

            return View(model);
        }

        private IList<PageData> GetAllChildren(ContentReference page)
        {
            var listChildren = new List<PageData>();
            var repo = ServiceLocator.Current.GetInstance<IContentRepository>();
            var pageData = repo.Get<PageData>(page);
            var children = repo.GetChildren<PageData>(page);
            listChildren.Add(pageData);

            foreach (var item in children)
            {
                var listSubChilrren = GetAllChildren(item.ContentLink);
                listChildren.AddRange(listSubChilrren);
            }

            return listChildren;
        }

        private string CreatePagesForLanguage(ContentReference startPage, string language, IContentRepository repo)
        {
            var errorMsg = new StringBuilder();
            var startPageData = repo.Get<PageData>(startPage);
            var children = repo.GetChildren<PageData>(startPage);

            var newLanguagePage = repo.CreateLanguageBranch<PageData>(startPage, new LanguageSelector(language));
            var listExcludedProperty = new List<string> { "PageMasterLanguageBranch", "PageLanguageBranch" };
            foreach (var prop in startPageData.Property)
            {
                if (!listExcludedProperty.Contains(prop.Name))
                {
                    newLanguagePage.Property.Remove(prop.Name);
                    var cloneProperty = prop.CreateWritableClone();
                    cloneProperty.IsModified = true;
                    newLanguagePage.Property.Add(cloneProperty);
                }
            }

            try
            {
                var existedNewLanguagePage = repo.Get<PageData>(startPageData.ContentLink, CultureInfo.GetCultureInfo(language));
                if (existedNewLanguagePage == null)
                {
                    repo.Save(newLanguagePage, EPiServer.DataAccess.SaveAction.Publish);
                }
            }
            catch (Exception ex)
            {
                errorMsg.AppendLine($"\nName: {startPageData.Name}, ID: {startPageData.ContentLink.ID}, Exception: {ex}");
            }

            foreach (var item in children)
            {
                var msg = CreatePagesForLanguage(item.ContentLink, language, repo);
                errorMsg.Append(msg);
            }

            return errorMsg.ToString();
        }

        private void DeleteAllPageInLanguage(IEnumerable<PageData> listPages, string language)
        {
            var repo = ServiceLocator.Current.GetInstance<IContentRepository>();
            foreach (var childPage in listPages)
            {
                var childPageData = repo.Get<PageData>(childPage.ContentLink, CultureInfo.GetCultureInfo("sv"));
                if (childPageData != null)
                {
                    repo.DeleteLanguageBranch(childPage.ContentLink, language, EPiServer.Security.AccessLevel.Delete);
                }
            }
        }
    }
}
