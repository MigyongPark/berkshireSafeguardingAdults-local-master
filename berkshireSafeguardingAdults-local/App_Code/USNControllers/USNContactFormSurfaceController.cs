﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Umbraco.Core.Models;
using createsend_dotnet;
using USNStarterKit.USNBusinessLogic;
using USNStarterKit.USNModels;
using Umbraco.Web;
using System.Web.UI.WebControls;

namespace USNStarterKit.USNControllers
{
    /// <summary>
    /// Summary description for USNContactFormSurfaceController
    /// </summary>
    public class USNContactFormSurfaceController : Umbraco.Web.Mvc.SurfaceController
    {

        public ActionResult Index(int NodeID, string PageType)
        {
            var model = new USNContactFormViewModel();
            model.CurrentNodeID = NodeID;
            model.PageType = PageType;

            return PartialView("USNForms/USN_ContactForm", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HandleContactSubmit(USNContactFormViewModel model)
        {
            System.Threading.Thread.Sleep(1000);

            string returnValue = "";

            if (!ModelState.IsValid)
            {
                return JavaScript(String.Format("$(ContactError{0}).show();$(ContactError{0}).html('{1}');", model.CurrentNodeID, umbraco.library.GetDictionaryItem("USN Contact Form General Error")));
            }

            //Need to get NodeID from hidden field. CurrentPage does not work with Ajax.BeginForm
            var currentNode = Umbraco.TypedContent(model.CurrentNodeID);

            IPublishedContent homeNode = currentNode.AncestorOrSelf(1);
            var settingsFolder = Umbraco.TypedContent(homeNode.GetProperty("websiteConfigurationNode").Value);
            var globalSettings = settingsFolder.Children.Where(x => x.DocumentTypeAlias == "USNGlobalSettings").First();

            string mailTo = currentNode.GetProperty("recipientEmailAddress").Value.ToString();
            string websiteName = globalSettings.GetProperty("websiteName").Value.ToString();
            string pageName = currentNode.Parent.Parent.Name;

            string errorMessage = "";

            if (!SendContactFormMail(model, mailTo, websiteName, pageName, out errorMessage))
            {
                return JavaScript(String.Format("$(ContactError{0}).show();$(ContactError{0}).html('<div class=\"info\"><p>{1}</p><p>{2}</p></div>');", model.CurrentNodeID, umbraco.library.GetDictionaryItem("USN Contact Form Mail Send Error"), errorMessage));
            }

            try
            {
                if (model.NewsletterSignup && globalSettings.HasValue("campaignMonitorAPIKey") &&
                    (globalSettings.HasValue("defaultCampaignMonitorSubscriberListID") || currentNode.HasValue("campaignMonitorSubscriberListID")))
                {
                    string subsciberListID = "";

                    if (currentNode.GetProperty("campaignMonitorSubscriberListID").Value.ToString() != String.Empty)
                        subsciberListID = currentNode.GetProperty("campaignMonitorSubscriberListID").Value.ToString();
                    else
                        subsciberListID = globalSettings.GetProperty("defaultCampaignMonitorSubscriberListID").Value.ToString();

                    AuthenticationDetails auth = new ApiKeyAuthenticationDetails(globalSettings.GetProperty("campaignMonitorAPIKey").Value.ToString());

                    Subscriber loSubscriber = new Subscriber(auth, subsciberListID);

                    List<SubscriberCustomField> customFields = new List<SubscriberCustomField>();

                    string lsSubscriberID = loSubscriber.Add(model.Email, model.FirstName + " " + model.LastName, customFields, false);
                }
            }
            catch (Exception ex)
            {
                return JavaScript(String.Format("$(ContactError{0}).show();$(ContactError{0}).html('<div class=\"info\"><p>{1}</p><p>{2}</p></div>');", model.CurrentNodeID, umbraco.library.GetDictionaryItem("USN Contact Form Signup Error"), ex.Message));
            }

            returnValue = String.Format("<div class=\"page_component alert alert-success alert-dismissible fade in\" role=\"alert\"><div class=\"info\">{0}</div></div>", currentNode.GetProperty("submissionMessage").Value.ToString());

            return Content(returnValue);
        }

        public static bool SendContactFormMail(USNContactFormViewModel model, string mailTo, string websiteName, string pageName, out string lsErrorMessage)
        {
            lsErrorMessage = String.Empty;

            try
            {
                //Create MailDefinition 
                MailDefinition md = new MailDefinition();
                string lsSendTo = String.Empty;

                //specify the location of template 
                md.BodyFileName = "/usn/emailtemplates/contactform.htm";
                md.IsBodyHtml = true;

                //Build replacement collection to replace fields in template 
                System.Collections.Specialized.ListDictionary replacements = new System.Collections.Specialized.ListDictionary();
                replacements.Add("<% formFirstName %>", model.FirstName == null ? "" : model.FirstName);
                replacements.Add("<% formLastName %>", model.LastName == null ? "" : model.LastName);
                replacements.Add("<% formEmail %>", model.Email == null ? "" : model.Email);
                replacements.Add("<% formPhone %>", model.Telephone == null ? "" : model.Telephone);
                replacements.Add("<% formMessage %>", model.Message == null ? "" : umbraco.library.ReplaceLineBreaks(model.Message));
                replacements.Add("<% WebsitePage %>", pageName);
                replacements.Add("<% WebsiteName %>", websiteName);

                lsSendTo = mailTo;

                //now create mail message using the mail definition object 
                System.Net.Mail.MailMessage msg = md.CreateMailMessage(lsSendTo, replacements, new System.Web.UI.Control());
                msg.ReplyToList.Add(model.Email);
                msg.Subject = websiteName + " Website: " + pageName + " Page Enquiry";

                //this uses SmtpClient in 2.0 to send email, this can be configured in web.config file.
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.Send(msg);

                return true;
            }
            catch (Exception ex)
            {
                lsErrorMessage = ex.Message;
            }

            return false;
        }
    }
}