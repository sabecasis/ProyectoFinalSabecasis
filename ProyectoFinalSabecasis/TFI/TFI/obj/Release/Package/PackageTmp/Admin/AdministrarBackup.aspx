<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="AdministrarBackup.aspx.vb" Inherits="TFI.AdministrarBackup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload=function(){
            obtenerListBackup();
        }

        function restaurarBackup() {
            var url = this.id;
            function onSuccess(result) {
                alert(result);
            }
            function onFailure(result) { }
            PageMethods.restaurarBackup(url, onSuccess, onFailure);
        }

        function obtenerListBackup() {
            function onSuccess(result) {
                if (result != null) {
                    var cont = document.getElementById('listadoBackups');
                    var tabla = document.createElement('TABLE');
                    cont.appendChild(tabla);
                    var trheader = document.createElement('tr');
                    var tdd = document.createElement('td');
                    tdd.appendChild(document.createTextNode('Id'));
                    trheader.appendChild(tdd);
                    tabla.appendChild(trheader);
                    var tdd2 = document.createElement('td');
                    tdd2.appendChild(document.createTextNode('Backup'));
                    trheader.appendChild(tdd2);
                    for (i = 0; i < result.length; i++) {
                        var tr = document.createElement('TR');
                        var td = document.createElement('td');
                        var a = document.createElement('a');
                        a.href = "#";
                        a.onclick = restaurarBackup;
                        a.id = result[i].urlEnServidor;
                        a.appendChild(document.createTextNode(result[i].urlEnServidor));
                        td.appendChild(a);
                        var td1 = document.createElement('td');
                        td1.appendChild(document.createTextNode(result[i].id));
                        tr.appendChild(td1);
                        tr.appendChild(td);
                        tabla.appendChild(tr);
                    }
                }
                
            }
            function onFailure(result) { }

            PageMethods.obtenerBackups(onSuccess, onFailure);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
    <aside id="lateral"></aside>
    <section id="contenedor">
        <div id="2barra"></div>
        <input type="hidden" id="accion" name="accion" />
        <input type="submit" id="backupBtn" value="Crear Backup"  onclick="setAccion(this.id)" />
        <div id="3barra"></div>
        <div id="listadoBackups">

        </div>
    </section>
        </div>
</asp:Content>
