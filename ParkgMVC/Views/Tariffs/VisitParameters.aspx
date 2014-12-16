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
                        <% if (User.IsInRole("Admin"))
                   { %>
            <th></th>        <%} %>
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
        <% if (User.IsInRole("Admin")) { %>
                         <% using (Html.BeginForm("Edit_visit_parameters", "Tariffs", FormMethod.Post))
                            {%>
            <td>
                <input type="hidden" name="id_vis_param" id="id_vis_param" value="<%: item.id_vis_param %>" />
                 <input type="submit" name="Edit_visit_parameters" id="Edit_visit_parameters" value="Edit_visit_parameters" />
                                  <input type="submit" name="Edit_visit_parameters" id="Submit2" value="Off_parameters" />
            </td>
            <% } } %>
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

                    <% if (User.IsInRole("Admin"))
                       { %>
                       <br />
                             <% using (Html.BeginForm("Create_visit_parameters", "Tariffs", FormMethod.Post))
                                {%>
                     <input type="submit" name="Create_visit_parameters" id="Submit1" value="New_visit_parameters" /><% }
                       } %>
        </fieldset>
        <fieldset>
        <h2>Неактивные наборы параметров</h2>
        
            <table>
        <tr>
                                <% if (User.IsInRole("Admin"))
                           { %>
            <th></th>            <%} %>
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
        <% if (User.IsInRole("Admin")) { %>
                         <% using (Html.BeginForm("Edit_visit_parameters", "Tariffs", FormMethod.Post))
                            {%>
            <td>
                <input type="hidden" name="id_vis_param" id="Hidden1" value="<%: item.id_vis_param %>" />
                 <input type="submit" name="Edit_visit_parameters" id="Submit3" value="Activate_parameters" />
            </td>
            <% } } %>
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

