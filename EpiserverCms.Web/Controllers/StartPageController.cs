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

namespace EpiserverCms.Web.Controllers
{
    public class StartPageController : PageControllerBase<StartPage>
    {
        public ActionResult Index(StartPage currentPage)
        {
            var repo = ServiceLocator.Current.GetInstance<IContentRepository>();
            var startPage = repo.GetChildren<SitePageData>(PageReference.StartPage);

            var content = GetAllChildren("", " ", PageReference.StartPage);

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

        public string GetAllChildren(string content, string padding, PageReference page)
        {
            var repo = ServiceLocator.Current.GetInstance<IContentRepository>();
            var pageData = repo.Get<PageData>(page);
            var children = repo.GetChildren<SitePageData>(page);
            content += "\n" + padding + " " + pageData.Name;

            var baseTypeOfPageData = pageData.GetType().BaseType;
            var pageProperties = baseTypeOfPageData
                .GetProperties()
                .Where(p => p.DeclaringType != null && p.DeclaringType.Name == baseTypeOfPageData.Name)                
                .ToList();

            if (pageData.Name == "Alloy Track")
            {
                var newLanguage = repo.CreateLanguageBranch<PageData>(page, new LanguageSelector("sv"));
                newLanguage.Name = pageData.Name;
                foreach (var property in newLanguage.Property)
                {
                    if (pageProperties.Select(p=>p.Name).Contains(property.Name))
                    {
                        var pPro = pageProperties.Where(p1 => p1.Name == property.Name).FirstOrDefault();                        
                        var propertyValue = pageData.GetPropertyValue(property.Name);

                        if (property.Name== "UniqueSellingPoints")
                        {
                            //var xType = typeof(IList<string>);
                            var propValue = pageData.GetPropertyValue<Type>(property.Name);
                        }

                        if (!property.IsReadOnly)
                        {
                            property.Value = propertyValue;
                        }
                    }
                }
                //var copied = repo.Save(newLanguage, EPiServer.DataAccess.SaveAction.Publish);
            }

            foreach (var item in children)
            {
                //content += "\n" + padding + " " + item.Name;
                var nextLevalPadding = padding + "  ";
                content = GetAllChildren(content, nextLevalPadding, item.ContentLink as PageReference);
            }
            return content;
        }
    }
}
