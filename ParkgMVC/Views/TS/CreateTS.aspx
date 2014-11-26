<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ParkgMVC.Models.ts>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
    <h2>Create</h2>
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
<% Html.EnableClientValidation(); %>

<% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Number) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Number)%>
                <%: Html.ValidationMessageFor(model => model.Number)%>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Company) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Company)%>
                <%: Html.ValidationMessageFor(model => model.Company)%>
            </div>

            <div class="editor-label">
                <%: Html.LabelFor(model => model.Mode) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Mode)%>
                <%: Html.ValidationMessageFor(model => model.Mode)%>
            </div>  
            <input type="checkbox" name="multi_note" value="1" onclick="hideOrShowText(this)">
            <INPUT ID="Lo" TYPE="text"
NAME="name" SIZE=20>
                               
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>
            <script language="JavaScript">
                function hideOrShowText(box) {
                    var vis = (box.checked) ? "block" : "none";
                    document.getElementById('Lo').style.display = vis;
                }
    </script>
    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "TS") %>
    </div>

</asp:Content>

