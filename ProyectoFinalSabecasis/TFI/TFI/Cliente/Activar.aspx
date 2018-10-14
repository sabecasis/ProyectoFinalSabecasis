<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ConsumidorFinal.Master" CodeBehind="Activar.aspx.vb" Inherits="TFI.Activar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            var param = window.location.search.replace("?", "");
            var usuario = param.split("=")[1];
            function onSuccess(result) {
                alert('El usuario ha sido activado con éxito');
                window.location = "/Cliente/Perfil.aspx";
            }
            function onFailure(result) {
                alert('Algo ocurrió con la activación y no se ha podido efectuar correctamente');
            }

            PageMethods.desbloquear(usuario, onSuccess, onFailure);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
</asp:Content>
