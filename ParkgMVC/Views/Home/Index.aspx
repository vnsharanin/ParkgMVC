<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>
<ul id="vert_menu">
<% if (!User.IsInRole("Security"))
   { %>
       <li><a href="#"><span><%: Html.ActionLink("Listening all tariffs", "AllTariffs", "Tariffs")%></span></a></li>
       <li><a href="#"><span> <%: Html.ActionLink("Zones/Levels/Places", "ZonesLevelsPlaces")%></span></a></li>

       <% } %>
      <% if (User.IsInRole("Driver")) { %>
       <li><a href="#"><span><%: Html.ActionLink("List TS", "TS","TS") %></span></a></li>
 
        
            <li><a href="#"><span>  <%: Html.ActionLink("Visit", "ListVisit", new { Controller = "Visit" })%></span></a></li>
       <li><a href="#"><span> <%: Html.ActionLink("List your Reservation", "Reservation", new { Controller = "Res" })%></span></a></li>
  
  <li><a href="#"><span><%: Html.ActionLink("Your abonements", "AbonementList", new { Controller = "Abonement" })%></span></a></li>
         
      <li><a href="#"><span>  <%: Html.ActionLink("Balance", "Balance", "Balance")%></span></a></li>
           <% } %>
      <% if (User.IsInRole("Security")) { %>
            <li><a href="#"><span>  <%: Html.ActionLink("ChangeBalance", "cashFlow", "Balance")%></span></a></li>

                <li><a href="#"><span>  <%: Html.ActionLink("Register In", "RegisterIn", new { Controller = "Visit" })%></span></a></li>
         <li><a href="#"><span>  <%: Html.ActionLink("Register Out", "RegisterOut", new { Controller = "Visit" })%></span></a></li>

      <% } %>
   </ul>
</asp:Content>
