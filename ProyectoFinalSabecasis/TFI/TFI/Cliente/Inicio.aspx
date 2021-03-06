﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ConsumidorFinal.Master" CodeBehind="Inicio.aspx.vb" Inherits="TFI.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />

    <title>Doo-Ba</title>

    <!-- Bootstrap Core CSS -->
    <link href="../static/bootstrap.min.css" rel="stylesheet" />

    <!-- Custom CSS -->
    <link href="../static/Principal.css" rel="stylesheet" />

    <!-- Custom Fonts -->
    <link href="../static/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Montserrat:400,700" rel="stylesheet" type="text/css" />
    <link href='https://fonts.googleapis.com/css?family=Kaushan+Script' rel='stylesheet' type='text/css' />
    <link href='https://fonts.googleapis.com/css?family=Droid+Serif:400,700,400italic,700italic' rel='stylesheet' type='text/css' />
    <link href='https://fonts.googleapis.com/css?family=Roboto+Slab:400,100,300,700' rel='stylesheet' type='text/css' />


    <script src="../scripts/multiidioma.js"></script>

          <!-- jQuery -->
    <script src="../scripts/jquery.js"></script>

    <!-- Plugin JavaScript -->
    <script src="http://cdnjs.cloudflare.com/ajax/libs/jquery-easing/1.3/jquery.easing.min.js"></script>
    <script src="../scripts/classie.js"></script>
    <script src="../scripts/cbpAnimatedHeader.js"></script>
    <link href="../static/half-slider.css" rel="stylesheet"/>

    <!-- Contact Form JavaScript -->
    <script src="../scripts/jqBootstrapValidation.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Navigation -->
    <nav class="navbar navbar-default navbar-fixed-top">
        <div class="container">       
            <!-- Collect the nav links, forms, and other content for toggling -->
            <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                <a class="navbar-brand page-scroll" href="#page-top">
                    <img class="dooba-img" src="../static/logotipo-trans.png" id="logo"/></a>
                <ul class="nav navbar-nav navbar-right" id="menuHorizontal">
                    <li class="hidden">
                        <a href="#page-top"></a>
                    </li>
                    <li>
                        <a  href="/Cliente/IniciarSesion.aspx" id="alogin">Iniciar sesión</a>
                    </li>
                    <li>
                        <a class="page-scroll" href="#servicios">Productos</a>
                    </li>
                    <li>
                        <a class="page-scroll" href="#about">¿Quiénes somos?</a>
                    </li>
                    <li>
                        <a class="page-scroll" href="#opinion">Dejanos tu opinión</a>
                    </li>
                    <li>
                        <a class="page-scroll" href="#contact">Contacto</a>
                    </li>
                    <li>
                        <a href="/Cliente/NovedadesPublicas.aspx" id="aNovedadesPublicas">Novedades</a>
                    </li>
                    <li>
                        <a  href="#" data-target="#idioma" data-toggle="modal" id="acambiaridioma">Cambiar idioma</a>
                    </li>
                </ul>
               <div class="enlinea espaciado-izquierdo-breve espaciado-suerior" style="visibility:hidden"  id="user_tag"><i class="fa fa-user logo-usuario"></i><a href="/Cliente/Perfil.aspx" id="aPerfil" runat="server" class="usuario espaciado-izquierdo-breve"></a> </div>
            </div>
              
        </div>
        <div class="mensaje-respuesta-contacto" id="LblMensaje" runat="server"></div>
         <div id="breadcrums" runat="server" class="breadcrums">

        </div>
              
    </nav>
    
    <!-- Header -->
    <header>

        <div class="container" id="page-top">
            <div class="intro-text">
                <div class="intro-lead-in" id="aBienvenido">Bienvenido a nuestro sitio!</div>
                <div class="intro-heading" id="aExplora">Divertite, explorá, disfrutá.</div>
                <a href="#servicios" class="page-scroll btn btn-xl" id="aConoceMasSobreNosotros">Conocé más sobre nosotros</a>
            </div>
        </div>
    
    </header>

    <section id="servicios">

        <div class="container">
            <div class="row">
                <div class="col-lg-12 text-center">
                    <h2 class="section-heading" id="aServicios">Servicios</h2>
                    <h3 class="section-subheading text-muted">Sentite como en casa, paseá, compartí, divertite.</h3>
                </div>
            </div>
            <div class="row text-center">
                <div class="col-md-4">
                   <a href="/Cliente/Catalogo.aspx">
                     <span class="fa-stack fa-4x">
                        <i class="fa fa-circle fa-stack-2x text-primary"></i>
                        <i class="fa fa-shopping-cart fa-stack-1x fa-inverse"></i>
                    </span>
                    </a>
                    <a href="/Cliente/Catalogo.aspx?id=3" class="service-link"><h4 class="service-heading" id="aEcomerce">E-Commerce</h4></a>
                    <p class="text-muted">Explorá nuestro catálogo de productos</p>
                </div>
                <div class="col-md-4">
                    <span class="fa-stack fa-4x">
                        <i class="fa fa-circle fa-stack-2x text-primary"></i>
                        <i class="fa fa-users fa-stack-1x fa-inverse"></i>
                    </span>
                     <a href="/Cliente/Perfil.aspx" class="service-link"><h4 class="service-heading" id="aredSocial">Red social</h4></a>
                    <p class="text-muted">Gestioná tu usuario y disfruta de interactuar con centenares de músicos dispuestos a compartir sus ideas.</p>
                </div>
                <div class="col-md-4">
                    <span class="fa-stack fa-4x">
                        <i class="fa fa-circle fa-stack-2x text-primary"></i>
                        <i class="fa fa-cogs fa-stack-1x fa-inverse"></i>
                    </span>
                    <a href="/Cliente/Ayuda.aspx" class="service-link"><h4 class="service-heading" id="aAyudaEnLinea">Ayuda en línea</h4></a>
                    <p class="text-muted">Inicia sesión en nuestro módulo de ayuda.</p>
                    <p class="text-muted">Conocé tus movimientos monetarios en el sitio, contactá con nuestros representantes y gestioná tus garantías.</p>
                </div>
            </div>
        </div>
    </section>

    <!-- About Section -->
    <section id="about">
        <div class="container">
            <div class="row">
                <div class="col-lg-12 text-center">
                    <h2 class="section-heading" id="aQuienesSomoss">¿Quiénes somos?</h2>
                    <h3 class="section-subheading text-muted"></h3>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <ul class="timeline">
                        <li>
                            <div class="timeline-image">
                                <img class="img-circle img-responsive" src="../static/buenos-aires.jpg" alt="" />
                            </div>
                            <div class="timeline-panel">
                                <div class="timeline-heading">
                                    <h4>2015</h4>
                                    <h4 class="subheading">Nuestros comienzos</h4>
                                </div>
                                <div class="timeline-body">
                                    <p class="text-muted">En 2015 abrimos nuestras puertas otorgando productos musicales que permitan a nuestros clientes y usuarios comunicar sus instrumentos de forma inalámbrica.</p>
                                    <p class="text-muted">Desde entonces brindamos soluciones tecnológicas de alta calidad desde nuestra sede en Flores, Buenos Aires, Argentina a toda la Republica Argentina.</p>
                                </div>
                            </div>
                        </li>
                        <li class="timeline-inverted">
                            <div class="timeline-image">
                                <img class="img-circle img-responsive" src="../static/musico.jpg" alt="" />
                            </div>
                            <div class="timeline-panel">
                                <div class="timeline-heading">
                                    <h4></h4>
                                    <h4 class="subheading">Nuestra misión</h4>
                                </div>
                                <div class="timeline-body">
                                    <p class="text-muted">Producir y comercializar productos de calidad a nivel nacional, dentro del territorio de Argentina, que permitan interconectar instrumentos con pedaleras, amplificadores y auriculares de forma inalámbrica de la forma más genérica posible, brindando la mejor calidad de interconexión y transmisión de señal sonora a un costo razonable.</p>
                                </div>
                            </div>
                        </li>
                        <li>
                            <div class="timeline-image">
                                <img class="img-circle img-responsive" src="../static/vision.jpg" alt="" />
                            </div>
                            <div class="timeline-panel">
                                <div class="timeline-heading">
                                    <h4></h4>
                                    <h4 class="subheading">Nuestra visión</h4>
                                </div>
                                <div class="timeline-body">
                                    <p class="text-muted">Posicionarse como líder en el mercado argentino de la música con la gama de productos actual. Expandir la gama de productos con el añadido de productos complementarios e introducir al mercado nuevos modelos que resulten versiones mejoradas de los productos actualmente comercializados.</p>
                                    <p class="text-muted">Ser reconocido mundialmente como uno de los fabricantes más destacados de elementos de interconexión inalámbrica de instrumentos.</p>
                                </div>
                            </div>
                        </li>
                        <li class="timeline-inverted">
                            <div class="timeline-image">
                                <img class="img-circle img-responsive" src="../static/productos.jpg" alt="" />
                            </div>
                            <div class="timeline-panel">
                                <div class="timeline-heading">
                                    <h4></h4>
                                    <h4 class="subheading">Nuestos principales productos</h4>
                                </div>
                                <div class="timeline-body">
                                    <ul class="text-muted">
                                        <li>* Transmisores inalámbricos Bluetooth y Wi-Fi para guitarras y bajos.</li>
                                        <li>* Pedaleras inalámbricas Wi-Fi</li>
                                        <li>* Amplificadores inalámbricos valvulares Bluetooth y Wi-Fi.</li>
                                    </ul>

                                </div>
                            </div>
                        </li>
                        <li>
                            <div class="timeline-image">
                                <img class="img-circle img-responsive" src="../static/comunidad.jpg" alt="" />
                            </div>
                            <div class="timeline-panel">
                                <div class="timeline-heading">
                                    <h4></h4>
                                    <h4 class="subheading">Nuestra Comunidad</h4>
                                </div>
                                <div class="timeline-body">
                                    <p class="text-muted">Nuestros productos ofrecen flexibilidad con distorsiones de sonido personalizadas e intercambiables.</p>
                                    <p class="text-muted">En nuestra comunidad podrás compartir tu propio sonido, así como descargar opciones de distorsión que convierten a nuestros productos en los más creativos y flexibles de su tipo.</p>
                                    <p class="text-muted">Nuestra comunidad permite conocer opiniones sobre nuestros productos, así como interambiar ideas y experiencias con otros músicos</p>
                                </div>
                            </div>
                        </li>
                        <li class="timeline-inverted">
                            <div class="timeline-image">
                                <h4>Sé Parte de nuestra historia!</h4>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </section>

    <!-- section opinion -->
    <section id="opinion">
        <div class="container">
            <div class="row">
                <div class="col-lg-12 text-center">
                    <h2 class="section-heading" id="aDejarOpinion">Dejanos tu opinión</h2>
                    <h3 class="section-subheading text-muted" id="aDejarOpinionPie">Completá la encuesta y ayudanos a mejorar nuestros productos y servicios</h3>
                </div>
            </div>
            <div class="row text-center" id="encuestas">
            </div>
        </div>

    </section>

    <!-- Contact Section -->
    <section id="contact">
        <div class="container">
            <div class="row">
                <div class="col-lg-12 text-center" id="cabeceraContacto">
                    <h2 class="section-heading" id="aContactanos">Contactanos</h2>
                    <h3 class="section-subheading text-muted" id="aContactanosPie">Envianos tu mensaje indicando tu dirección de email y te contestaremos a la brevedad o si lo preferís, podés llamarnos al 0800-222-5154, de lunes a viernes de 9:00 a 18:00 horas.</h3>
                    <asp:Label ID="mensajeRespuesta" runat="server" Text="" CssClass="mensaje-respuesta-contacto"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <input type="text" class="form-control" placeholder="Nombre *" id="nombre" name="nombre" required="required" data-validation-required-message="Please enter your name." />
                                <p class="help-block text-danger"></p>
                            </div>
                            <div class="form-group">
                                <input type="email" class="form-control" placeholder="Email *" id="email" name="email" required="required" data-validation-required-message="Please enter your email address." />
                                <p class="help-block text-danger"></p>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <textarea class="form-control" placeholder="Mensaje *" id="mensaje" required="required" name="mensaje" data-validation-required-message="Please enter a message."></textarea>
                                <p class="help-block text-danger"></p>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-lg-12 text-center">
                            <div id="success"></div>
                            <button type="submit" id="enviarBtn" name="enviarBtn" class="btn btn-xl">Enviar</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <footer>
        <div class="container">
            <div class="row">
                <div class="col-md-4">
                    <span class="copyright">Copyright &copy; Sabrina Abecasis 2015</span>
                </div>
                <div class="col-md-4">
                    <ul class="list-inline social-buttons">
                        <li><a href="#"><i class="fa fa-twitter"></i></a>
                        </li>
                        <li><a href="#"><i class="fa fa-facebook"></i></a>
                        </li>
                        <li><a href="#"><i class="fa fa-linkedin"></i></a>
                        </li>
                    </ul>
                </div>
                <div class="col-md-4">
                    <ul class="list-inline quicklinks">
                        <li><a href="#" data-toggle="modal" data-target="#divpoliticas">Política de seguridad</a>
                        </li>
                        <li><a href="#" data-target="#divterminos" data-toggle="modal">Términos y condiciones</a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </footer>
    <div id="divpoliticas" class="modal fade" role="dialog">
        <div id="apoliticaseguridad" class="modal-dialog modal-lg modal-content modal-dooba">
            <button type="button" class="btn btn-default btn-modal-close btn-circle" data-dismiss="modal"><i class="fa fa-times"></i></button>
            <h3>Bienvenidos a www.dooba.com (el “Sitio”) de Doo-ba S.R.L ("Doo-ba").</h3>
            <p>continuación se incluyen las políticas de seguridad de utilización de los servicios ofrecidos en el Sitio (el “Servicio” y las “Políticas de seguridad” respectivamente) que regirán los derechos y obligaciones entre los usuarios (los “Usuarios” y/o el “Usuario”) y Doo-ba. Doo-ba recomienda especialmente guardar una copia impresa de las Condiciones de Uso Al ingresar en el Sitio, el Usuario declara saber, conocer y aceptar las presentes Políticas de seguridad. </p>

            <h4>Medios de Pago:</h4>
            <p>
                El Sitio cuenta con los siguientes medios de pago válidos para compras online: - Visa, Master Card y American Express. Es derecho de Doo-ba la inclusión, exclusión o suspensión de cualquier medio de pago. Tipo de comprobantes a emitir. En venta on line, por el momento sólo se emitirán facturas electrónicas a Consumidor Final (Comprobantes tipo “B”). La misma será remitida al e-mail informado en el pedido, sin excepción. En caso de no recibirla, contactarse al Centro de Atención al Cliente al teléfono (011) 4588-7000 para corroborar un posible error en la carga del e-mail en el pedido. Operatividad y Seguridad del Sitio. El Usuario se compromete a no acceder ni a intentar acceder al Sitio ni usar de ninguno de los Servicios por ningún otro medio que no sea la interfaz provista por Doo-ba. Asimismo, se compromete a no involucrarse en ninguna actividad que interfiera o que interrumpa o que tuviere entidad suficiente para interferir o interrumpir la prestación de los Servicios del Sitio o los servidores y redes conectados al mismo. 
Está prohibido a los Usuarios violar, vulnerar y/o de cualquier forma afectar el normal uso y la seguridad del Sitio, incluyendo, pero sin limitarse a:
            </p>
            <p>a) acceder a datos que no se destinan a ese Usuario o ingresar a una computadora o a una cuenta a los cuales no esté el Usuario autorizado para acceder; </p>
            <p>b) tratar de sondear, analizar o probar la vulnerabilidad de un sistema o una red o romper medidas de seguridad o autenticación sin la debida autorización; </p>
            <p>
                c) intentar interferir con el servicio a cualquier Usuario, computadora o red, incluyendo, sin que la siguiente enumeración implique limitación, cuando se lo haga a través de medios de envío de un virus al Sitio, sobrecarga, inundación ("flooding") envío masivo de mensajes no solicitados ("spamming") envío de códigos destructivos ("mailbombing") o anulación de instrucciones del software ("crashing”). 
Las violaciones de la seguridad del sistema o de la red pueden dar lugar a responsabilidad civil o penal. Doo-ba investigará sucesos que puedan implicar esas violaciones y puede involucrar a cooperar con las autoridades encargadas de hacer cumplir la ley para enjuiciar a los usuarios que participen en esas violaciones. Usted acepta no utilizar ningún dispositivo, software o rutina para interferir o intentar interferir con el buen funcionamiento de este Sitio o cualquier actividad que se lleve a cabo en este Sitio. Usted acepta, además, no utilizar ni intentar utilizar cualquier motor, software, herramienta, agente u otro dispositivo o mecanismo (incluyendo sin limitación los navegadores, arañas (spyders), robots y avatares) para navegar o buscar en este Sitio, que no sean los motores de búsqueda y agentes de búsqueda que Doo-ba hace disponibles en este Sitio y diferentes de los navegadores Web de terceros disponibles para el público en general (por ejemplo, Mozilla Firefox, Internet Explorer, Opera, Google Chrome, entre otros). El Usuario y/o el navegante ocasional del Sitio aceptan y reconocen expresamente que cualquier hecho, acto u omisión vinculadas con lo referido anteriormente y que tengan entidad para afectar la seguridad y/o el normal desarrollo del Sitio tienen entidad suficiente para causar a Doo-ba considerables daños y perjuicios incluyendo pero no limitándose a lucro cesante por caída y/o merma en las ventas, afectación de sus marcas, imagen comercial, pérdida de clientela, etc.
            </p>

            <h4>Contraseñas y seguridad de la cuenta del Usuario: </h4>
            <p>Por medio de la aceptación de las Condiciones de Uso el Usuario, reconoce y acepta que es el único responsable de mantener la confidencialidad de su contraseña. En consecuencia, el Usuario acepta que será el único responsable ante Doo-ba de todas y cada una de las actividades que se efectúen en su cuenta en el Sitio. El Usuario deberá notificar de inmediato a Doo-ba cualquier uso no autorizado de su contraseña o cuenta de que tenga conocimiento, a través del Sitio. Esta notificación deberá efectuarse a través del Sitio y, adicionalmente, remitiendo una nota al domicilio de Doo-ba debiendo especificar en ambos casos y en detalle toda la información con la que cuente respecto del uso no autorizado de su contraseña.</p>

            <h4>Datos Personales:</h4>
            <p>Al registrarse como Usuario de este Sitio, suministrará información que, a criterio de Doo-ba, resulta necesaria para brindar un mejor servicio. Los datos personales del Usuario se toman con los fines de cumplir con el giro comercial de Doo-ba, realizar acciones de marketing y publicidad. Para el caso que el Usuario decida proporcionar sus datos, se consiente expresamente que Doo-ba podrá utilizarlos con fines operativos del sistema de comercio electrónico en este Sitio y/o en otros sitios Web pertenecientes a Doo-ba e incluso para hacerle llegar por distintos medios ofertas de mercaderías o servicios, anuncios de promociones, o publicidad. Al comunicarnos sus datos personales por medio de su registro en el Sitio, usted presta el consentimiento previsto por la ley argentina Nº 25.236, incluyendo la posibilidad de cesión prevista en su art. 11 que usted podrá revocar en cualquier momento simplemente solicitándolo vía correo electrónico y/o mediante cualquier otra forma que pueda ser fácilmente acreditable. El Sitio tiene una estricta política de privacidad y protección de los datos generados por la actividad en línea de sus usuarios. Doo-ba no entregará ningún tipo de datos personales ni de información individualizada sobre ninguno de sus Usuarios a ningún tercero, sea en forma gratuita u onerosa, incluyendo cualquier dato relacionado con la identidad de sus usuarios, sus hábitos, costumbres, ámbitos de pertenencia, hábitos de consumo, etc. Usted se notifica y autoriza a Doo-ba para analizar la información relacionada con sus usuarios como grupo, y basándose en dichos análisis ofrecer, individualmente o en conjunto con terceros, bienes y servicios a sus usuarios. Doo-ba está autorizado a compartir con terceros la información despersonalizada relacionada con sus usuarios. El Usuario, en cuanto titular de los datos personales, tiene la facultad de ejercer el derecho de acceso a los mismos en forma gratuita a intervalos no inferiores a seis meses, salvo que se acredite un interés legítimo al efecto conforme lo establecido al artículo 14, inciso 3 de la Ley n° 25.326. Doo-ba aconseja a sus clientes que mantengan actualizados sus datos. De esta forma recibirán en forma periódica ofertas y promociones diseñadas a medida de sus necesidades. Si desea actualizar sus datos dirijase a: datospersonales@Doo-ba.com.ar o mediante el ejercicio del derecho de acceso previsto por el art. 14 de la Ley 25.326. Doo-ba no solicita datos de sus clientes, ni en forma telefónica ni a través del correo electrónico, más que en los casos estipulados en el punto “Aceptación de Pedidos”. De existir un contacto, tiene como finalidad la realización de alguna encuesta de satisfacción o comunicar alguna promoción o beneficio especial. Doo-ba se encuentra inscripto en el Registro Nacional de la Dirección Nacional de Protección de Datos Personales, dependiente del Ministerio de Justicia y Derechos humanos tal como lo establece la ley. Si Ud. desea conocer más en profundidad sobre este tema haga click aquí o en la siguiente dirección: http://www.jus.gov.ar/dnpdp La Dirección Nacional De Protección De Datos Personales, Órgano de Control de la Ley n° 25.326, tiene la atribución de atender las denuncias y reclamos que se interpongan con relación al incumplimiento de las normas sobre protección de datos personales permite. Si Ud. desea conocer más en profundidad sobre este tema haga click aquí o en la siguiente dirección: http://www.jus.gov.ar/dnpdp </p>

            <p>Doo-ba. Todos los derechos reservados.</p>
        </div>
    </div>
   
    <div id="divterminos" class="modal fade" role="dialog">
        <div id="aterminos" class="modal-dialog modal-lg modal-content modal-dooba">
            <button type="button" class="btn btn-default btn-modal-close btn-circle" data-dismiss="modal"><i class="fa fa-times"></i></button>
            <h3>Bienvenidos a www.dooba.com (el “Sitio”) de Doo-ba S.R.L ("Doo-ba").</h3>
            <p>
                A continuación se incluyen las condiciones generales de utilización de los servicios ofrecidos en el Sitio (el “Servicio” y las “Condiciones de Uso” respectivamente) que regirán los derechos y obligaciones entre los usuarios (los “Usuarios” y/o el “Usuario”) y Doo-ba. Doo-ba recomienda especialmente guardar una copia impresa de las Condiciones de Uso Al ingresar en el Sitio, el Usuario declara saber, conocer y aceptar las presentes Condiciones de Uso. Las Condiciones de Uso, así como sus modificaciones, estarán vigentes en forma inmediata a su publicación en el Sitio.
La utilización del Sitio implica el conocimiento y la aceptación plena por parte del Usuario de las Condiciones de Uso. En caso de no aceptar en las Condiciones de Uso o cualquier cambio o modificación de las mismas no deberá continuar utilizando el Sitio.
            </p>
            <h4>Naturaleza del Servicio y destino del Sitio: </h4>
            <p>El Sitio ha sido programado para que constituya un medio virtual para el acceso de los Usuarios a las góndolas de la tienda de Doo-ba del área de su domicilio. </p>

            <h4>Usuarios que pueden utilizar los Servicios del Sitio:</h4>
            <p>Los Servicios sólo están disponibles para personas que tengan capacidad legal para contratar. No podrán utilizar los Servicios las personas que no tengan esa capacidad, los menores de edad o quien registre como Usuario una persona jurídica, deberá tener capacidad para contratar a nombre de tal entidad y de obligar a la misma en los términos de estas Condiciones de Uso. </p>

            <h4>Uso de los Servicios:</h4>
            <p>El Usuario, ya sea registrado o “invitado”, declara que la información que brindará al remitir el pedido de registración será precisa, correcta y actual comprometiéndose a informar en forma inmediata y fehaciente respecto de cualquier cambio siendo a su vez enteramente responsable frente a Doo-ba por los daños y perjuicios que el incumplimiento de dicha obligación pudiere acarrearle incluyendo pero no limitándose a costos de ubicación física del Usuario, costos de intimaciones y citaciones, etc. El Usuario acepta que utilizará los Servicios exclusivamente con los fines estipulados en (a) las Condiciones de Uso y (b) cualquier norma o regulación aplicable ya sea de índole municipal, provincial o nacional incluyendo pero no limitándose a leyes, decretos, ordenanzas, resoluciones, directivas, etc. El Usuario se compromete a no divulgar su contraseña a terceros que no estuvieren autorizados por el Usuario para acceder a los Servicios del Sitio. </p>

            <h4>Ámbito de aplicación:</h4>
            <p>Estas Condiciones de Uso son aplicables a cualquiera de todas las ventas de productos y/o servicios a ser realizadas por Doo-ba dentro de la República Argentina. La realización de un pedido supone la aceptación expresa del Usuario a las presentes Condiciones de Uso. Al aceptar estas Condiciones de Uso, registrándose como Usuario o ingresando como “invitado”, usted declara bajo juramento y certifica tener 18 años de edad o más. Si usted no está de acuerdo (o no puede cumplir) con cualquiera de las reglas de las Condiciones de Uso, no utilice el Sitio. Toda la información proporcionada al momento de operar con el Sitio debe ser precisa y veraz. Proporcionar información inexacta o falsa constituye una violación grave de estas Condiciones de Uso. Confirmando su pedido en el final del proceso de una operación, el Usuario acuerda aceptar los artículos adquiridos y pagar su precio, perfeccionando con Doo-ba un contrato de compra-venta de mercadería. </p>

            <h4>Referencias a terceros:</h4>
            <p>Las referencias en el Sitio a nombres, marcas, productos o servicios de terceros, o vínculos de hipertexto a sitios Web o información de terceros se proveen únicamente como una comodidad para el Usuario y de ninguna forma constituyen o implican respaldo, patrocinio o recomendación por Doo-ba respecto de la tercera parte, su información, productos o servicios. Doo-ba no tiene control de las prácticas o políticas de esos terceros, ni del contenido de los sitios Web de cualesquiera terceros y no hace ninguna declaración o promesa con respecto a productos o servicios de terceros, o respecto del contenido o la exactitud de cualquier material alojado en dichos sitios de terceros. Si el Usuario decide navegar o seguir un vínculo hacia cualquiera de tales sitios Web de terceros, lo hace enteramente bajo su propio riesgo y responsabilidad.</p>

            <h4>Aceptación de pedidos:</h4>
            <p>Por favor, tenga en cuenta que puede ocurrir que luego de aprobado por el Usuario un pedido, por distintas razones Doo-ba no esté en condiciones de aceptarlo total o parcialmente y deba por ello cancelar o excluir del mismo algunos ítems pedidos por el Usuario. Algunas situaciones pueden dar lugar a que algunos ítems de su pedido sean excluidos del mismo, ya sea debido a limitaciones en las cantidades disponibles para la compra; faltantes de mercadería en la respectiva góndola; falta de autorización a la operación por parte de la entidad emisora o administradora de su tarjeta de crédito; inexactitudes o errores en el producto o información; o problemas identificados por el departamento de prevención de fraude de crédito y de precios. Para su seguridad, le informamos que eventualmente podemos requerirle información o realizar verificaciones adicionales antes de aceptar cualquier pedido. Si se cancela todo o parte de su pedido o si se necesita información adicional para aceptar su pedido, nos pondremos en contacto con usted. En todos los casos que deban realizarse una modificación o cancelación parcial de su pedido, el Usuario tendrá siempre derecho a cancelar la totalidad del mismo. Doo-ba, en uso de sus facultades, se reserva el derecho de anular definitivamente cualquier operación luego de la constatación de los datos ingresados, en cuyo caso se le comunicará al usuario la decisión. Todas las compras efectuadas estarán sujetas al stock informado, debido a las demoras propias de las actualizaciones del sistema, es posible que el mismo le permita realizar una compra que luego deba ser anulada por falta de stock. En ese caso le será informado en el e-mail confirmatorio. Forma de entrega y pago.</p>
            <p>Una vez que un pedido haya quedado confirmado y el importe de su factura aprobado, salvo que existan inconvenientes excepcionales para la aceptación del pedido por parte de Doo-ba, que le serán debidamente comunicados, recibirá un correo electrónico en el plazo de las 24 horas confirmando que la operación ha sido exitosa. Si el Usuario no recibe tal notificación vía correo electrónico deberá contactarse con el Centro de Atención al Cliente Doo-ba a fin de constatar que no hubo errores de registro al ingresar la casilla de correo. Actualmente el Usuario dispone dos métodos de entrega de la mercadería: Envío a domicilio. Retira en Sucursal. Cuando el método de entrega seleccionado sea “Envío a domicilio”, la compra deberá ser recepcionada preferentemente por el titular del método de pago utilizado, o bien por una persona autorizada que cumpla con las Condiciones de Uso del Sitio. El Usuario podrá designar una persona autorizada a recibir la mercadería y/o servicio adquirido en el domicilio de entrega indicado únicamente para la modalidad “Envío a domicilio”. En todos los casos, para que la mercadería y/o servicio sea entregado, la persona que recepciona deberá exhibir su DNI. En el caso de operaciones de “Retira en Sucursal” deberá seguir los pasos que se detallan a continuación: Elija la sucursal de retiro. Recomendamos la más cercana a su domicilio. Recibirá, dentro de las siguientes setenta y dos (72) horas, un e-mail de confirmación indicando que ya puede retirar el pedido. Una vez que reciba el e-mail de confirmación de su compra y habilitación para retirar el/los producto/s adquirido/s, podrá dirigirse, dentro de los treinta (30) días corridos, a la sucursal que haya seleccionado, con el comprobante de la compra efectuada impreso o exhibido en su dispositivo móvil, y retirar su pedido. Cuando se presente en la sucursal, deberá dirigirse al sector de Cajas, donde deberá presentar: DNI y tarjeta con la que realizó el pago (ambos en original). La entrega sólo se hará efectiva al titular del medio de pago, sin excepción. Mail de confirmación de compra, que incluye el número de pedido: NOTA: para operaciones de sábados, domingos y feriados de 17 a 22 Hs. los pedidos podrán ser retirados el día siguiente, una vez que reciba el e-mail de confirmación de su compra. En cualquiera de las modalidades de entrega, se le exhibirá al Usuario el estado de la mercadería y los accesorios que vienen con esta debiendo el Usuario (o la persona autorizada por este, en su caso) verificar que la mercadería entregada corresponde a los artículos incluidos en el pedido y en el remito que se le presentará en el acto de entrega. La firma del remito y/o la del comprobante de pago implicará declaración de conformidad con la mercadería entregada, sin perjuicio de los derechos que le pudieren corresponder en caso de que la misma resultara por algún motivo defectuosa. </p>

            <h4>Costo del servicio de entrega a domicilio y casos de imposibilidad de entrega:</h4>
            <p>El servicio de entrega a domicilio se encuentra habilitado en determinadas localidades del país, las cuales podrán ser consultadas durante el proceso de compra online, momento en el cual el Usuario procederá a seleccionar según su conveniencia. El servicio de entrega a domicilio se efectuará dentro de un margen mínimo de 72 horas (que podrá extenderse hasta 10 días hábiles en función de la ubicación del domicilio de entrega informado por el Usuario) y tendrá un costo, que aparecerá expresado y diferenciado en la etapa del proceso de llenado del carrito previa a la emisión de su pedido. Dicho costo se añadirá al de las mercaderías detalladas en la correspondiente factura electrónica. La confirmación del pedido y el pago realizado on line, implicarán la aceptación del Usuario del costo del servicio de entrega a domicilio y consecuentemente también el monto total de la correspondiente factura electrónica que recibirá en el e-mail registrado en el pedido. En todos los casos, la fecha de entrega será comunicada al Usuario en el e-mail que registró en el pedido. En caso de que en el día de entrega no encontremos en el domicilio un titular habilitado para la recepción (sea el Usuario y/o a la persona autorizada por este en su caso), se volverá a coordinar otra fecha de entrega para lo cual el cliente puede comunicarse al Centro de Atención al Cliente, (011) 4588-7000. En caso de que por dificultades imprevistas Doo-ba no pudiere cumplir con la entrega a domicilio, según los plazos por zona geográfica, se le comunicará en no más de cinco (5) días hábiles y se pactará la entrega de la mercadería para un nuevo día y horario a convenir, sin que se devengue por ello ningún cargo adicional para el Usuario. Cualquier plazo pactado de entrega informado es estimado, aclarándosele que pueden verificarse demoras en la entrega del producto derivadas de un sinnúmero de variables ajenas al control de Doo-ba.</p>

            <h4>Medios de Pago:</h4>
            <p>El Sitio cuenta con los siguientes medios de pago válidos para compras online: - Visa, Master Card, American Express, Mercado Pago, Paypal. Es derecho de Doo-ba la inclusión, exclusión o suspensión de cualquier medio de pago. Tipo de comprobantes a emitir. En venta on line, por el momento sólo se emitirán facturas electrónicas a Consumidor Final (Comprobantes tipo “B”). La misma será remitida al e-mail informado en el pedido, sin excepción. En caso de no recibirla, contactarse al Centro de Atención al Cliente al teléfono (011) 4588-7000 para corroborar un posible error en la carga del e-mail en el pedido. Operatividad y Seguridad del Sitio. El Usuario se compromete a no acceder ni a intentar acceder al Sitio ni usar de ninguno de los Servicios por ningún otro medio que no sea la interfaz provista por Doo-ba. Asimismo, se compromete a no involucrarse en ninguna actividad que interfiera o que interrumpa o que tuviere entidad suficiente para interferir o interrumpir la prestación de los Servicios del Sitio o los servidores y redes conectados al mismo. </p>
            <p>Está prohibido a los Usuarios violar, vulnerar y/o de cualquier forma afectar el normal uso y la seguridad del Sitio, incluyendo, pero sin limitarse a: </p>
            <p>a) acceder a datos que no se destinan a ese Usuario o ingresar a una computadora o a una cuenta a los cuales no esté el Usuario autorizado para acceder; </p>
            <p>b) tratar de sondear, analizar o probar la vulnerabilidad de un sistema o una red o romper medidas de seguridad o autenticación sin la debida autorización; </p>
            <p>
                c) intentar interferir con el servicio a cualquier Usuario, computadora o red, incluyendo, sin que la siguiente enumeración implique limitación, cuando se lo haga a través de medios de envío de un virus al Sitio, sobrecarga, inundación ("flooding") envío masivo de mensajes no solicitados ("spamming") envío de códigos destructivos ("mailbombing") o anulación de instrucciones del software ("crashing”). 
Las violaciones de la seguridad del sistema o de la red pueden dar lugar a responsabilidad civil o penal. Doo-ba investigará sucesos que puedan implicar esas violaciones y puede involucrar a cooperar con las autoridades encargadas de hacer cumplir la ley para enjuiciar a los usuarios que participen en esas violaciones. Usted acepta no utilizar ningún dispositivo, software o rutina para interferir o intentar interferir con el buen funcionamiento de este Sitio o cualquier actividad que se lleve a cabo en este Sitio. Usted acepta, además, no utilizar ni intentar utilizar cualquier motor, software, herramienta, agente u otro dispositivo o mecanismo (incluyendo sin limitación los navegadores, arañas (spyders), robots y avatares) para navegar o buscar en este Sitio, que no sean los motores de búsqueda y agentes de búsqueda que Doo-ba hace disponibles en este Sitio y diferentes de los navegadores Web de terceros disponibles para el público en general (por ejemplo, Mozilla Firefox, Internet Explorer, Opera, Google Chrome, entre otros). El Usuario y/o el navegante ocasional del Sitio aceptan y reconocen expresamente que cualquier hecho, acto u omisión vinculadas con lo referido anteriormente y que tengan entidad para afectar la seguridad y/o el normal desarrollo del Sitio tienen entidad suficiente para causar a Doo-ba considerables daños y perjuicios incluyendo pero no limitándose a lucro cesante por caída y/o merma en las ventas, afectación de sus marcas, imagen comercial, pérdida de clientela, etc.
            </p>

            <h4>Contraseñas y seguridad de la cuenta del Usuario: </h4>
            <p>Por medio de la aceptación de las Condiciones de Uso el Usuario, reconoce y acepta que es el único responsable de mantener la confidencialidad de su contraseña. En consecuencia, el Usuario acepta que será el único responsable ante Doo-ba de todas y cada una de las actividades que se efectúen en su cuenta en el Sitio. El Usuario deberá notificar de inmediato a Doo-ba cualquier uso no autorizado de su contraseña o cuenta de que tenga conocimiento, a través del Sitio. Esta notificación deberá efectuarse a través del Sitio y, adicionalmente, remitiendo una nota al domicilio de Doo-ba debiendo especificar en ambos casos y en detalle toda la información con la que cuente respecto del uso no autorizado de su contraseña.</p>

            <h4>Datos Personales:</h4>
            <p>Al registrarse como Usuario de este Sitio, suministrará información que, a criterio de Doo-ba, resulta necesaria para brindar un mejor servicio. Los datos personales del Usuario se toman con los fines de cumplir con el giro comercial de Doo-ba, realizar acciones de marketing y publicidad. Para el caso que el Usuario decida proporcionar sus datos, se consiente expresamente que Doo-ba podrá utilizarlos con fines operativos del sistema de comercio electrónico en este Sitio y/o en otros sitios Web pertenecientes a Doo-ba e incluso para hacerle llegar por distintos medios ofertas de mercaderías o servicios, anuncios de promociones, o publicidad. Al comunicarnos sus datos personales por medio de su registro en el Sitio, usted presta el consentimiento previsto por la ley argentina Nº 25.236, incluyendo la posibilidad de cesión prevista en su art. 11 que usted podrá revocar en cualquier momento simplemente solicitándolo vía correo electrónico y/o mediante cualquier otra forma que pueda ser fácilmente acreditable. El Sitio tiene una estricta política de privacidad y protección de los datos generados por la actividad en línea de sus usuarios. Doo-ba no entregará ningún tipo de datos personales ni de información individualizada sobre ninguno de sus Usuarios a ningún tercero, sea en forma gratuita u onerosa, incluyendo cualquier dato relacionado con la identidad de sus usuarios, sus hábitos, costumbres, ámbitos de pertenencia, hábitos de consumo, etc. Usted se notifica y autoriza a Doo-ba para analizar la información relacionada con sus usuarios como grupo, y basándose en dichos análisis ofrecer, individualmente o en conjunto con terceros, bienes y servicios a sus usuarios. Doo-ba está autorizado a compartir con terceros la información despersonalizada relacionada con sus usuarios. El Usuario, en cuanto titular de los datos personales, tiene la facultad de ejercer el derecho de acceso a los mismos en forma gratuita a intervalos no inferiores a seis meses, salvo que se acredite un interés legítimo al efecto conforme lo establecido al artículo 14, inciso 3 de la Ley n° 25.326. Doo-ba aconseja a sus clientes que mantengan actualizados sus datos. De esta forma recibirán en forma periódica ofertas y promociones diseñadas a medida de sus necesidades. Si desea actualizar sus datos dirijase a: datospersonales@Doo-ba.com.ar o mediante el ejercicio del derecho de acceso previsto por el art. 14 de la Ley 25.326. Doo-ba no solicita datos de sus clientes, ni en forma telefónica ni a través del correo electrónico, más que en los casos estipulados en el punto “Aceptación de Pedidos”. De existir un contacto, tiene como finalidad la realización de alguna encuesta de satisfacción o comunicar alguna promoción o beneficio especial. Doo-ba se encuentra inscripto en el Registro Nacional de la Dirección Nacional de Protección de Datos Personales, dependiente del Ministerio de Justicia y Derechos humanos tal como lo establece la ley. Si Ud. desea conocer más en profundidad sobre este tema haga click aquí o en la siguiente dirección: http://www.jus.gov.ar/dnpdp La Dirección Nacional De Protección De Datos Personales, Órgano de Control de la Ley n° 25.326, tiene la atribución de atender las denuncias y reclamos que se interpongan con relación al incumplimiento de las normas sobre protección de datos personales permite. Si Ud. desea conocer más en profundidad sobre este tema haga click aquí o en la siguiente dirección: http://www.jus.gov.ar/dnpdp </p>

            <h4>Precios:</h4>
            <p>Dado que el propósito del Sitio es permitir a los Usuarios realizar compras online, sin concurrir personalmente a las sucursales, los precios de cada artículo deberán ser los que el Usuario observaría en caso de recorrer las góndolas de dicha sucursal el día del armado del pedido. No obstante que Doo-ba se esfuerza por proporcionar información precisa sobre esos precios, pueden ocurrir errores. En el caso de que un artículo figure en las listas del Sitio a un precio incorrecto debido a un error, Doo-ba tendrá el derecho, de corregir la orden facturando el precio vigente al momento del armado del pedido en la tienda o bien, de anular el pedido reintegrando al Usuario los importes abonados. En este caso, se dará aviso del error al Usuario, y con él la posibilidad de cancelar o modificar la compra del producto facturado u ofrecido de forma incorrecta. Promociones. Las promociones que se ofrezcan en el Sitio no son necesariamente las mismas que ofrezcan otros canales de venta utilizados por Doo-ba, tales como sucursales físicas, Venta Telefónica, catálogos u otros, a menos que se señale expresamente en este Sitio o en la publicidad que se realice para cada promoción. En los casos que el Sitio contenga promociones que consistan en la entrega gratuita o rebajada de un producto por la compra de otro, entonces el despacho del bien que se entregue gratuitamente o a precio rebajado, se hará en el mismo lugar en el cual se despacha el producto comprado. No se podrá participar en estas promociones sin adquirir conjuntamente todos los productos comprendidos en ellas. Salvo indicación en contrario o aprobación por parte de Doo-ba las promociones no son acumulativas unas con otras. Descripciones e imágenes de la mercadería. Doo-ba intenta ser lo más preciso posible en sus descripciones de productos. Sin embargo, es posible que las descripciones de los productos u otros contenidos de este Sitio no sean exactos o contengan errores. Sin perjuicio de lo dispuesto en el art. 34 de la ley 24.240, en caso de existencia de error entre la imagen publicitada de un producto y el producto adquirido, el Usuario tendrá derecho a cancelar el pedido hasta 15 días corridos de que hubiere sido entregado sin recibir observación alguna por parte del Usuario. En caso de cancelación del pedido una vez recibido el producto el Usuario fundándose en inexactitudes en la descripción o imágenes del producto, el Usuario deberá reintegrar el mismo en perfecto estado, conjuntamente con todo su empaque original y todos sus accesorios. La información sobre características de los productos, a la que se accede a través de este Sitio se obtiene de afirmaciones hechas por el fabricante de cada producto. Por favor tenga en cuenta que, en ocasiones los fabricantes pueden alterar sus empaques y etiquetas, de manera que el verdadero embalaje del producto y materiales adjuntos pueden contener información diferente de la que se muestra en el Sitio. Doo-ba recomienda especialmente que adicionalmente a la información presentada en línea lea las etiquetas, advertencias e instrucciones antes de adquirir, utilizar o en cualquier forma consumir un producto. Para obtener información adicional acerca de un producto, por favor póngase en contacto con el fabricante, distribuidor o importador.</p>

            <h4>Publicidad:</h4>
            <p>
                Doo-ba se reserva el derecho de incluir en el Sitio anuncios publicitarios, que se identificarán a los efectos de ser distinguirlos de las informaciones acerca de descripción y precio de los artículos ofrecidos. Esas inclusiones publicitarias se dirigen únicamente a llamar la atención de los Usuarios sobre determinadas ofertas y no reemplazan las descripciones de los productos hechas en el Sitio correspondiente al artículo, que el Usuario deberá utilizar para evaluar y realizar su compra online. Límites de cantidad y ventas a distribuidores. Doo-ba se reserva el derecho, cuando tenga razones que así lo justifiquen, de limitar la cantidad de artículos comprados por persona, por hogar o por pedido. Estas restricciones pueden ser aplicables a los pedidos realizados por la misma cuenta o la misma tarjeta de crédito y también a los pedidos que utilizan la misma dirección de facturación y/o de entrega. Derechos de autor y marcas registradas. A menos que se indique lo contrario, los derechos de autor, marcas comerciales, presentación comercial (trade dress) y/u otros derechos de propiedad intelectual sobre todo y cualquier contenido del Sitio son de propiedad, o controlados o licenciados, de Doo-ba y están protegidos por las leyes Argentinas y tratados internacionales sobre propiedad intelectual. La compilación (es decir, la colección, secuencia, estructura, organización y montaje) de todo el contenido de este Sitio es propiedad exclusiva de Doo-ba y también está protegida por las leyes Argentinas y tratados internacionales sobre propiedad intelectual. Doo-ba y/o sus proveedores y/o licenciantes se reservan expresamente todos los derechos de propiedad intelectual sobre todos los textos, productos, procesos, tecnología, contenido y otros materiales que aparecen en este Sitio. El acceso a este Sitio no confiere a nadie ninguna licencia respecto de cualquier derecho de propiedad intelectual de Doo-ba o de cualquier tercero, salvo que conste con una autorización expresa de parte de Doo-ba. Los nombres y logotipos de Doo-ba y todos los productos relacionados y nombres de servicio, marcas de diseño y eslóganes son las marcas comerciales o marcas de servicio de Doo-ba. Todas las demás marcas son propiedad de sus respectivos registrantes. No se concede ninguna licencia de marca registrada o marca de servicio en relación con los materiales contenidos en este Sitio. El acceso a este Sitio no autoriza a nadie a utilizar cualquier nombre, logotipo o marca de cualquier manera.
Listas de Regalos. Las bases a condiciones del micrositio de Listas de Regalos,  están disponibles en los siguientes links: - Teens & 15 Años: http://listas.Doo-ba.com/teens/bases.php - Casamiento: http://listas.Doo-ba.com/casamiento/bases.php - Viajes: http://listas.Doo-ba.com/viajes/bases.php - 1er Hogar: http://listas.Doo-ba.com/primer-hogar/bases.php Todo lo que no esté expresamente regulado por las bases particulares antes referidas se regirá por las presentes Condiciones de Uso. Prohibiciones al Usuario y sus responsabilidades. Está terminantemente prohibido al Usuario explotar de cualquier forma las informaciones adquiridas por medio del Sitio. No podrá reproducir los textos o imágenes de los anuncios para otros fines que los de su propio recordatorio personal. El Usuario se compromete a tomar a su cargo cualquier responsabilidad contractual o extracontractual que derive de actos como Usuario del Sitio y acepta mantener indemne a Doo-ba, respecto de y contra cualquier reclamo por parte de terceros, derivado o relacionado con el uso inadecuado del Sitio o por la violación de las presentes Condiciones de Uso y sus respectivas modificaciones, o que surja de dicho uso y/o a causa de algún comentario publicado por el Usuario en el Sitio.
            </p>

            <h4>Terminación:</h4>
            <p>Estas Condiciones de Uso implican un contrato que entrará en vigor tan pronto el Usuario acepte las Condiciones de Uso y/o use los servicios del Sitio y permanecerán vigentes hasta que el presente contrato sea cancelado sea por el Usuario o por Doo-ba. El Usuario puede rescindir este contrato en cualquier momento, siempre que lo haga para el futuro evitando el uso de este Sitio y/o siendo Usuario Registrado, renunciando a su registro siguiendo el procedimiento especialmente previsto para ello. Doo-ba también puede rescindir este contrato en cualquier momento siendo válida la notificación de tal rescisión a los domicilios reales o electrónicos que el Usuario tuviere registrados en el Sitio. Asimismo Doo-ba podrá cancelar sin previo aviso la condición de Usuario, y en consecuencia denegar el acceso a los Servicios del Sitio o a comprar por esta vía, si el Usuario no cumpliera con cualquier término o disposición de las presentes Condiciones de Uso. Esta cláusula se aplicará sea o no el Usuario un navegante registrado. Doo-ba podrá en cualquier momento, temporal o permanentemente dar de baja este Sitio. Modificaciones en las Condiciones de Uso. En caso de que estas Condiciones de Uso sean modificadas, tales modificaciones serán publicadas en el Sitio, obrando en él la versión vigente al momento del inicio de cada sesión. Toda vez que por tratarse de un sitio Web abierto a los clientes en general, Doo-ba carece de posibilidad de notificarle particularmente a cada uno de ellos, salvo mediante la publicación en el propio Sitio, por lo que el Usuario se compromete a verificar en forma las Condiciones de Uso con cualquier uso que haga del mismo, entendiéndose que al iniciar cada sesión acepta las que estén vigentes y publicadas en ese momento en la forma y con los efectos establecidos en las presentes Condiciones de Uso.</p>

            <h4>General:</h4>
            <p>Las Condiciones de Uso representan el acuerdo completo entre las partes y sustituyen a todos los acuerdos anteriores que pudieran existir entre ellas. Los títulos utilizados en estas Condiciones de Uso son sólo con fines de referencia y en ninguna manera definen o limitan el alcance de la disposición que titulan. Si cualquier disposición de las mismas se considerara inaplicable por cualquier razón, tal disposición deberá reformarse sólo en la medida necesaria para hacerla exigible y las demás condiciones del presente Acuerdo permanecerán en pleno vigor y efecto. La inacción de Doo-ba con respecto a un incumplimiento de este acuerdo por el Usuario o por otros no constituye una renuncia y no limitará los derechos de Doo-ba con respecto a dicho incumplimiento o infracciones posteriores. Ley aplicable, resolución de controversias, medidas procesales y notificaciones. Este contrato será gobernado por y se interpretará según la legislación vigente en la República Argentina. Cualquier conflicto relacionado con este contrato o con el uso que el Usuario haga de este Sitio será resuelto por los tribunales ordinarios competentes según la legislación vigente y aplicable a la relación de consumo existente entre las partes. En caso de que dicha legislación no defina una competencia específica, será competente la justicia nacional ordinaria en asuntos comerciales con asiento en la Ciudad Autónoma de Buenos Aires, siendo aplicable esta disposición aunque el Usuario estuviera realmente domiciliado fuera de los límites de la Ciudad Autónoma de Buenos Aires o de la República Argentina, por entenderse que este lugar ha sido el lugar de celebración del presente contrato. Salvo que lo contrario haya sido acordado previamente y por escrito firmado entre el Usuario y Doo-ba, todas las notificaciones que se hagan a los usuarios en relación a las presentes Condiciones de Uso se publicarán en el Sitio y tendrán efecto desde la fecha de su publicación. Las notificaciones que el Usuario quiera dirigir a Doo-ba deberán ser dirigidas a Guevara 533, Ciudad Autónoma de Buenos Aires donde queda constituido el domicilio. Por dudas sobre las Condiciones de Uso o demás políticas y principios que rigen el Sitio el Usuario podrá efectuar las consultas que estime corresponder comunicándose al teléfono del Centro de Atención al Cliente de Doo-ba (011-4588-7000) de Lunes a Viernes de 9:30hs. a 19:30hs. Y Sábados de 9:00hs. a 13:00hs. </p>

            <p>Doo-ba. Todos los derechos reservados.</p>
        </div>
    </div>
    <div id="divencuesta" class="modal fade" role="dialog">
        <div id="resultados" class="modal-dialog modal-lg modal-content modal-dooba">
             <button type="button" class="btn btn-default btn-modal-close btn-circle" data-dismiss="modal"><i class="fa fa-times"></i></button>
            <div class ="col-md-3"><span id="sppreguntaencuesta">Pregunta realizada</span></div><select id="seleccionpreguntas" onchange="crearGrafico();"></select>
            <div id="morris-bar-chart"></div>
        </div>
    </div>

     <div id="idioma" class="modal fade" role="dialog">
        <div id="Div2" class="modal-dialog modal-lg modal-content modal-dooba modal-idioma">
             <button type="button" class="btn btn-default btn-modal-close btn-circle" data-dismiss="modal"><i class="fa fa-times"></i></button>
             <div class="col-md-3"><span id="sidioma">Idioma: </span></div><select id="idiomaSeleccionado" onchange="seleccionarIdioma();"></select>
        </div>
    </div>


     <!-- Bootstrap Core JavaScript -->
    <script src="../scripts/bootstrap.min.js"></script>
    <script src="../scripts/morris.min.js"></script>
    <script src="../scripts/raphael.min.js"></script>
      <script src="../scripts/Principal.js"></script>
     <script src="../scripts/encuestas.js"></script>
    <script>
       

        window.onload = function () {
            obtenerEncuestas();
            obtenerInformacionDePreguntas();
            $("#ContentPlaceHolder1_mensajeRespuesta").fadeOut(3000, function () {
            });
            
            
        }
    </script>



</asp:Content>
