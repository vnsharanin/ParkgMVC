<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ParkgMVC.Models.ts>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	RegisterOut
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <h1 style="color:#FF0000"><%: ViewData["ExceptionRegisterOut"]%></h1>
    <h2>RegisterOut</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>

            <div class="editor-label">
                <%: Html.LabelFor(model => model.Number) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Number) %>
                <%: Html.ValidationMessageFor(model => model.Number) %>
            </div>

            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

            <fieldset>
            <h2>Active visit:</h2>

    <table>
        <tr>
            <th></th>
            <th>
                Num_vis
            </th>
            <th>
                id_ts
            </th>
            <th>
                id_location_place
            </th>
            <th>
                DateIn
            </th>
            <th>
                DateOut
            </th>
            <th>
                FirstAttemptGoOut
            </th>
            <th>
                NextAttemptGoOut
            </th>
            <th>
                id_vis_param
            </th>
            <th>
                Status
            </th>
        </tr>

    <% foreach (var item in (IEnumerable<ParkgMVC.Models.visit>)ViewData["ActiveVisit"]) {%>
    
        <tr>
            <td>
                <%: Html.ActionLink("Register Out", "RegisterOut", new { /* id=item.PrimaryKey */ })%>
            </td>
            <td>
                <%: item.Num_vis %>
            </td>
            <td>
                <%: item.id_ts %>
            </td>
            <td>
                <%: item.id_location_place %>
            </td>
            <td>
                <%: item.DateIn %>
            </td>
            <td>
                <%: item.DateOut %>
            </td>
            <td>
                <%: item.FirstAttemptGoOut %>
            </td>
            <td>
                <%: item.NextAttemptGoOut %>
            </td>
            <td>
                <%: item.id_vis_param %>
            </td>
            <td>
                <%: item.Status %>
            </td>
        </tr>
    
    <% } %>

    </table>


            </fieldset>
    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

