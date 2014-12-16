<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ParkgMVC.Models.place>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Места
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1><%: ViewData["Reservation"] %></h1>
        <h1 style="color:#FF0000"><%: ViewData["ReservationPlace"] %></h1>
    <fieldset>
<h2>Список активных тарифов на места:</h2>
        <table>
        <tr>
            <th>
                Номер тарифа
            </th>
            <th>
                Поддержка климат-контроля
            </th>
            <th>
                Тип(открытый, крытый, полукрытый)
            </th>
            <th>
                Цена за час без абонемента
            </th>
            <th>
                Цена за час с абонементом
            </th>
            <th>
                Статус
            </th>
        </tr>

    <% foreach (var item in (IEnumerable<ParkgMVC.Models.tariffonplace>)ViewData["ActiveTariffs"]) {%>
    
        <tr>
            <td>
                <%: item.id_tariff_on_place %>
            </td>
            <td>
                <%: item.SupportClimateControl %>
            </td>
            <td>
                <%: item.Type %>
            </td>
            <td>
                <%: String.Format("{0:F}", item.PriceForHourWithoutAbonement) %>
            </td>
            <td>
                <%: String.Format("{0:F}", item.PriceForHourWithAbonement) %>
            </td>
            <td>
                <%: item.Status %>
            </td>
        </tr>
    
    <% } %>

    </table>
</fieldset>
    <% using (Html.BeginForm("Places", "Home", FormMethod.Post)){%>
        <%: Html.ValidationSummary(true) %>
                <fieldset>

    <h2>Места</h2>

    <% if (ViewData["Reservation"].ToString() == "RESERVATION" & Model.Count() == 0)
       { %>
       <h1> На этом уровне свободных мест нет, Вы можете подождать пока они появятся или посмотреть другие доступные уровни, если таковые имеются.</h1>
    <% } else {%>
        <%: ViewData["Zone-Level"] %>
    <table>
        <tr>
         <% if (User.IsInRole("Admin")) 
            { %>
            <th>Включение/Выключение места</th>
            <th>Смена тарифа</th>
            <% } else if (ViewData["Reservation"].ToString() == "RESERVATION") { %>
            <th></th>
            <% } %>
            <th style="width: 100px">
                № места
            </th>
            <th style="width: 100px">
                Статус
            </th>
            <th style="width: 100px">
                Номер тарифа
            </th>
        </tr>
    
    <% foreach (var item in Model) { %>
    
        <tr>

           
                         
             <% if (User.IsInRole("Admin")) { %> 
                <% using (Html.BeginForm("Places", "Home", FormMethod.Post))
                   {%>
                   <input type="hidden" name="id_location_level" id="id_location_level" value="<%: item.id_location_level %>" />
                <input type="hidden" name="id_loc_pl" id="id_loc_pl" value="<%: item.id_location_place %>" />
                 <td>
                <% if (item.Status == "Free" || item.Status == "In waiting visit")
                   { %>
               <input type="submit" name="Places" id="Stop_work" value="Stop_work" /> <% }
                   else if (item.Status == "Not working")
                   { %>
               <input type="submit" name="Places" id="Start_work" value="Start_work" /> <% } %>
             
               </td><% } %>
               <% using (Html.BeginForm("Place_tariff", "Home", FormMethod.Post))
                   {%><td>
               <% if (item.Status == "Not working" || item.Status == "Free") {  %>
                                  <input type="hidden" name="id_location_level" id="id_location_level" value="<%: item.id_location_level %>" />
                <input type="hidden" name="id_loc_pl" id="Hidden3" value="<%: item.id_location_place %>" />
                 <input type="submit" name="Place_tariff" id="Change_tariff_for_this_place" value="Change_tariff_for_this_place" />
                              <%} %>  </td><% } %>
               <%} %>

                           <% if (ViewData["Reservation"].ToString() == "RESERVATION") { %><td>
             <input type="hidden" name="id_location_level" id="id_location_level" value="<%: item.id_location_level %>" />
             <input type="hidden" name="tariff" id="tariff" value="<%: item.id_tariff_on_place %>" />
                <input type="radio" id="ChoosePlace" name="ChoosePlace" value="<%: item.id_location_place %>" onchange="document.getElementById('SaveReservation').disabled = !this.checked; document.getElementById('ConnectReservation').disabled = !this.checked; document.getElementById('label').innerHTML='';"/>
               
                        </td>   <%   } %>
              
              
            <td style="width: 100px">
                <%: item.NumberPlace %>
            </td>
            <td style="width: 100px">
                <%: item.Status %>
            </td>
            <td style="width: 100px">
                <%: item.id_tariff_on_place %>
            </td>
        </tr>
    
    <% } %>
    </table>
            <p>
             <% if (ViewData["Reservation"].ToString() == "RESERVATION") { %> 
             <br />
            <label id="label">МЕСТОРАСПОЛОЖЕНИЕ НЕ ВЫБРАНО!</label><br>
            <input type="submit" name="Places" id="ConnectReservation" value="ConnectReservation"  />
            <input type="submit" name="Places" id="SaveReservation" value="SaveReservation" />
            </p>
                    <script language="JavaScript">
                        document.getElementById("ConnectReservation").disabled = true;
                        document.getElementById("SaveReservation").disabled = true;
    </script>
       <% } %>
    <% } %>
    <% } %>
</asp:Content>

