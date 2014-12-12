<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ParkgMVC.Models.place>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Place_tariff
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%: ViewData["Place_tariff"]%></h1>
        <h1 style="color:#FF0000"><%: ViewData["Place_tariff_err_message"]%></h1>

   
        <fieldset>
        <h2>Изменяемое месторасположение:</h2>
        	<table>	<tr>
			<th rowspan="2" style="text-align: center;">Зона</th>
			<th colspan="2" style="text-align: center;">Уровень</th>
			<th rowspan="2" style="text-align: center;">Место</th>
		</tr>
		<tr>
			<th style="text-align: center;">Номер</th>
			<th style="text-align: center;">Тип</th>
		</tr>
                        <td><%:  Model.levelzone.Parking_zone%> </td>

               <td> <%:  Model.levelzone.Level %></td>
               <td><%:  Model.levelzone.TypeLevel %></td>
               <td> <%:  Model.NumberPlace %> </td>
                      </table>

</fieldset>
        <fieldset>
            
    <h2>Тарифы, доступные на текущий момент</h2>
        <table>
        <tr>
            <th></th>
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
            <td>
               <% using (Html.BeginForm("Places", "Home", FormMethod.Post))
               {%>
                                  <input type="hidden" name="choose_tariff_on_place" id="choose_tariff_on_place" value="<%: item.id_tariff_on_place %>" />
                               <input type="hidden" name="not_working_place" id="not_working_place" value="<%: Model.id_location_place %>" />
                                   <input type="submit" name="Places" id="Choose_tariff_on_place" value="Choose_tariff_on_place" />

                <% } %>
            </td>
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

   

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

