<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ParkgMVC.Models.tariffonplace>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit_tariff_on_place
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit_tariff_on_place</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Fields</legend>
                                        <input type="hidden" name="id_tariff_on_place" id="id_tariff_on_place" value="<%: Model.id_tariff_on_place %>" />
         
            <div class="editor-label">
                <%: Html.LabelFor(model => model.PriceForHourWithoutAbonement) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.PriceForHourWithoutAbonement, String.Format("{0:F}", Model.PriceForHourWithoutAbonement)) %>
                <%: Html.ValidationMessageFor(model => model.PriceForHourWithoutAbonement) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.PriceForHourWithAbonement) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.PriceForHourWithAbonement, String.Format("{0:F}", Model.PriceForHourWithAbonement)) %>
                <%: Html.ValidationMessageFor(model => model.PriceForHourWithAbonement) %>
            </div>
            
            <p>
          <input type="submit" name="Edit_tariff_on_place" id="Change_tariff_on_place" value="Change_tariff_on_place" />
                </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

