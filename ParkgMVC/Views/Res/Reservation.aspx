<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ParkgMVC.Models.reservation>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Reservation
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script src="/Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <h1 style="color:#FF0000"><%: ViewData["AnswerFromReservation"] %></h1>
    <h2>Текущий тариф на бронирование</h2>
    
    <table>
        <tr>
            <th>
                Номер тарифа
            </th>
            <th>
                Первое бесплатное время(мин)
            </th>
            <th>
                Цена(р) за час свыше
                бесплатного времени
            </th>
            <th>
                Статус тарифа.
            </th>
            <th>
                Время действия с
                момента подключения (ч)
            </th>
        </tr>

    <% foreach (var item in (IEnumerable<ParkgMVC.Models.reservation_tariff>)ViewData["ReservationTariff"])
       { %>
    
        <tr>
            <td>
                <%: item.id_Reservation_Tariff %>
            </td>
            <td>
                <%: item.FirstFreeTimeInMinutes %>
            </td>
            <td>
                <%: item.PriceInRubForHourHightFreeTime %>
            </td>
            <td>
                <%: item.Status%>
            </td>
            <td>
                <%: item.ValidityPeriodFromTheTimeOfActivationInHour%>
            </td>
        </tr>
    
    <% } %>

    </table>

    <h2>Ваша активная запись бронирования</h2>
                <% int count = Model.Count(); %>
                <% if (count != 0)
                   { %>
    <table>
          		<tr>
			<th rowspan="3"></th>
			<th rowspan="3"></th>
			<th rowspan="3"></th>
			<th rowspan="3" style="text-align: center;">Номер тарифа</th>
			<th rowspan="3" style="text-align: center;">Дата/Время создания брони</th>

                       <th colspan="4" style="text-align: center;">Расположение</td>
                       <th rowspan="3" style="text-align: center;">Альт.место</th>
        

			<th rowspan="3" style="text-align: center;">Предположительная дата выхода из активности</th>
			<th rowspan="3" style="text-align: center;">Итоговая дата выхода из активности</th>
			<th rowspan="3" style="text-align: center;">Статус брони</th>
            			<th rowspan="3" style="text-align: center;">Описание</th>
		</tr>
		<tr>
			<th rowspan="2" style="text-align: center;">Зона</th>
			<th colspan="2" style="text-align: center;">Уровень</th>
			<th rowspan="2" style="text-align: center;">Место</th>
		</tr>
		<tr>
			<th style="text-align: center;">Номер</th>
			<th style="text-align: center;">Тип</th>
		</tr>
		<tr>

        

    <% foreach (var item in Model)
       { %>
    
        <tr>
               
                <td> <% using (Html.BeginForm("Reservation", "Res", FormMethod.Post))
                        {%>  <input type="hidden" name="id_reservation_user" id="Hidden3" value="<%: item.id_reservation_user%>" /> <% if (item.Status == "Formed")
                                                                                                                                       { %><input type="submit" name="Reservation" id="Connect" value="Connect" /> <% } %><% } %>
                </td>

                <td> <% using (Html.BeginForm("Agreement", "Res", FormMethod.Post))
                        {%>  <input type="hidden" name="id_Reservation_Tariff" id="Hidden2" value="<%: item.id_Reservation_Tariff %>" /> <% if (item.Status == "Formed")
                                                                                                                                            { %><input type="submit" name="Agreement" id="Continue_Formed" value="Continue_Formed" />  <% } %><% } %>
                </td>

                <td>    
              <% using (Html.BeginForm("Reservation", "Res", FormMethod.Post))
                 {%>  <input type="hidden" name="id_reservation_user" id="Hidden1" value="<%: item.id_reservation_user%>" />  <input type="submit" name="Reservation" id="Revoke" value="Revoke" />    <% } %>
               </td>
            <td>
                <%: item.id_Reservation_Tariff%>
            </td>
            <td>
                <%: item.DateConnection%>
            </td>
            
                <td><% if (item.id_location_place != null)
                       { %><%: item.place.levelzone.Parking_zone%><% } %></td>

               <td><% if (item.id_location_place != null)
                      { %> <%: item.place.levelzone.Level%><% } %></td>
               <td><% if (item.id_location_place != null)
                      { %> <%: item.place.levelzone.TypeLevel%><% } %></td>
               <td><% if (item.id_location_place != null)
                      { %> <%: item.place.NumberPlace%> <% } %></td>
                <% if (item.Status != "Formed")
                   { %>
                                         <td>   <a href="javascript:void(0)" id="toglink0">показать альтернативное место</a>
<div class="togblock" id="togblock0">
                <%  
                       ParkgMVC.Models.MyParkingEntities mp = new ParkgMVC.Models.MyParkingEntities();
                       if (item.id_alternative_location_place != null)
                       {
                           var alt = mp.place.Where(x => x.id_location_place == item.id_alternative_location_place).FirstOrDefault();
                           if (alt.id_location_place != item.id_location_place)
                           {
                 %><table>
		<tr>
			<th rowspan="2" style="text-align: center;">Зона</th>
			<th colspan="2" style="text-align: center;">Уровень</th>
			<th rowspan="2" style="text-align: center;">Место</th>
		</tr>
		<tr>
			<th style="text-align: center;">Номер</th>
			<th style="text-align: center;">Тип</th>
		</tr>
		
                               <tr> <td><%: alt.levelzone.Parking_zone%></td>
               <td><%: alt.levelzone.Level%></td>
               <td><%: alt.levelzone.TypeLevel%></td>
               <td><%: alt.NumberPlace%></td></tr></table>
               <% }
                           else if (alt.id_location_place == item.id_location_place)
                           { %>
                       <h5>Ваше место активно и альтернативного не требуется.</h5>
               <% }
                       }
                       else if (item.id_alternative_location_place == null)
                       { %>
                   <h5>Альтернативного места не найдено.</h5>
               <% } %>
</div></td> <% } else { %>
<td></td>
<%} %>
            <td >
                <%: item.ApproximatelyDateOutFromActivity%>
            </td>
                        <td>
                <%: item.DateOutFromActivity%>
            </td>
            <td>
                <%: item.Status%>
            </td>
             <td>
                <%: item.Description%>
            </td>
        </tr>
    
    <% } %>

    </table>
       <% }
                   else
                   { %>
                       
                           <p>
    У вас нет активной заявки на бронирование, но Вы можете создать ее щелкнув
        <%: Html.ActionLink(" сюда", "Agreement")%>
    </p>
     <% } %>

       <style type="text/css">
 
 .togblock {
 display:none;
 text-align:inherit
 }
 
           #Connect
           {
               width: 60px;
           }
           #Continue_Formed
           {
               width: 120px;
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
    if ($('#toglink0').text() == 'скрыть альтернативное место') {
        $('#toglink0').text('показать альтернативное место');


    } else {
        $('#toglink0').text('скрыть альтернативное место');
    }
});
    });
 </script>


</asp:Content>

