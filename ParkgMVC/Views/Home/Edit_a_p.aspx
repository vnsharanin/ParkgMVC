<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ParkgMVC.Models.levelzone>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit_a_p
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <h1 style="color:#FF0000"><%: ViewData["EditLevel"]%></h1>
    <h2>Редактирование уровня</h2>
        <% using (Html.BeginForm("Edit_a_p", "Home", FormMethod.Post))
           {%>
           <input type="hidden" name="id_location_level" id="id_location_level" value="<%: Model.id_location_level %>" />
           <input type="hidden" name="Parking_zone" id="Parking_zone" value="<%: Model.Parking_zone %>" />
          

                      <input type="radio"  checked="true" id="AmPlace" name="AmPlace" value="NewAmountPlaces" onchange="check()">Задать новое количество мест: </> 
          <input type="text" id="NewAmountPlaces" name="NewAmountPlaces" onchange="getNew()" value="" />

           <br />

                      <input type="radio" id="AdPlace" name="AmPlace" value="AddPlaces"  onchange="check()">Добавить количество мест: </>
           <input type="text" id="AddPlaces" disabled="true" name="AddPlaces" onchange="getAdd()" value="" />
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
                }
                else {
                    document.getElementById("AddPlaces").disabled = true;
                    document.getElementById("AddPlaces").value = "";
                    document.getElementById("NewAmountPlaces").disabled = false;

                }
            }

            function getNew() {
              
                document.getElementById("AmPlace").value = document.getElementById("NewAmountPlaces").value;
            }
                        function getAdd() {
            
                document.getElementById("AdPlace").value = document.getElementById("AddPlaces").value;
            }
</script>
        <% } %>
</asp:Content>
