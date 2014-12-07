<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	VisitParameters
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Visit Parameters</h2>
<fieldset>
    <h2>Текущие параметры</h2>
    <table>
        <tr>
            <th></th>
            <th>
                Номер набора параметров
            </th>
            <th>
                Первое бесплатное время (в минутах с момента въезда)
            </th>
            <th>
                Первое бесплатное время на погашение отрицательного баланса (при выезде)
            </th>
            <th>
                Статус набора параметров
            </th>
        </tr>

    <% foreach (var item in (IEnumerable<ParkgMVC.Models.visitparameters>)ViewData["ActiveParameters"]) {%>
    
        <tr>
            <td>
                <%: Html.ActionLink("Edit", "Edit", new { /* id=item.PrimaryKey */ }) %> |
                <%: Html.ActionLink("Details", "Details", new { /* id=item.PrimaryKey */ })%> |
                <%: Html.ActionLink("Delete", "Delete", new { /* id=item.PrimaryKey */ })%>
            </td>
            <td>
                <%: item.id_vis_param %>
            </td>
            <td>
                <%: item.FirstFreeTimeInMinutes %>
            </td>
            <td>
                <%: item.FirstFreeTimeOnChangeBalans %>
            </td>
            <td>
                <%: item.Status %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%: Html.ActionLink("Create New", "Create") %>
    </p>
        </fieldset>
        <fieldset>
        <h2>Неактивные наборы параметров</h2>
        
            <table>
        <tr>
            <th></th>
            <th>
                Номер набора параметров
            </th>
            <th>
                Первое бесплатное время (в минутах с момента въезда)
            </th>
            <th>
                Первое бесплатное время на погашение отрицательного баланса (при выезде)
            </th>
            <th>
                Статус набора параметров
            </th>
        </tr>

    <% foreach (var item in (IEnumerable<ParkgMVC.Models.visitparameters>)ViewData["NotActiveParameters"]) {%>
    
        <tr>
            <td>
                <%: Html.ActionLink("Edit", "Edit", new { /* id=item.PrimaryKey */ }) %> |
                <%: Html.ActionLink("Details", "Details", new { /* id=item.PrimaryKey */ })%> |
                <%: Html.ActionLink("Delete", "Delete", new { /* id=item.PrimaryKey */ })%>
            </td>
            <td>
                <%: item.id_vis_param %>
            </td>
            <td>
                <%: item.FirstFreeTimeInMinutes %>
            </td>
            <td>
                <%: item.FirstFreeTimeOnChangeBalans %>
            </td>
            <td>
                <%: item.Status %>
            </td>
        </tr>
    
    <% } %>

    </table>
        
        </fieldset>
</asp:Content>

