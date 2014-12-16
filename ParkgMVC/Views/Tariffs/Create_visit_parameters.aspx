<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ParkgMVC.Models.visitparameters>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create_visit_parameters
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
                <h1 style="color:#FF0000"><%: ViewData["VisitParameters"] %></h1>
    <h2>Create_visit_parameters</h2>

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
                <%: Html.LabelFor(model => model.FirstFreeTimeOnChangeBalans) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.FirstFreeTimeOnChangeBalans) %>
                <%: Html.ValidationMessageFor(model => model.FirstFreeTimeOnChangeBalans) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Status) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Status) %>
                <%: Html.ValidationMessageFor(model => model.Status) %>
            </div>
            
            <p>
   <input type="submit" name="Create_visit_parameters" id="Save_visit_parameters" value="Save_visit_parameters" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

