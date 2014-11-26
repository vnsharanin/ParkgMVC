<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>
<ul id="vert_menu">
       <li><a href="#"><span><%: Html.ActionLink("List TS", "TS","TS") %></span></a></li>
 
         
       <li><a href="#"><span> <%: Html.ActionLink("List VISIT", "VISIT") %></span></a></li>
  
      
       <li><a href="#"><span> <%: Html.ActionLink("Zones/Levels/Places", "ZonesLevelsPlaces")%></span></a></li>
 
               
       <li><a href="#"><span> <%: Html.ActionLink("List your Reservation", "Reservation", new { Controller = "Res" })%></span></a></li>
  

         
      <li><a href="#"><span>  <%: Html.ActionLink("Balance", "Balance", "Balance")%></span></a></li>
   </ul>
</asp:Content>
