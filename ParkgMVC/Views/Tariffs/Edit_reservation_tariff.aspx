<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ParkgMVC.Models.reservation_tariff>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit_reservation_tariff
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit_reservation_tariff</h2>
                <h1 style="color:#FF0000"><%: ViewData["ReservationTariff"] %></h1>
    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Fields</legend>
                
                <input type="hidden" name="id_reservation_tariff" id="id_reservation_tariff" value="<%: Model.id_Reservation_Tariff %>" />
      
            <div class="editor-label">
                <%: Html.LabelFor(model => model.FirstFreeTimeInMinutes) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.FirstFreeTimeInMinutes) %>
                <%: Html.ValidationMessageFor(model => model.FirstFreeTimeInMinutes) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.PriceInRubForHourHightFreeTime) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.PriceInRubForHourHightFreeTime, String.Format("{0:F}", Model.PriceInRubForHourHightFreeTime)) %>
                <%: Html.ValidationMessageFor(model => model.PriceInRubForHourHightFreeTime) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.ValidityPeriodFromTheTimeOfActivationInHour) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.ValidityPeriodFromTheTimeOfActivationInHour) %>
                <%: Html.ValidationMessageFor(model => model.ValidityPeriodFromTheTimeOfActivationInHour) %>
            </div>
            
            <p>
                <input type="submit" name="Edit_reservation_tariff" id="Change_reservation_tariff" value="Change_reservation_tariff" />
          
            </p>
        </fieldset>

    <% } %>

</asp:Content>

