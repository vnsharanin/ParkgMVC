<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ListActiveVisit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<fieldset>
    <h2>Active Visit</h2>

    </script>
    

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
<fieldset>
    <h2>Not Active Visit</h2>

    </script>


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

    <% foreach (var item in (IEnumerable<ParkgMVC.Models.visit>)ViewData["NotActiveVisit"]) {%>
    
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
</asp:Content>

