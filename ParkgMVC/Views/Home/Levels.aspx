<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ParkgMVC.Models.levelzone>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	LAZLP
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1><%: ViewData["Reservation"] %></h1>
<fieldset>
    <h2>Levels</h2>
        <%: ViewData["Zone"] %>
    <table>
        <tr>
            <th></th>
                        <th></th>
                                    <th></th>
            <th>
                Level
            </th>
            <th>
                Type Level
            </th>
            <th>
                Количество мест
            </th>
        </tr>

    <% foreach (var item in Model) { %>
                <% using (Html.BeginForm("Places", "Home", FormMethod.Post)){%>
            <input type="hidden" name="Value" id="Hidden1" value="<%: ViewData["Reservation"] %>" />
            <input type="hidden" name="zone" id="Hidden2" value="<%: item.Parking_zone %>" />
            <input type="hidden" name="level" id="Hidden3" value="<%: item.Level %>" />
            <input type="hidden" name="type_level" id="Hidden5" value="<%: item.TypeLevel %>" />
            <input type="hidden" name="id_location_level" id="Hidden4" value="<%: item.id_location_level %>" />
        <tr>
            <td>
                <%: Html.ActionLink("Edit", "EditAmountPlace", new { id_location_level = item.id_location_level })%>
                </td>
                <td>

                                <input type="submit" name="Places" id="Select_level" value="Select_level" />
               </td>
               <td>
                <%: Html.ActionLink("Delete", "Delete", new { id_location_level = item.id_location_level })%>
            </td>
            <td>
                <%: item.Level %>
            </td>
            <td>
                <%: item.TypeLevel%>
            </td>
             <td>
                
            </td>
        </tr>
        <% } %>
    <% } %>

    </table>
    </fieldset>
    <p>
        <%: Html.ActionLink("Create New", "Create") %>
    </p>
        <p>
        <%: Html.ActionLink("Back to ZonesLevelsPlaces", "ZonesLevelsPlaces")%>
    </p>

            <div>
        <%: Html.ActionLink("Back to Index", "Index")%>
    </div>

</asp:Content>

