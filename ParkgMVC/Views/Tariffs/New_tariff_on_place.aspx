<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ParkgMVC.Models.tariffonplace>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	New_tariff_on_place
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>New_tariff_on_place</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>

            <div class="editor-label">
                <%: Html.LabelFor(model => model.SupportClimateControl) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.SupportClimateControl) %>
                <%: Html.ValidationMessageFor(model => model.SupportClimateControl) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Type) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Type) %>
                <%: Html.ValidationMessageFor(model => model.Type) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.PriceForHourWithoutAbonement) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.PriceForHourWithoutAbonement) %>
                <%: Html.ValidationMessageFor(model => model.PriceForHourWithoutAbonement) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.PriceForHourWithAbonement) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.PriceForHourWithAbonement) %>
                <%: Html.ValidationMessageFor(model => model.PriceForHourWithAbonement) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Status) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Status) %>
                <%: Html.ValidationMessageFor(model => model.Status) %>
            </div>
            
            <p>
   <input type="submit" name="New_tariff_on_place" id="Submit2" value="Save_new_tariff_on_place" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

