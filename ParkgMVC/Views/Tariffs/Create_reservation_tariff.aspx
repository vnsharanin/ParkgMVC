<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ParkgMVC.Models.reservation_tariff>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create_reservation_tariff
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create_reservation_tariff</h2>
                <h1 style="color:#FF0000"><%: ViewData["ReservationTariff"] %></h1>
    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>

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
                <%: Html.TextBoxFor(model => model.PriceInRubForHourHightFreeTime) %>
                <%: Html.ValidationMessageFor(model => model.PriceInRubForHourHightFreeTime) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.ValidityPeriodFromTheTimeOfActivationInHour) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.ValidityPeriodFromTheTimeOfActivationInHour) %>
                <%: Html.ValidationMessageFor(model => model.ValidityPeriodFromTheTimeOfActivationInHour) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Status) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Status) %>
                <%: Html.ValidationMessageFor(model => model.Status) %>
            </div>
            
            <p>
   <input type="submit" name="Create_reservation_tariff" id="Save_new_reservation_tariff" value="Save_new_reservation_tariff" />
          
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

