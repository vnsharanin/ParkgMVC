<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ReservationTariffs
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>ReservationTariffs</h2>
    <fieldset>
    <h2>Текущий тариф на бронирование</h2>
    <table>
        <tr>
                <% if (User.IsInRole("Admin"))
                   { %>
        <th></th>
        <%} %>
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

    <% foreach (var item in (IEnumerable<ParkgMVC.Models.reservation_tariff>)ViewData["ActiveTariff"]) {%>
    
        <tr>
        <% if (User.IsInRole("Admin")) { %>
                         <% using (Html.BeginForm("Edit_reservation_tariff", "Tariffs", FormMethod.Post))
                            {%>
            <td>
                <input type="hidden" name="id_reservation_tariff" id="id_reservation_tariff" value="<%: item.id_Reservation_Tariff %>" />
                 <input type="submit" name="Edit_reservation_tariff" id="Edit_reservation_tariff" value="Edit_reservation_tariff" />
                                  <input type="submit" name="Edit_reservation_tariff" id="Submit2" value="Off_reservation_tariff" />
            </td>
            <% } } %>
            <td>
                <%: item.id_Reservation_Tariff %>
            </td>
            <td>
                <%: item.FirstFreeTimeInMinutes %>
            </td>
            <td>
                <%: String.Format("{0:F}", item.PriceInRubForHourHightFreeTime) %>
            </td>
            <td>
                <%: item.Status %>
            </td>
            <td>
                <%: item.ValidityPeriodFromTheTimeOfActivationInHour %>
            </td>
        </tr>
    
    <% } %>

    </table>
                    <% if (User.IsInRole("Admin"))
                       { %>
                       <br />
                             <% using (Html.BeginForm("Create_reservation_tariff", "Tariffs", FormMethod.Post))
                                {%>
                     <input type="submit" name="Create_reservation_tariff" id="Submit1" value="New_reservation_tariff" /><% }
                       } %>
    </fieldset>
    <br />
    <fieldset>
    <h2>Неактивные тарифы</h2>
        <table>
        <tr>
                        <% if (User.IsInRole("Admin"))
                           { %>
            <th></th>
            <%} %>
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

    <% foreach (var item in (IEnumerable<ParkgMVC.Models.reservation_tariff>)ViewData["NotActiveTariffs"]) {%>
    
        <tr>
                        <% if (User.IsInRole("Admin"))
                           { %>
                                 <% using (Html.BeginForm("Edit_reservation_tariff", "Tariffs", FormMethod.Post))
                                    {%>
            <td>
                            <input type="hidden" name="id_reservation_tariff" id="Hidden1" value="<%: item.id_Reservation_Tariff %>" />
                                  <input type="submit" name="Edit_reservation_tariff" id="Submit3" value="Activate_reservation_tariff" />
            </td>
            <% }
                           }%>
            <td>
                <%: item.id_Reservation_Tariff %>
            </td>
            <td>
                <%: item.FirstFreeTimeInMinutes %>
            </td>
            <td>
                <%: String.Format("{0:F}", item.PriceInRubForHourHightFreeTime) %>
            </td>
            <td>
                <%: item.Status %>
            </td>
            <td>
                <%: item.ValidityPeriodFromTheTimeOfActivationInHour %>
            </td>
        </tr>
    
    <% } %>

    </table>

    
    </fieldset>

</asp:Content>

