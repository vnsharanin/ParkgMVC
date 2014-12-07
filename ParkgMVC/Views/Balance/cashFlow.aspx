<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ParkgMVC.Models.balance>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	cashFlow
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1 style="color:#FF0000"><%: ViewData["Message"] %></h1>
    <h2>cashFlow</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            
            
            <input type="radio" name="r" value="1" onchange="check()">Number TS: </>
            <input type="text" id="Number"  disabled="true" name="Number" value="" />

            <div class="editor-field">
            <input type="radio" name="r" value="2" checked="true" onchange="check()">Login: </>
                <%: Html.TextBoxFor(model => model.Login) %>
                <%: Html.ValidationMessageFor(model => model.Login) %>
            </div>


              <label>Тип операции: </label>
            <%: Html.DropDownListFor(model => model.Type_Operation, new SelectList(new[] { "Replenishment", "Debit" }))%>

           
            <div class="editor-field">
             <label>Сумма: </label>
                <%: Html.TextBoxFor(model => model.Sum) %>
                <%: Html.ValidationMessageFor(model => model.Sum) %>
            </div>
            <p>
                <input type="submit" value="Carry out" />
            </p>
        </fieldset>


        <script>
            function check() {
                var rarr = document.getElementsByName("r");
                if (rarr[1].checked) {
                    document.getElementById("Number").disabled = true;
                    document.getElementById("Number").value = "";
                    document.getElementById("Login").disabled = false;
                }
                else {
                    document.getElementById("Login").disabled = true;
                    document.getElementById("Login").value = "";
                    document.getElementById("Number").disabled = false;
                    
                }
            }
</script>

        
    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

