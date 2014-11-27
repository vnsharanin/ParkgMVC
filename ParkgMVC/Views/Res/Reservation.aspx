<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ParkgMVC.Models.reservation>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Reservation
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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

    <table>
          		<tr>
			<th rowspan="3"></th>
			<th rowspan="3"></th>
			<th rowspan="3"></th>
			<th rowspan="3" style="text-align: center;">Номер тарифа</th>
			<th rowspan="3" style="text-align: center;">Дата/Время создания брони</th>
			<th colspan="4" style="text-align: center;">Расположение</td>
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
               
                <td> <% using (Html.BeginForm("Reservation", "Res", FormMethod.Post)){%>  <input type="hidden" name="id_reservation_user" id="Hidden3" value="<%: item.id_reservation_user%>" /> <% if (item.Status == "Formed")
                { %><input type="submit" name="Reservation" id="Connect" value="Connect" /> <% } %><% } %>
                </td>

                <td> <% using (Html.BeginForm("Agreement", "Res", FormMethod.Post)){%>  <input type="hidden" name="id_Reservation_Tariff" id="Hidden2" value="<%: item.id_Reservation_Tariff %>" /> <% if (item.Status == "Formed")
                 { %><input type="submit" name="Agreement" id="Continue_Formed" value="Continue_Formed" />  <% } %><% } %>
                </td>

                <td>    
              <% using (Html.BeginForm("Reservation", "Res", FormMethod.Post)){%>  <input type="hidden" name="id_reservation_user" id="Hidden1" value="<%: item.id_reservation_user%>" />  <input type="submit" name="Reservation" id="Revoke" value="Revoke" />    <% } %>
               </td>
            <td>
                <%: item.id_Reservation_Tariff %>
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
       
    <p>
    У вас нет активной заявки на бронирование, но Вы можете создать ее щелкнув
        <%: Html.ActionLink(" сюда", "Agreement") %>
    </p>
</asp:Content>

