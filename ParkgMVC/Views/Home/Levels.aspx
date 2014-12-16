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
                                <% if (!User.IsInRole("Admin"))
                                   { %>
                        <th>
                Количество свободных мест
            </th>
            <% } else { %>
                                    <th>
                Количество ТС, простаивающих на уровне.
            </th>
            <% } %>

        </tr>
         <% ParkgMVC.Models.MyParkingEntities mp = new ParkgMVC.Models.MyParkingEntities();%>
    <% foreach (var item in Model) { %>
                <%  int p = mp.place.Count(x => x.id_location_level == item.id_location_level & x.Status != "Was replaced" & x.Status != "Disabled");
                    int freep = mp.place.Count(x => x.id_location_level == item.id_location_level & x.Status == "Free");
                    int notw = mp.place.Count(x => x.id_location_level == item.id_location_level & x.Status == "Not working");
                    int occup = mp.place.Count(x => x.id_location_level == item.id_location_level & x.Status == "Occupied");
                    int inwait = mp.place.Count(x => x.id_location_level == item.id_location_level & x.Status == "In waiting visit");
                    int disabled = mp.place.Count(x => x.id_location_level == item.id_location_level & x.Status == "Was replaced" & x.Status == "Disabled");           
                    %>

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
                                                 <% using (Html.BeginForm("Levels", "Home", FormMethod.Post))
                                                    {%>
                                                 <% if (User.IsInRole("Admin"))
                                                    { %>
            <input type="hidden" name="id_location_level" id="Hidden7" value="<%: item.id_location_level %>" />
            <input type="hidden" name="Parking_zone" id="Hidden8" value="<%: item.Parking_zone %>" />
                <td>
          <% if (disabled != p) 
             {%>     
             <input type="submit" name="Levels" id="Submit1" value="Disabled_level" /> <% } %>
               </td>
               

        <td>  <% if (disabled != p & (occup + notw) != p)
             {%>
           <input type="submit" name="Levels" id="Submit2" value="Temporarily_not_working" />
          <% }
             if (disabled != p & (freep+occup+inwait) != p)
             { %>
              <input type="submit" name="Levels" id="Submit3" value="Run_this_level" />
          <% } %>
          
                 </td>
                  <% } %>
               
               <% } %>

                                <% using (Html.BeginForm("Places", "Home", FormMethod.Post))
                                   {%>
            <input type="hidden" name="Value" id="Hidden1" value="<%: ViewData["Reservation"] %>" />
            <input type="hidden" name="zone" id="Hidden2" value="<%: item.Parking_zone %>" />
            <input type="hidden" name="level" id="Hidden3" value="<%: item.Level %>" />
            <input type="hidden" name="type_level" id="Hidden5" value="<%: item.TypeLevel %>" />
            <input type="hidden" name="id_location_level" id="Hidden4" value="<%: item.id_location_level %>" />

                
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
                <%: p%>
               </td><td> 
                                                <% if (!User.IsInRole("Admin"))
                                                   { %>
                         
                <%: freep%>
           
            <% }
                                                   else
                                                   { %>
                                                                          
                <%: occup%>
           
            <% } %> </td>
        </tr>
        <% } %>
    <% } %>

    </table>
    </fieldset>
</asp:Content>

