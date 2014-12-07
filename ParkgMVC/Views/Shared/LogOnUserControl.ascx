<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if (Request.IsAuthenticated) {
%>
        Добро пожаловать <b><%: Page.User.Identity.Name %></b>! <br />
                      <% ParkgMVC.Models.MyParkingEntities mp = new ParkgMVC.Models.MyParkingEntities();
                         ParkgMVC.Models.usr u = mp.usr.Where(x => x.Login == Page.User.Identity.Name).FirstOrDefault();
                 ViewData["Balance"] = u.Now_Balance;%>
       Ваш баланс: <b><%: ViewData["Balance"] %></b>
        [ <%: Html.ActionLink("Выход", "LogOff", "Account") %> ]
<%
    }
    else {
%> 
        [ <%: Html.ActionLink("Вход", "LogOn", "Account") %> ]
<%
    }
%>
