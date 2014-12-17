<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ParkgMVC.Models.levelzone>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit_a_p
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script src="/Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
        <h1 style="color:#FF0000"><%: ViewData["EditLevel"]%></h1>
    <h2>Редактирование уровня</h2>
        <% using (Html.BeginForm("Edit_a_p", "Home", FormMethod.Post))
           {%>

                           <%  ParkgMVC.Models.MyParkingEntities mp = new ParkgMVC.Models.MyParkingEntities();
                               int ap = mp.place.Count(x => x.id_location_level == Model.id_location_level & x.Status != "Was replaced" & x.Status != "Disabled");          
                            %>

<h3>Выбранный уровень:</h3>
        	<table>	<tr>
			<th rowspan="2" style="text-align: center;">Зона</th>
			<th colspan="2" style="text-align: center;">Уровень</th>
			<th rowspan="2" style="text-align: center;">Всего мест в уровне</th>
		</tr>
		<tr>
			<th style="text-align: center;">Номер</th>
			<th style="text-align: center;">Тип</th>
		</tr>
                                <td><%:  Model.Parking_zone%> </td>

               <td> <%:  Model.Level %></td>
               <td><%:  Model.TypeLevel %></td>
               <td> <%:  ap  %> </td>
                      </table>


<br />
<h3>Изменение выбранного уровня:</h3>

           <input type="hidden" name="id_location_level" id="id_location_level" value="<%: Model.id_location_level %>" />
           <input type="hidden" name="Parking_zone" id="Parking_zone" value="<%: Model.Parking_zone %>" />
                     <input type="hidden" name="EditPlace" id="EditPlace" value="0" />

                       

            <a href="javascript:void(0)" id="toglink0">...раскрыть управление местами уровня</a>
<div class="togblock" id="togblock0">








            <br />
                      <input type="radio"   id="AmPlace" name="AmPlace" value="NewAmountPlaces" onchange="check()">Задать новое количество мест: </> 
          <input type="text" id="NewAmountPlaces" disabled="true" name="NewAmountPlaces" value="" " />

           <br />

                      <input type="radio" id="AdPlace" name="AmPlace" value="AddPlaces"  onchange="check()">Добавить количество мест: </>
           <input type="text" id="AddPlaces" disabled="true" name="AddPlaces" value="" />
  <br />
  Статус добавляемых мест:
  <select name="Status" size="1" id="Status" disabled="true"> 
<option value="Free">Free</option>
<option selected="selected" value="Not working">Not working</option>
</select>
<br />
<br />

Выберите тариф.
        <table>
        <tr>
            <th></th>
            <th>
                Номер тарифа
            </th>
            <th>
                Поддержка климат-контроля
            </th>
            <th>
                Тип(открытый, крытый, полукрытый)
            </th>
            <th>
                Цена за час без абонемента
            </th>
            <th>
                Цена за час с абонементом
            </th>
            <th>
                Статус
            </th>
        </tr>
        </tr>

    <% foreach (var item in (IEnumerable<ParkgMVC.Models.tariffonplace>)ViewData["ActiveTariffs"]) {%>
    
        <tr>
            <td>
                <input type="radio" id="ChTariffForPlaces" name="ChTariffForPlaces" value="<%: item.id_tariff_on_place %>"/>
             
            </td>
            <td>
                <%: item.id_tariff_on_place %>
            </td>
            <td>
                <%: item.SupportClimateControl %>
            </td>
            <td>
                <%: item.Type %>
            </td>
            <td>
                <%: String.Format("{0:F}", item.PriceForHourWithoutAbonement) %>
            </td>
            <td>
                <%: String.Format("{0:F}", item.PriceForHourWithAbonement) %>
            </td>
            <td>
                <%: item.Status %>
            </td>
        </tr>
    
    <% } %>

    </table>


     










<% if (ap == 0)
   { %>
<input type="checkbox" id="TariffForAllPlace" disabled="true" name="TariffForAllPlace" value="True" />Изменить тариф для всех мест уровня</>
<% }
   else
   { %>
   <input type="checkbox" id="Checkbox1" name="TariffForAllPlace" value="True" />Изменить тариф для всех мест уровня</>

<%} %>
</div>
<br />
           <br />
             <input type="submit" name="Edit_a_p" id="Change_level" value="Change_level" /> 


        <script>
            function check() {
                var rarr = document.getElementsByName("AmPlace");
                if (rarr[1].checked) {
                    document.getElementById("NewAmountPlaces").disabled = true;
                    document.getElementById("NewAmountPlaces").value = "";
                    document.getElementById("AddPlaces").disabled = false;
                    document.getElementById("Status").disabled = false;
                }
                else {
                    document.getElementById("AddPlaces").disabled = true;
                    document.getElementById("AddPlaces").value = "";
                    document.getElementById("NewAmountPlaces").disabled = false;
                    document.getElementById("NewAmountPlaces").value = <%: ap %>;
                    document.getElementById("Status").disabled = false;
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
    if ($('#toglink0').text() == 'скрыть управление местами уровня...') {
        $('#toglink0').text('...раскрыть управление местами уровня');
        document.getElementById("EditPlace").value = "0";
        document.getElementById("Status").disabled = true;
        document.getElementById("AddPlaces").disabled = true;
        document.getElementById("NewAmountPlaces").disabled = true;
        document.getElementById("AddPlaces").value = "";
        document.getElementById("NewAmountPlaces").value = "";
        var rarr = document.getElementsByName("AmPlace");
        if (rarr[1].checked) {
            rarr[1].checked = false;
        }
        else {
            rarr[0].checked = false;
        }



    } else {
        $('#toglink0').text('скрыть управление местами уровня...');
        document.getElementById("EditPlace").value = "1";
    }
});
    });
 </script>

        <% } %>
</asp:Content>
