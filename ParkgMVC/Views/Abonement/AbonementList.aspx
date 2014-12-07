<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	AbonementList
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <h1 style="color:#FF0000"><%: ViewData["MyAbonementMessage"] %></h1>
    <h2>Ваши абонементы</h2>
    <fieldset><h2>Активный абонемент</h2>
    <table>
        <tr>
            <th></th>
            <th>
                Наименование тарифа на абонмент
            </th>
            <th>
                Количество посещений, сделанных по абонементу
            </th>
            <th>
                Дата подключения
            </th>
        </tr>

    <% foreach (var item in (IEnumerable<ParkgMVC.Models.usingtariffonabonementforvisit>)ViewData["ActiveAbonement"]) {%>
                         <% using (Html.BeginForm("AbonementList", "Abonement", FormMethod.Post))
                       {%>
     <input type="hidden" name="id_abonement" id="Hidden" value="<%: item.id_abonement %>" />
        <tr>
            <td>
              <input type="submit" name="AbonementList" id="Revoke_abonement" value="Revoke_abonement"  />
            </td>
            <td>
                <%: item.Name_tariff_on_abonement %>
            </td>
            <td>
                <%: item.NumOfVisitsMadeWithUsingThisTariff %>
            </td>
            <td>
                <%: item.DateConnection %>
            </td>
        </tr>
        <%} %>
    <% } %>

    </table>
    <% using (Html.BeginForm("TariffsOnAbonements", "Tariffs", FormMethod.Post))
       {%>
    <p>
        У Вас нет абонмента? Вы можете выбрать и подключить его, щелкнув по кнопке <input type="submit" name="TariffsOnAbonements" id="Choose_abonement" value="Choose_abonement"  />
    </p>
    <% } %>
    </fieldset>
    <fieldset>
    <h2>Используемые Вами ранее абонементы</h2>
    
        <table>
        <tr>
            <th>
                Наименование тарифа на абонмент
            </th>
            <th>
                Количество посещений, сделанных по абонементу
            </th>
            <th>
                Дата подключения
            </th>
            <th>
                Дата выхода из активности
            </th>
            <th>
                Статус
            </th>
        </tr>

    <% foreach (var item in (IEnumerable<ParkgMVC.Models.usingtariffonabonementforvisit>)ViewData["NotActiveAbonements"]) {%>
    
        <tr>
            <td>
                <%: item.Name_tariff_on_abonement %>
            </td>
            <td>
                <%: item.NumOfVisitsMadeWithUsingThisTariff %>
            </td>
            <td>
                <%: item.DateConnection %>
            </td>
            <td>
                <%: item.DateOutFromActivity %>
            </td>
            <td>
                <%: item.Status %>
            </td>
        </tr>
    
    <% } %>

    </table>
    </fieldset>
</asp:Content>

