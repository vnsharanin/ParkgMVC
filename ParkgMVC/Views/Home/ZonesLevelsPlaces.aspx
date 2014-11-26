<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ParkgMVC.Models.parkingzone>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ZonesLevelsPlaces
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <h1 style="color:#FF0000"><%: ViewData["ReservationPlace"] %></h1>
    <h1><%: ViewData["Reservation"] %></h1>
    <fieldset>
    <h2>Zones</h2>
    <table>
        <tr>
            <th></th>
                        <th></th>
                                    <th></th>
            <th>
                Parking_zone
            </th>
            <th>
                Address
            </th>
        </tr>


    <% foreach (var item in Model) { %>
            <% using (Html.BeginForm("Levels", "Home", FormMethod.Post)){%>
        <input type="hidden" name="Value" id="Hidden1" value="<%: ViewData["Reservation"] %>" />
    <input type="hidden" name="Parking_zone" id="Hidden2" value="<%: item.Parking_zone %>" />
        <tr>
            <td>
                <%: Html.ActionLink("EL", "EL", new { Parking_zone = item.Parking_zone, Address = item.Address })%>
                </td>
                <td>
                <input type="submit" name="Levels" id="Select_zone" value="Select_zone" />
                </td>
                <td>
                <%: Html.ActionLink("Delete", "Delete", new { id=item.Parking_zone })%>
            </td>
            <td>
                <%: item.Parking_zone %>
            </td>
            <td>
                <%: item.Address %>
            </td>
        </tr>
    
    <% } %>
    <% } %>
    </table>
    </fieldset>
    <p>
        <%: Html.ActionLink("Create New", "Create") %>

        <%: Html.ActionLink("Back to Index", "Index") %>
    </p>
</asp:Content>

