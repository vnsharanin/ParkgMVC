<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ParkgMVC.Models.ts>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1 style="color:#FF0000"><%: ViewData["Error"] %></h1>
    <h2>Index</h2>

    <table>
        <tr>
            <th></th>
            <th>
                Number
            </th>
            <th>
                Company
            </th>
            <th>
                Mode
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    <% using (Html.BeginForm("TS", "TS", FormMethod.Post)){%>
    <input type="hidden" name="id_ts" id="id_ts" value="<%: item.id_ts %>"/>
        <tr>
            <td>
                <input type="submit" name="TS" id="Delete_TS" value="Delete_TS" />
            </td>
            <td>
                <%: item.Number %>
            </td>
            <td>
                <%: item.Company %>
            </td>
            <td>
                <%: item.Mode %>
            </td>
        </tr>
        <% } %>
    <% } %>

    </table>

    <p>
        <%: Html.ActionLink("Create New", "CreateTS") %>
    </p>

</asp:Content>

