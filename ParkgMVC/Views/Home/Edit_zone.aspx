<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ParkgMVC.Models.parkingzone>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit_zone
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.4/jquery.min.js" type="text/javascript"></script>
        <h1 style="color:#FF0000"><%: ViewData["EditZone1"]%></h1>
                <h1 style="color:#FF0000"><%: ViewData["EditZone2"]%></h1>

    <% using (Html.BeginForm("Edit_zone", "Home", FormMethod.Post))
       {%>
                          <input type="hidden" name="P_z" id="Hidden6" value="<%: Model.Parking_zone  %>" />
            <h2>Редактирование зоны № <%: Model.Parking_zone %></h2>

           <h3>Изменение информации о зоне</h3>

           <b>Тип зоны: </b><%= Html.DropDownList("Type_zone")%>
              <br />
          
            <b>Адрес зоны: </b>
        
                <%: Html.TextBoxFor(model => model.Address) %>

<h3>Управление уровнями</h3>
             



            <a href="javascript:void(0)" id="toglink0">...раскрыть управление уровнями зоны</a>
<div class="togblock" id="togblock0">


                      <input type="radio"   id="AmLev" name="AmLev" value="NewAmountLevels" onchange="check()">Задать новое количество уровней: </> 
          <input type="text" id="NewAmountLevels" disabled="true" name="NewAmountLevels" value="" " />

           <br />

                      <input type="radio" id="AdLev" name="AmLev" value="AddLevels"  onchange="check()">Добавить количество уровней: </>
           <input type="text" id="AddLevels" disabled="true" name="AddLevels" value="" />
  <br />

    Тип добавляемых(закрываемых) уровней:
  <select name="Type_lev" size="1" id="Type_lev" disabled="true"> 
<option value="Underground">Underground</option>
<option selected="selected" value="Elevated">Elevated</option>
</select>
  <br />
  




















</div>
<br />
<br />
                <input type="submit" name="Edit_zone" id="Change_zone" value="Change_zone" /> 
         
                 <script>
            function check() {
                var rarr = document.getElementsByName("AmLev");
                if (rarr[1].checked) {
                    document.getElementById("NewAmountLevels").disabled = true;
                    document.getElementById("NewAmountLevels").value = "";
                    document.getElementById("AddLevels").disabled = false;
                    document.getElementById("Type_lev").disabled = false;
                }
                else {
                    document.getElementById("AddLevels").disabled = true;
                    document.getElementById("AddLevels").value = "";
                    document.getElementById("NewAmountLevels").disabled = false;
                    document.getElementById("Type_lev").disabled = false;
                }
            }
</script>
<style type="text/css">
 
 .togblock {
 display:none;
 text-align:inherit
 }
 
 </style>
<script type="text/javascript">
    $(document).ready(function () {
        $('#toglink0').click(
function () {
    if (jQuery.browser.msie && parseInt(jQuery.browser.version) == 6) {
        if ($('#togblock1').css("display") == "block") {
            $('#togblock0').css("display", "none");
        } else {
            $('#togblock0').css("display", "block");
        }
    } else {
        $('#togblock0').toggle("slow");
    }
    if ($('#toglink0').text() == 'скрыть управление уровнями зоны') {
        $('#toglink0').text('...раскрыть управление уровнями зоны');
        document.getElementById("Type_lev").disabled = true;
        document.getElementById("AddLevels").disabled = true;
        document.getElementById("NewAmountLevels").disabled = true;
        document.getElementById("AddLevels").value = "";
        document.getElementById("NewAmountLevels").value = "";
        var rarr = document.getElementsByName("AmLev");
        if (rarr[1].checked) {
            rarr[1].checked = false;
        }
        else {
            rarr[0].checked = false;
        }


    } else {
        $('#toglink0').text('скрыть управление уровнями зоны');
    }
});
    });
 </script>
    <% } %>

</asp:Content>

