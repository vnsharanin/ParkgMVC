<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	AllTariffs
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>AllTariffs</h2>
           <li><a href="#"><span><%: Html.ActionLink("Reservation Tariffs", "ReservationTariffs") %></span></a></li>
 
         
       <li><a href="#"><span> <%: Html.ActionLink("Tariffs on places", "TariffsOnPlace") %></span></a></li>
  <li><a href="#"><span> <%: Html.ActionLink("Tariffs on abonements", "TariffsOnAbonements") %></span></a></li>
         <li><a href="#"><span> <%: Html.ActionLink("Visit Parameters", "VisitParameters") %></span></a></li>
</asp:Content>
