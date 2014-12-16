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
			<th rowspan="2" style="text-align: center; width: 55px;">Зона</th>
			<th colspan="2" style="text-align: center;">Уровень</th>
			<th rowspan="2" style="text-align: center; width: 81px;">Место</th>
            			<th rowspan="2" style="text-align: center; width: 174px;">Поддержка климат-контроля </th>
                        			<th rowspan="2" style="text-align: center; width: 214px;">Тип(открытый, крытый, полукрытый) </th>
                                    <th rowspan="2" style="text-align: center; width: 162px;">Цена за час без абонемента  </th>
                                    <th rowspan="2" style="text-align: center; width: 217px;">Цена за час с абонементом   </th>
		</tr>
		<tr>
			<th style="text-align: center; width: 55px;">Номер</th>
			<th style="text-align: center; width: 115px;">Тип</th>
		</tr>
                        <td style="width: 55px"><%:  Model.levelzone.Parking_zone%> </td>

               <td style="width: 55px"> <%:  Model.levelzone.Level %></td>
               <td style="width: 115px"><%:  Model.levelzone.TypeLevel %></td>
               <td style="width: 81px"> <%:  Model.NumberPlace %> </td>
                              <td style="width: 174px"> <%:  Model.tariffonplace.SupportClimateControl %> </td>
                              <td style="width: 214px"> <%:  Model.tariffonplace.Type %> </td>
                              <td style="width: 162px"> <%:  Model.tariffonplace.PriceForHourWithoutAbonement %> </td>
                              <td style="width: 217px"> <%:  Model.tariffonplace.PriceForHourWithAbonement %> </td>
                      </table>

</fieldset>
        <fieldset>
            
    <h2>Тарифы, доступные на текущий момент</h2>
        <table>
        <tr>
            <th></th>
            <th style="width: 120px">
                Номер тарифа
            </th>
            <th style="width: 173px">
                Поддержка климат-контроля
            </th>
            <th>
                Тип(открытый, крытый, полукрытый)
            </th>
            <th style="width: 154px">
                Цена за час без абонемента
            </th>
            <th style="width: 157px">
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
                                   <input type="submit" name="Places" 
                    id="Choose_tariff_on_place" value="Choose_tariff_on_place" 
                    style="width: 200px" />

                <% } %>
            </td>
            <td style="width: 120px">
                <%: item.id_tariff_on_place %>
            </td>
            <td style="width: 173px">
                <%: item.SupportClimateControl %>
            </td>
            <td>
                <%: item.Type %>
            </td>
            <td style="width: 154px">
                <%: String.Format("{0:F}", item.PriceForHourWithoutAbonement) %>
            </td>
            <td style="width: 157px">
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

