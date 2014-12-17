<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ParkgMVC.Models.parkingzone>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ZonesLevelsPlaces
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script src="/Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
<h1 style="color:#FF0000"><%: ViewData["EditZone1"]%></h1>
<h1 style="color:#FF0000"><%: ViewData["EditZone2"]%></h1>
        <h1 style="color:#FF0000"><%: ViewData["ReservationPlace"] %></h1>
    <h1><%: ViewData["Reservation"] %></h1>
                      <% if (User.IsInRole("Admin"))
                   { %>  <fieldset>  <h1 style="color:#FF0000"><%: ViewData["Apply"] %></h1>
                   <a href="javascript:void(0)" id="toglink0">...раскрыть управление зонами парковки</a>
    <div class="togblock" id="togblock0">

     <% using (Html.BeginForm("ZonesLevelsPlaces", "Home", FormMethod.Post))
        { %>
                      <input type="radio"   id="AmZone" name="AmZone" value="NewAmountZones" onchange="check()">Задать новое количество зон: </> 
          <input type="text" id="NewAmountZones" disabled="true" name="NewAmountZones" value="" " />

           <br />

                      <input type="radio" id="AdZone" name="AmZone" value="AddZones"  onchange="check()">Добавить количество зон: </>
           <input type="text" id="AddZones" disabled="true" name="AddZones" value="" />
  <br />

    Тип добавляемых(закрываемых) зон:<%= Html.DropDownList("Type_zone")%>
  <br />              Адрес: <input type="text" id="Address" disabled="true" name="Address" value="" />
                <br />

                  <input type="submit" name="ZonesLevelsPlaces" id="Apply" value="Apply" /> 
                  </div>
                 <script>
                     function check() {
                         var rarr = document.getElementsByName("AmZone");
                         if (rarr[1].checked) {
                             document.getElementById("NewAmountZones").disabled = true;
                             document.getElementById("NewAmountZones").value = "";
                             document.getElementById("AddZones").disabled = false;
                             document.getElementById("Type_zone").disabled = false;
                             document.getElementById("Address").disabled = false;
                         }
                         else {
                             document.getElementById("AddZones").disabled = true;
                             document.getElementById("AddZones").value = "";
                             document.getElementById("NewAmountZones").disabled = false;
                             document.getElementById("Type_zone").disabled = false;
                             document.getElementById("Address").disabled = false;
                         }
                     }
</script>
<style type="text/css">
 
 .togblock {
 display:none;
 text-align:inherit
 }
 
 </style>
<script type="text/javascript">
    $(document).ready(function () {
        $('#toglink0').click(
function () {
    if (jQuery.browser.msie && parseInt(jQuery.browser.version) == 6) {
        if ($('#togblock1').css("display") == "block") {
            $('#togblock0').css("display", "none");
        } else {
            $('#togblock0').css("display", "block");
        }
    } else {
        $('#togblock0').toggle("slow");
    }
    if ($('#toglink0').text() == 'скрыть управление зонами парковки') {
        $('#toglink0').text('...раскрыть управление зонами парковки');
        document.getElementById("Type_zone").disabled = true;
        document.getElementById("Address").disabled = true;
        document.getElementById("AddZones").disabled = true;
        document.getElementById("NewAmountZones").disabled = true;
        document.getElementById("AddZones").value = "";
        document.getElementById("NewAmountZones").value = "";
        var rarr = document.getElementsByName("AmZone");
        if (rarr[1].checked) {
            rarr[1].checked = false;
        }
        else {
            rarr[0].checked = false;
        }


    } else {
        $('#toglink0').text('скрыть управление зонами парковки');
    }
});
    });
 </script>



<% } %>

    </fieldset><%} %>
    <fieldset>
    <h2>Парковочные зоны</h2>
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
                Номер парковочной зоны
            </th>
            <th>
                Тип парковочный зоны
            </th>
            <th>
                Адрес
            </th>
        </tr>

                 <% ParkgMVC.Models.MyParkingEntities mp = new ParkgMVC.Models.MyParkingEntities();%>

    <% foreach (var item in Model) { %>

                    <%  int p = mp.place.Count(x => x.levelzone.Parking_zone == item.Parking_zone & x.Status != "Was replaced" & x.Status != "Disabled");
                        int disabled = mp.place.Count(x => x.levelzone.Parking_zone == item.Parking_zone & x.Status == "Was replaced" & x.Status == "Disabled");

                        int freep = mp.place.Count(x => x.levelzone.Parking_zone == item.Parking_zone & x.Status == "Free");
                        int notw = mp.place.Count(x => x.levelzone.Parking_zone == item.Parking_zone & x.Status == "Not working");
                        int occup = mp.place.Count(x => x.levelzone.Parking_zone == item.Parking_zone & x.Status == "Occupied");
                        int inwait = mp.place.Count(x => x.levelzone.Parking_zone == item.Parking_zone & x.Status == "In waiting visit");         
                    %>
            <% if (disabled == p & !User.IsInRole("Admin")) { }
               else
               { %>


        <tr>
        <% if (User.IsInRole("Admin")) { %>

            <td>
                    <% using (Html.BeginForm("Edit_zone", "Home", FormMethod.Post))
           {%>
               <input type="hidden" name="Parking_zone" id="Hidden3" value="<%: item.Parking_zone %>" />
                <input type="submit" name="Edit_zone" id="Edit_zone" value="Edit_zone" /> <% } %>
                </td>

                                    <% using (Html.BeginForm("ZonesLevelsPlaces", "Home", FormMethod.Post))
           {%>            <input type="hidden" name="Parking_zone" id="Parking_zone" value="<%: item.Parking_zone %>" />
                                <td>          <% if (disabled != p) 
             {%>    
                <input type="submit" name="ZonesLevelsPlaces" id="Disabled_zone" value="Disabled_zone" /> <% } %>
            </td>

            <td>
            <% if (disabled != p & (occup + notw) != p)
             {%>
                            <input type="submit" name=ZonesLevelsPlaces id="Temporarily_not_working_zone" value="Temporarily_not_working_zone" />
                                   <% }
             if (disabled != p & (freep+occup+inwait) != p)
             { %> 
                          
                            <input type="submit" name="ZonesLevelsPlaces" id="Run_this_zone" value="Run_this_zone" /> <% } %>
            </td>
            <% } %>
           
            <% } %>

                        <% using (Html.BeginForm("Levels", "Home", FormMethod.Post)){%>
        <input type="hidden" name="Value" id="Hidden1" value="<%: ViewData["Reservation"] %>" />
    <input type="hidden" name="Parking_zone" id="Hidden2" value="<%: item.Parking_zone %>" />
                <td>
                <input type="submit" name="Levels" id="Select_zone" value="Select_zone" />
                </td>

            <td>
                <%: item.Parking_zone %>
            </td>
                        <td>
                <%: item.type_parking.Name %>
            </td>
            <td>
                <%: item.Address %>
            </td>
        </tr>
        <% } %>
    <% } %>
    <% } %>
    </table>
    </fieldset>
    <p>
        <%: Html.ActionLink("Create New", "Create") %>

        <%: Html.ActionLink("Back to Index", "Index") %>
    </p>
</asp:Content>

