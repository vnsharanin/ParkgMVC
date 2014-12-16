<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ParkgMVC.Models.visitparameters>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit_visit_parameters
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
                <h1 style="color:#FF0000"><%: ViewData["VisitParameters"] %></h1>
    <h2>Edit_visit_parameters</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Fields</legend>
                            <input type="hidden" name="id_vis_param" id="id_vis_param" value="<%: Model.id_vis_param %>" />

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
            
            <p>
               <input type="submit" name="Edit_visit_parameters" id="Change_visit_parameters" value="Change_visit_parameters" />
 
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

