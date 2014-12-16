<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ParkgMVC.Models.tariffonabonementforvisit>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create_abonement
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create_abonement</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Name_tariff_on_abonement) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Name_tariff_on_abonement) %>
                <%: Html.ValidationMessageFor(model => model.Name_tariff_on_abonement) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Num_days) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Num_days) %>
                <%: Html.ValidationMessageFor(model => model.Num_days) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Max_Num_visits_in_this_tariff) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Max_Num_visits_in_this_tariff) %>
                <%: Html.ValidationMessageFor(model => model.Max_Num_visits_in_this_tariff) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Price) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Price) %>
                <%: Html.ValidationMessageFor(model => model.Price) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Status) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Status) %>
                <%: Html.ValidationMessageFor(model => model.Status) %>
            </div>
            
            <p>
   <input type="submit" name="Create_abonement" id="Save_abonement" value="Save_abonement" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

