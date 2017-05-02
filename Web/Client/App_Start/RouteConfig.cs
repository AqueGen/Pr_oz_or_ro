using System.Web.Mvc;
using System.Web.Routing;
using Kapitalist.Web.Client.Controllers;
using Kapitalist.Web.Client.Routes;

namespace Kapitalist.Web.Client
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.html/{*pathInfo}");
            routes.IgnoreRoute("Scripts/{*pathinfo}");
            routes.IgnoreRoute("Content/{*pathinfo}");


            routes.MapMvcAttributeRoutes();

   //         routes.MapRouteEx("Home", "", nameof(HomeController), nameof(HomeController.Index));
   //         routes.MapRouteEx("HomeTendersSearch", "ajax/Tenders/Template/{template}", nameof(HomeController), nameof(HomeController.GetTemplate));

   //         routes.MapRouteEx("ErrorMessage", "Error", nameof(MessagesController), nameof(MessagesController.Error));
			//routes.MapRouteEx("ConfirmationMessage", "Confirmation", nameof(MessagesController), nameof(MessagesController.Confirmation));

   //         routes.MapRouteEx("AccountLogin", "Account/Login", nameof(AccountController), nameof(AccountController.Login));
   //         routes.MapRouteEx("AccountRegistration", "Account/Registration", nameof(AccountController), nameof(AccountController.Register));
			//routes.MapRouteEx("AccountConfirmEmail", "Account/ConfirmEmail", nameof(AccountController), nameof(AccountController.ConfirmEmail));
			//routes.MapRouteEx("AccountForgotPassword", "Account/ForgotPassword", nameof(AccountController), nameof(AccountController.ForgotPassword));
   //         routes.MapRouteEx("AccountLogOff", "Account/LogOff", nameof(AccountController), nameof(AccountController.LogOff));

   //         routes.MapRouteEx("Tenders", "Tenders", "Tenders", "Index");
   //         routes.MapRouteEx("TendersFilter", "Tenders/{viewModel}", "Tenders", "Index");
   //         routes.MapRouteEx("TendersAjaxGetTemplate", "ajax/Tenders/Template/{template}", "Tenders", "GetTemplate");

   //         routes.MapRouteEx("Plans", "Plans", "Plans", "Index");
   //         routes.MapRouteEx("PlansFilter", "Plans/{viewModel}", "Plans", "Index");
   //         routes.MapRouteEx("PlansAjaxGetTemplate", "ajax/Plans/Template/{template}", "Plans", "GetTemplate");


   //         routes.MapRouteEx("TenderTenderInfo", "Tender/{tenderGuid}/Info", "Tender", "TenderInfo");
   //         routes.MapRouteEx("TenderDetails", "Tender/{tenderGuid}/Details", "Tender", "Details");
   //         routes.MapRouteEx("TenderEditTender", "Tender/{tenderGuid}/Edit/Tender", "Tender", "EditTender");
   //         routes.MapRouteEx("TenderItems", "Tender/{tenderGuid}/Items", "Tender", "Items");
   //         routes.MapRouteEx("TenderAddItem", "Tender/{tenderGuid}/Add/Item", "Tender", "AddItem");
   //         routes.MapRouteEx("TenderEditItem", "Tender/{tenderGuid}/Edit/Item/{itemId}", "Tender", "EditItem");
   //         routes.MapRouteEx("TenderAddLot", "Tender/{tenderGuid}/Add/Lot", "Tender", "AddLot");
   //         routes.MapRouteEx("TenderEditLot", "Tender/{tenderGuid}/Edit/Lot/{lotId}", "Tender", "EditLot");
   //         routes.MapRouteEx("TenderAddDocument", "Tender/{tenderGuid}/Add/Document/{relatedTo}/{relatedId}", "Tender", "AddDocument");
   //         routes.MapRouteEx("TenderEditDocument", "Tender/{tenderGuid}/Edit/Document/{documentId}", "Tender", "EditDocument");
   //         routes.MapRouteEx("TenderDeleteDocument", "Tender/{tenderGuid}/Delete/Document/{documentId}", "Tender", "DeleteDocument");
   //         routes.MapRouteEx("TenderCancel", "Tender/{tenderGuid}/Cancel", "Tender", "Cancel");
   //         routes.MapRouteEx("TenderAddQuestion", "Tender/{tenderGuid}/Add/Question", "Tender", "AddQuestion");

   //         routes.MapRouteEx("TenderAddAnswer", "Tender/{tenderGuid}/Add/Answer/{questionId}", "Tender", "AddAnswer");
   //         routes.MapRouteEx("TenderAjaxGetQuestionTargetNames", "ajax/Tender/GetQuestionTargetNames", "Tender", "GetQuestionTargetNames");
   //         routes.MapRouteEx("TenderAddFeature", "Tender/{tenderGuid}/Add/Feature", "Tender", "AddFeature");
   //         routes.MapRouteEx("TenderEditFeature", "Tender/{tenderGuid}/Edit/Feature/{featureStringId}", "Tender", "EditFeature");
   //         routes.MapRouteEx("TenderAddFeatureValue", "Tender/{tenderGuid}/Feature/{featureStringId}/Add/FeatureValue", "Tender", "AddFeatureValue");
   //         routes.MapRouteEx("TenderEditFeatureValue", "Tender/{tenderGuid}/Feature/{featureStringId}/Edit/FeatureValue/{featureValueId}", "Tender", "EditFeatureValue");
   //         routes.MapRouteEx("TenderAjaxGetFeatureTargetNames", "ajax/Tender/GetFeatureTargetNames", "Tender", "GetFeatureTargetNames");

   //         routes.MapRouteEx("TenderAjaxGetCPV", "ajax/Tender/GetCPV", "Tender", "GetCPV");
   //         routes.MapRouteEx("TenderAjaxGetGSIN", "ajax/Tender/GetGSIN", "Tender", "GetGSIN");

   //         //Complaint
   //         routes.MapRouteEx("Complaints", "Tender/{tenderGuid}/Complaints", "Complaint", "Complaints");
   //         routes.MapRouteEx("AddComplaint", "Tender/{tenderGuid}/Add/Complaint", "Complaint", "Add");
   //         routes.MapRouteEx("EditComplaintAddAnswer", "Tender/{tenderGuid}/Edit/Complaint/{complaintId}/AddAnswer", "Complaint", "AddAnswer");
   //         routes.MapRouteEx("EditComplaint", "Tender/{tenderGuid}/Edit/Complaint/{complaintId}", "Complaint", "Edit");


   //         //routes.MapRouteEx("DraftTenderTenderInfo", "Draft/Tender/{tenderGuid}/Info", nameof(DraftTenderController), nameof(DraftTenderController.TenderInfo));
   //         //routes.MapRouteEx("DraftTenderAddTender", "Draft/Tender/Add", "DraftTender", "AddTender");
   //         //routes.MapRouteEx("DraftTenderEditTender", "Draft/Tender/{tenderGuid}/Edit", "DraftTender", "EditTender");
   //         //routes.MapRouteEx("DraftTenderAddItem", "Draft/Tender/{tenderGuid}/Add/Item", "DraftTender", "AddItem");
   //         //routes.MapRouteEx("DraftTenderEditItem", "Draft/Tender/{tenderGuid}/Edit/Item/{itemId}", "DraftTender", "EditItem");
   //         //routes.MapRouteEx("DraftTenderAddLot", "Draft/Tender/{tenderGuid}/Add/Lot", "DraftTender", "AddLot");
   //         //routes.MapRouteEx("DraftTenderEditLot", "Draft/Tender/{tenderGuid}/Edit/Lot/{lotId}", "DraftTender", "EditLot");
   //         //routes.MapRouteEx("DraftTenderAddDocument", "Draft/Tender/{tenderGuid}/Add/Document/{relatedTo}/{relatedId}", "DraftTender", "AddDocument");
   //         //routes.MapRouteEx("DraftTenderEditDocument", "Draft/Tender/{tenderGuid}/Edit/Document/{documentId}", "DraftTender", "EditDocument");
   //         //routes.MapRouteEx("DraftTenderDeleteDocument", "Draft/Tender/{tenderGuid}/Delete/Document/{documentId}", "DraftTender", "DeleteDocument");
   //         //routes.MapRouteEx("DraftTenderItems", "Draft/Tender/{tenderGuid}/Items", "DraftTender", "Items");
   //         //routes.MapRouteEx("DraftTenderAddItemTemplate", "Draft/AddItemTemplate", "DraftTender", "AddItemTemplate");
   //         //routes.MapRouteEx("DraftTenderTotalInformation", "Draft/Tender/{tenderGuid}/Create/TotalInformation", "DraftTender", "TotalInformation");
   //         //routes.MapRouteEx("DraftTenderTenderStatus", "Draft/Tender/{tenderGuid}/TenderStatus", "DraftTender", "TenderStatus");
   //         //routes.MapRouteEx("DraftTenderAddFeature", "Draft/Tender/{tenderGuid}/Add/Feature", "DraftTender", "AddFeature");
   //         //routes.MapRouteEx("DraftTenderEditFeature", "Draft/Tender/{tenderGuid}/Edit/Feature/{featureStringId}", "DraftTender", "EditFeature");
   //         //routes.MapRouteEx("DraftTenderAddFeatureValue", "Draft/Tender/{tenderGuid}/Feature/{featureStringId}/Add/FeatureValue", "DraftTender", "AddFeatureValue");
   //         //routes.MapRouteEx("DraftTenderEditFeatureValue", "Draft/Tender/{tenderGuid}/Feature/{featureStringId}/Edit/FeatureValue/{featureValueId}", "DraftTender", "EditFeatureValue");
   //         //routes.MapRouteEx("DraftTenderAjaxGetFeatureTargetNames", "ajax/Draft/GetFeatureTargetNames", "DraftTender", "GetFeatureTargetNames");

   //         //routes.MapRouteEx("DraftTenderDeleteTender", "Draft/Tender/{tenderGuid}/Delete/Tender", "DraftTender", "DeleteTender");
   //         //routes.MapRouteEx("DraftTenderDeleteItem", "Draft/Tender/{tenderGuid}/Delete/Item/{itemId}", "DraftTender", "DeleteItem");
   //         //routes.MapRouteEx("DraftTenderDeleteLot", "Draft/Tender/{tenderGuid}/Delete/Lot/{lotId}", "DraftTender", "DeleteLot");
   //         //routes.MapRouteEx("DraftTenderDeleteFeature", "Draft/Tender/{tenderGuid}/Delete/Feature/{featureStringId}", "DraftTender", "DeleteFeature");
   //         //routes.MapRouteEx("DraftTenderDeleteFeatureValue", "Draft/Tender/{tenderGuid}/Feature/{featureStringId}/Delete/FeatureValue/{featureValueId}", "DraftTender", "DeleteFeatureValue");

   //         //routes.MapRouteEx("DraftTenderAjaxGetCPV", "ajax/Draft/GetCPV", "DraftTender", "GetCPV");
   //         //routes.MapRouteEx("DraftTenderAjaxGetGSIN", "ajax/Draft/GetGSIN", "DraftTender", "GetGSIN");



   //         routes.MapRouteEx("DraftPlanAddPlan", "DraftPlan/Add/Plan", "DraftPlan", "AddPlan");
   //         routes.MapRouteEx("DraftPlanEditPlan", "DraftPlan/{planGuid}/Edit/Plan", "DraftPlan", "EditPlan");
   //         routes.MapRouteEx("DraftPlanAddPlanItem", "DraftPlan/{planGuid}/Add/Item", "DraftPlan", "AddItem");
   //         routes.MapRouteEx("DraftPlanEditPlanItem", "DraftPlan/{planGuid}/Edit/Item/{itemId}", "DraftPlan", "EditItem");
   //         routes.MapRouteEx("DraftPlanItems", "DraftPlan/{planGuid}/Items", "DraftPlan", "Items");


   //         routes.MapRouteEx("ProfileIndex", "Profile", "Profile", "Index");
   //         routes.MapRouteEx("ProfilePersonal", "Profile/Personal", "Profile", "Personal");
   //         routes.MapRouteEx("ProfilePersonalEdit", "Profile/Personal/Edit", "Profile", "Edit");
   //         routes.MapRouteEx("ProfilePersonalChangePassword", "Profile/Personal/ChangePassword", "Profile", "ChangePassword");
   //         routes.MapRouteEx("ProfileMessages", "Profile/Messages", "Profile", "Messages");
   //         routes.MapRouteEx("ProfileMyTenders", "Profile/MyTenders", "Profile", "MyTenders");
   //         routes.MapRouteEx("ProfileMyTenderDrafts", "Profile/MyTenderDrafts", "Profile", "MyTenderDrafts");
   //         routes.MapRouteEx("ProfileMyPlanDrafts", "Profile/MyPlanDrafts", "Profile", "MyPlanDrafts");
   //         routes.MapRouteEx("ProfileMemberTenders", "Profile/MemberTenders", "Profile", "MemberTenders");
			
			//routes.MapRouteEx("RoleIndex", "Role", "Role", "Index");
   //         routes.MapRouteEx("RoleAdd", "Role/AddRole", "Role", "AddRole");
   //         routes.MapRouteEx("RoleEdit", "Role/EditRole", "Role", "EditRole");

   //         routes.MapRouteEx("AdminIndex", "Admin", "Admin", "Index");


   //         // TODO: create error controller
   //         routes.MapRouteEx("CatchAll", "{*url}", new { controller = "Home", action = "PageNotFound" });

            //routes.MapRoute(
            //    name: "Tender",
            //    url: "Tender/{action}/{guid}",
            //     defaults: new { controller = "Tender", action = "Index", guid = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}