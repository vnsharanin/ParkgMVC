<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ParkgMVC.Models.visit>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	TS
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>TS</h2>


    <table>
        <tr>
            <th></th>
            <th>
                Number
            </th>
            <th>
                Parking_zone
            </th>
            <th>
                Level
            </th>
            <th>
                Type Level
            </th>
            <th>
                Place
            </th>
            <th>
                DateIn
            </th>
            <th>
                DateOut
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
               <%: Html.ActionLink("Delete", "RegisterOUT", new { Number = item.id_ts, Parking_zone = item.places.levelszone.Parking_zone, Level = item.places.levelszone.Level, Place = item.places.Place, DateIn = item.DateIn, DateOut = item.DateOut })%>
            </td>
<td> <%: item.ts.Number %></td>
<td> <%: item.places.levelszone.Parking_zone %></td>
<td> <%: item.places.levelszone.Level %></td>
<td> <%: item.places.levelszone.TypeLevel %></td>
<td> <%: item.places.Place %></td>
<td> <%: item.DateIn %></td>
<td> <%: item.DateOut %></td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%: Html.ActionLink("Create New", "CreateVISIT") %>
    </p>
        <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>
</asp:Content>
