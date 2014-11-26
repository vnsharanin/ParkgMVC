<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ParkgMVC.Models.places>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EditAmountPlace
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>EditAmountPlace</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Fields</legend>
                       
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Place) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Place) %>
                <%: Html.ValidationMessageFor(model => model.Place) %>
            </div>
            
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>
        <div>
        <%: Html.ActionLink("Back to LAZLP", "LAZLP") %>
    </div>
    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

