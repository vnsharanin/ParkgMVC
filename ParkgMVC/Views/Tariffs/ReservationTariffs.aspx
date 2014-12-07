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
        <th></th>
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
            <td>
                <%: Html.ActionLink("Edit", "Edit", new { /* id=item.PrimaryKey */ }) %> |
                <%: Html.ActionLink("Details", "Details", new { /* id=item.PrimaryKey */ })%> |
                <%: Html.ActionLink("Delete", "Delete", new { /* id=item.PrimaryKey */ })%>
            </td>
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
    <br />
    <fieldset>
    <h2>Неактивные тарифы</h2>
        <table>
        <tr>
            <th></th>
            <th>
                id_Reservation_Tariff
            </th>
            <th>
                FirstFreeTimeInMinutes
            </th>
            <th>
                PriceInRubForHourHightFreeTime
            </th>
            <th>
                ValidityPeriodFromTheTimeOfActivationInHour
            </th>
            <th>
                Status
            </th>
        </tr>

    <% foreach (var item in (IEnumerable<ParkgMVC.Models.reservation_tariff>)ViewData["NotActiveTariffs"]) {%>
    
        <tr>
            <td>
                <%: Html.ActionLink("Edit", "Edit", new { /* id=item.PrimaryKey */ }) %> |
                <%: Html.ActionLink("Details", "Details", new { /* id=item.PrimaryKey */ })%> |
                <%: Html.ActionLink("Delete", "Delete", new { /* id=item.PrimaryKey */ })%>
            </td>
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
                <%: item.ValidityPeriodFromTheTimeOfActivationInHour %>
            </td>
            <td>
                <%: item.Status %>
            </td>
        </tr>
    
    <% } %>

    </table>

    
    </fieldset>
    <p>
        <%: Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

