using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Publishing;
using Umbraco.Core.Services;


namespace USNStarterKit.USNBusinessLogic
{
    /// <summary>
    /// Summary description for RegisterEvents
    /// </summary>
    public class USNRegisterEvents : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            ContentService.Saved += Go;
        }

        private void Go(IContentService sender, SaveEventArgs<IContent> args)
        {
            foreach (var node in args.SavedEntities)
            {
                if (node.ContentType.Alias == "USNStandardPagelevel1" || node.ContentType.Alias == "USNStandardPagelevel2" || node.ContentType.Alias == "USNStandardPagelevel3" || node.ContentType.Alias == "USNStandardPageBlogPost")
                {

                    if (node.Children().Count() == 0)
                    {
                        var c = ApplicationContext.Current.Services.ContentService;
                        var content = c.CreateContent("Page Components", node.Id, "USNStandardPageComponents");
                        c.SaveAndPublishWithStatus(content);
                    }
                    else
                    {
                        bool found = false;

                        foreach (var child in node.Children())
                        {
                            if (child.Name == "Page Components")
                            {
                                found = true;
                            }
                        }
                        if (found == false)
                        {
                            var c = ApplicationContext.Current.Services.ContentService;
                            var content = c.CreateContent("Page Components", node.Id, "USNStandardPageComponents");
                            c.SaveAndPublishWithStatus(content);
                        }
                    }
                }
                else if (node.ContentType.Alias == "USNHomepage" || node.ContentType.Alias == "USNAdvancedPageLevel1" || node.ContentType.Alias == "USNAdvancedPageLevel2" || node.ContentType.Alias == "USNAdvancedPageLevel3")
                {
                    if (node.Children().Count() == 0)
                    {
                        var c = ApplicationContext.Current.Services.ContentService;
                        var content = c.CreateContent("Page Components", node.Id, "USNAdvancedPageComponents");
                        c.SaveAndPublishWithStatus(content);
                    }
                    else
                    {
                        bool found = false;

                        foreach (var child in node.Children())
                        {
                            if (child.Name == "Page Components")
                            {
                                found = true;
                            }
                        }
                        if (found == false)
                        {
                            var c = ApplicationContext.Current.Services.ContentService;
                            var content = c.CreateContent("Page Components", node.Id, "USNAdvancedPageComponents");
                            c.SaveAndPublishWithStatus(content);
                        }
                    }
                }
            }
        }

        
    }

}