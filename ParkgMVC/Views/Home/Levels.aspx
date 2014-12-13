<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ParkgMVC.Models.levelzone>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	LAZLP
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h1 style="color:#FF0000"><%: ViewData["EditLevel"]%></h1>
    <h1><%: ViewData["Reservation"] %></h1>
<fieldset>
    <h2>Levels</h2>
        <%: ViewData["Zone"] %>
    <table>
        <tr>
            <% if (User.IsInRole("Admin"))
            { %>
            <th></th>
            <th></th>
                <% } %>
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
                        <th>
                Количество свободных мест
            </th>
        </tr>
         <% ParkgMVC.Models.MyParkingEntities mp = new ParkgMVC.Models.MyParkingEntities();%>
    <% foreach (var item in Model) { %>
                <%  int p = mp.place.Count(x => x.id_location_level == item.id_location_level & x.Status != "Was replaced" & x.Status != "Disabled");
                    int freep = mp.place.Count(x => x.id_location_level == item.id_location_level & x.Status == "Free");
                    int disabled = mp.place.Count(x => x.id_location_level == item.id_location_level & x.Status == "Was replaced" & x.Status == "Disabled");           
                    ViewData["AmPl"] = p; ViewData["AmFreePl"] = freep;%>

                    <!-- возможно & p != 0 из условия будет лучше убрать. Подумать над этим.-->

            <% if (disabled == p & !User.IsInRole("Admin")) { }
               else
               { %>
        <tr>
                    <% if (User.IsInRole("Admin"))
                       { %>
                <td>
                 <% using (Html.BeginForm("Edit_a_p", "Home", FormMethod.Post))
                    {%>
                    <input type="hidden" name="id_location_level" id="Hidden6" value="<%: item.id_location_level %>" />
                <input type="submit" name="Edit_a_p" id="Edit_amount_place" value="Edit_amount_place" />
                  <% } %>

                </td> 
                 <% } %>

                                <% using (Html.BeginForm("Places", "Home", FormMethod.Post))
                                   {%>
            <input type="hidden" name="Value" id="Hidden1" value="<%: ViewData["Reservation"] %>" />
            <input type="hidden" name="zone" id="Hidden2" value="<%: item.Parking_zone %>" />
            <input type="hidden" name="level" id="Hidden3" value="<%: item.Level %>" />
            <input type="hidden" name="type_level" id="Hidden5" value="<%: item.TypeLevel %>" />
            <input type="hidden" name="id_location_level" id="Hidden4" value="<%: item.id_location_level %>" />

                                                 <% if (User.IsInRole("Admin"))
                                                    { %>
                <td>
                <%: Html.ActionLink("Delete", "Delete", new { id_location_level = item.id_location_level })%>
                 </td>
                  <% } %>
               
                
            <td><% if (disabled != p) {%>
                                <input type="submit" name="Places" id="Select_level" value="Select_level" /> <% } %>
            </td>
                      <% } %>
            <td>
                <%: item.Level%>
            </td>
            <td>
                <%: item.TypeLevel%>
            </td>
                                 
                       
             <td> 
                <%: ViewData["AmPl"]%>
            </td>
                         <td> 
                <%: ViewData["AmFreePl"]%>
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

