<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ConsumidorFinal.Master" CodeBehind="PagoDeOrden.aspx.vb" Inherits="TFI.PagoDeOrden" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function obtenerTodosLosTiposDePago() {
            function onSuccess(result) {
                var metodo = document.getElementById('metodo');
                if (result != null) {
                    for (i = 0; i < result.length; i++) {
                        var opt = document.createElement('OPTION');
                        opt.text = result[i].metodo;
                        opt.value = result[i].id;
                        metodo.appendChild(opt);
                    }
                }
            }
            function onFailure(result) { }
            PageMethods.obtenerTodosLosTiposDePago(onSuccess, onFailure);
        }

        function crearOpcionesDeCuotas() {
            var select = document.getElementById('cantCuotas');
            for (i = 1; i < 19; i++) {
                var op = document.createElement('OPTION');
                op.value = i;
                op.text = i;
                select.appendChild(op);
            }
        }

        function cargarMeses() {
            var select = document.getElementById('mes');
            for (i = 1; i < 13; i++) {
                var mes =i;
                if (i < 10) {
                    mes = '0' + i;
                }
                var op = document.createElement('OPTION');
                op.text = mes;
                op.value = mes;
                select.appendChild(op);
            }
        }

        function cargarAnios() {
            var select = document.getElementById('anio');
            var currentTime = new Date()
            for (i = currentTime.getFullYear() ; i < 3000; i++) {
                var op = document.createElement('option');
                op.text = i;
                op.value = i;
                select.appendChild(op);
            }
        }

        window.onload = function () {
            obtenerTodosLosTiposDePago();
            crearOpcionesDeCuotas();
            cargarMeses();
            cargarAnios();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id="background">
        <img src="../static/fondo%206.jpg" />
    </div>
       <aside id="lateral"></aside>
    <section id="contenedor">
        <div id="2barra"></div>
        <input type="hidden" id="accion" name="accion" />
         <div id="contenido">
             <span id="sptotal">Total  </span><input type ="text" disabled="disabled" id="total" runat="server" />
             <p />
             <span id="spmetodopago">Método de pago: </span><select id="metodo" name="metodo" ></select>
            <p/> 
            <span id="spnumerotarjeta">Número de tarjeta: </span><input type="text" id="nro" name="nro"/>
             <p/>
             <span id="sptitular">Titular de la tarjeta: </span><input type="text" id="titular" name="titular" />
            <p/>
             <span id="spcvv">CVV: </span><input type="text" id="cvv" name="cvv" />
            <p/>
             <span id="spmes">Mes: </span><select id="mes" name="mes" ></select>
            <p/>
             <span id="spanio">Año: </span><select id="anio" name="anio" ></select>
            <p/>
               <span id="spcantidadcuotas">Cantidad de cuotas: </span><select id="cantCuotas" name="cantCuotas"></select>
             <p />
        <input type="submit" value="checkout" id="checkout" name="checkout" onclick="setAccion(this.id)"/>
        <p />
    </section>
</asp:Content>
