<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ParkgMVC.Models.tariffonplace>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	TariffsOnPlace
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>TariffsOnPlace</h2>
    <fieldset>
    <h2>Тарифы, активные на текущий момент</h2>
        <table>
        <tr>
                        <% if (User.IsInRole("Admin"))
                   { %>
            <th></th>        <%} %>
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
        </tr>

    <% foreach (var item in (IEnumerable<ParkgMVC.Models.tariffonplace>)ViewData["ActiveTariffs"]) {%>
    
        <tr>
        <% if (User.IsInRole("Admin")) { %>
                         <% using (Html.BeginForm("Edit_tariff_on_place", "Tariffs", FormMethod.Post))
                            {%>
            <td>

                <input type="hidden" name="id_tariff_on_place" id="Hidden2" value="<%: item.id_tariff_on_place %>" />
                                 <input type="submit" name="Edit_tariff_on_place" id="Edit_tariff_on_place" value="Edit_tariff_on_place" />
                 <input type="submit" name="Edit_tariff_on_place" id="Submit1" value="Off_tariff" />
            </td>
            <% } } %>
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

                        <% if (User.IsInRole("Admin"))
                       { %>
                       <br />
                             <% using (Html.BeginForm("New_tariff_on_place", "Tariffs", FormMethod.Post))
                                {%>
                     <input type="submit" name="New_tariff_on_place" id="Submit2" value="New_tariff_on_place" /><% }
                       } %>
    </fieldset>
    <fieldset>
    <h2>Тарифы в неактивном состоянии</h2>

    <table>
        <tr>
                        <% if (User.IsInRole("Admin"))
                   { %>
            <th></th>        <%} %>
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

    <% foreach (var item in (IEnumerable<ParkgMVC.Models.tariffonplace>)ViewData["NotActiveTariffs"]) {%>
    
        <tr>
        <% if (User.IsInRole("Admin")) { %>
                         <% using (Html.BeginForm("Edit_tariff_on_place", "Tariffs", FormMethod.Post))
                            {%>
            <td>
                <input type="hidden" name="id_tariff_on_place" id="Hidden1" value="<%: item.id_tariff_on_place %>" />
                 <input type="submit" name="Edit_tariff_on_place" id="Submit3" value="Activate_tariff" />
            </td>
            <% } } %>
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
    <p>
        <%: Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

