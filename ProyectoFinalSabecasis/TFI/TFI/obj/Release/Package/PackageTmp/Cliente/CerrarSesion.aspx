<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ConsumidorFinal.Master" CodeBehind="CerrarSesion.aspx.vb" Inherits="TFI.CerrarSesion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            document.getElementById('ContentPlaceHolder1_urlanterior').value = document.referrer;
            __doPostBack();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input type="hidden" id="urlanterior" name="urlanterior" runat="server" />
</asp:Content>
