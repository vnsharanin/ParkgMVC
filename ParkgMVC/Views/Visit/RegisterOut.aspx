<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ParkgMVC.Models.ts>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	RegisterOut
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <h1 style="color:#FF0000"><%: ViewData["ExceptionRegisterOut"]%></h1>
    <h2>RegisterOut</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>

            <div class="editor-label">
                <%: Html.LabelFor(model => model.Number) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Number) %>
                <%: Html.ValidationMessageFor(model => model.Number) %>
            </div>

            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

            <fieldset>
            <h2>Active visit:</h2>

    <table>
        <tr>
            <th rowspan="3">Номер ТС</th>
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

    <% foreach (var item in (IEnumerable<ParkgMVC.Models.visit>)ViewData["ActiveVisit"]) {%>
    
        <tr>
               <td><%: item.ts.Number %> </td>
               <td><%: item.place.levelzone.Parking_zone%></td>
               <td><%: item.place.levelzone.Level%></td>
               <td><%: item.place.levelzone.TypeLevel%></td>
               <td><%: item.place.NumberPlace%></td>
        </tr>
    
    <% } %>

    </table>


            </fieldset>
    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

