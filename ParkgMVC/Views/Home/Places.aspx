<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ParkgMVC.Models.place>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Places
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

    <h2>Places</h2>
        <%: ViewData["Zone-Level"] %>
    <table>
        <tr>
            <th></th>
            <th>
                Place
            </th>
            <th>
                Status
            </th>
            <th>
                Номер тарифа
            </th>
        </tr>
    
    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%: Html.ActionLink("Edit", "Edit", new { id_location_place = item.id_location_place })%> |
                <%: Html.ActionLink("Details", "Details", new { id_location_place = item.id_location_place })%> |
                <%: Html.ActionLink("Delete", "Delete", new { id_location_place = item.id_location_place })%> |
                           <% if (ViewData["Reservation"].ToString() == "RESERVATION") { %> 
                <input type="radio" id="ChoosePlace" name="ChoosePlace" value="<%: item.id_location_place %>" onchange="document.getElementById('SaveReservation').disabled = !this.checked; document.getElementById('ConnectReservation').disabled = !this.checked; document.getElementById('label').innerHTML='';"/>
                <input type="hidden" name="id_location_level" id="id_location_level" value="<%: item.id_location_level %>" />
                           <%   } %>
            </td>
            <td>
                <%: item.NumberPlace %>
            </td>
            <td>
                <%: item.Status %>
            </td>
            <td>
                <%: item.id_tariff_on_place %>
            </td>
        </tr>
    
    <% } %>
    </table>

    <p>
        <%: Html.ActionLink("Create New", "Create") %>
    </p>
            <p>
             <% if (ViewData["Reservation"].ToString() == "RESERVATION") { %> 
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
</asp:Content>

