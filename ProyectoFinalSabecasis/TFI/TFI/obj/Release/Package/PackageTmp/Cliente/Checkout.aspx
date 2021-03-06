﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ConsumidorFinal.Master" CodeBehind="Checkout.aspx.vb" Inherits="TFI.Checkout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <script>
       function setId(id) {
           document.getElementById('idProd').value = id;
           setAccion('eliminarBtn');
           __doPostBack();
       }
      
   </script>
      <link href="../static/bootstrap.min.css" rel="stylesheet" />  
    <script src="../scripts/jquery.js"></script>
    <script src="../scripts/bootstrap.min.js"></script>
    <link href="../static/shop-homepage.css" rel="stylesheet" />
    <link href="../static/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="background">
        <img src="../static/fondo11.jpg" />
    </div>
     <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
        <div class="container">
            <div class="navbar-header page-scroll">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="/Cliente/Inicio.aspx">
                    <img class="dooba-img" src="../static/logotipo-trans.png" /></a>
            </div>

            <div class="collapse navbar-collapse" >
                <ul class="nav navbar-nav navbar-right" id="menuHorizontal">
                    <li>
                        <a href="/Cliente/IniciarSesion.aspx" id ="alogin">Iniciar sesión</a>
                    </li>
                    <li>
                        <a href="/Cliente/Inicio.aspx">Inicio</a>
                    </li>
                    <li>
                        <a href="#" data-target="#idioma" data-toggle="modal" id="acambiaridioma">Cambiar idioma</a>
                    </li>
                    <li>
                        <a href="/Cliente/Catalogo.aspx" id="acatalogo" >Catálogo</a>
                    </li>
                    <li>
                        <a href="/Cliente/Ayuda.aspx"  id="aayuda">Ayuda</a>
                    </li>
                </ul>
            </div>
        </div>  
    </nav>
    <section>
        <div class="container" id="content">
        <input type="hidden" id="idProd" name="idProd"/>
        <input type="hidden" id="accion" name="accion" />
        <div class="row">
        <div id="contenido" runat="server">

        </div>
        <input type="submit" value="Comprar" id="comprar" name="comprar" class="btn btn-primary"  onclick="setAccion(this.id)"/>
        </div>
        <div class ="thumbnail row">
        <span id="spsugerencias">Sugerencias</span>
        <div id="sugerencias" runat="server">

        </div>
        </div>
         </div>
    </section>
      
     <div id="idioma" class="modal fade" role="dialog">
        <div id="Div2" class="modal-dialog modal-lg modal-content modal-dooba modal-idioma">
             <button type="button" class="btn btn-default btn-modal-close btn-circle pull-right" data-dismiss="modal"><i class="fa fa-times"></i></button>
             <div class="col-md-3"><span id="sidioma">Idioma: </span></div><select id="idiomaSeleccionado" onchange="seleccionarIdioma();"></select>
        </div>
    </div>
</asp:Content>
