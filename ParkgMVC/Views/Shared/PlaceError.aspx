<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<System.Web.Mvc.HandleErrorInfo>" %>

<asp:Content ID="errorTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Ошибка
</asp:Content>

<asp:Content ID="errorContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Возможно, вы пытались перезагрузить страницу, связанную с изменением тарифа, что из-за безопасности делать нельзя, если же нет, то просим Вас связаться с высшим руководством.
    </h2>
</asp:Content>
