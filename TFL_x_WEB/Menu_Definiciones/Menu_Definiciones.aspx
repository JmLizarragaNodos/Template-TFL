<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu_Definiciones.aspx.cs" Inherits="TFL_x_WEB.Menu_Definiciones.Menu_Definiciones" %>

<!DOCTYPE html>
<html lang="en"> 
<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <meta http-equiv="x-ua-compatible" content="ie=edge">
  <title>Sistema de definici&oacute;n de Programas Formativos: M&oacute;dulo Dise&ntilde;o Etapa 1 MCTP</title>
  <link rel="shortcut icon" href="https://www.inacap.cl/web/template-aplicaciones/img/favicon.ico" />
  <link rel="stylesheet" href="https://www.inacap.cl/web/template-aplicaciones/css/mdb.min.css">
  <script>
      (function (i, s, o, g, r, a, m) {
          i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
              (i[r].q = i[r].q || []).push(arguments)
          }, i[r].l = 1 * new Date(); a = s.createElement(o),
              m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
      })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');
      ga('create', 'UA-6767823-1', 'auto');
      ga('send', 'pageview');
  </script>
  <style>
    .list-card{
      padding: 0;
      list-style: none;
      font-weight: 700;
    }
    .list-card li{
      margin-bottom: 1rem;
      font-size: 14px;
    }
    .card-home{
      height: 100%;
      min-height: 50vh;
    }
    .separator-inacap{
      width: 100%;
      height: 1px;
      background-color: #ccc;
    }
  </style>

</head>
<body id="appBody" class="d-flex flex-column">
  
  <div class="page-content">
    
    <div class="navbar-overlay"></div>
    
    <header class="header">
      
      <div class="header-container">
        
        <!--Logo-->
        <div class="header-logo">
          <a href="https://www.inacap.cl">
            <img src="https://www.inacap.cl/web/template-aplicaciones/img/isotipo.png" alt="INACAP" class="logo-mobile">
            <img src="https://www.inacap.cl/web/template-aplicaciones/img/logo-inacap.png" alt="INACAP" class="logo">
          </a>
        </div>
        
        <!--Titulo, recuerda que tambien debes reemplazar el nombre en el script m&aacute;s abajo-->
        <div class="header-title">
          <h1 id="title">
            <a href="#">
              Sistema de definici&oacute;n de Programas Formativos: M&oacute;dulo Dise&ntilde;o Etapa 1 MCTP
            </a>
          </h1>
        </div>
        
        <!--Datos del Usuario-->
        <div class="user-info">
          
          <!--Nombre-->
          <div class="user-name mr-4">
            <p> <%= usuario.nombre %> </p>
          </div>
          
          
          <!-- Iconos encabezado - Mobile -->
          <div class="user-mobile-menu user-button dropdown dropdown-toggle" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
            <i class="material-icons">more_vert</i>
            
            <div class="dropdown-menu dropdown-menu-right">
              <p class="dropdown-user-name">Colaborador X</p>
              <p class="dropdown-title dropdown-title--border">Accesibilidad</p> 
              <ul class="accesibility-menu">
                <li class="accesibility-menu__item">
                  <div  class="d-flex align-items-center justify-content-between" onClick="darkMode();">
                    <span class="tipo-de-modo">Modo Oscuro</span>
                    <div class="d-flex justify-content-center">
                      <span>
                        <i class="material-icons"> brightness_medium</i>
                      </span>
                    </div>
                  </div>
                </li>
                <li class="accesibility-menu__item">
                  <div class="d-flex align-items-center justify-content-between">
                    <span>Tama&ntilde;o de letra</span>
                    <div class="d-flex justify-content-center">
                      <span tabindex="0" role="button" class="btnDecrease btn btn-round btn-fonts btn-secondary d-flex align-items-center justify-content-center waves-effect waves-light">
                        <i class="material-icons">remove</i>
                      </span>
                      <span tabindex="0" role="button" class="btnEnlarge btn btn-round btn-fonts btn-default d-flex align-items-center justify-content-center ml-2 mr-0 waves-effect waves-light">
                        <i class="material-icons">add</i>
                      </span>
                    </div>
                  </div>
                </li>
              </ul>
              
              <!--- Cerrar Sesi&oacute;n-->
              <p class="dropdown-title dropdown-title--border">Cuenta</p> 
              <ul class="accesibility-menu">
                <li class="accesibility-menu__item">
                  <div class="d-flex align-items-center justify-content-between">
                    <span id="tipo-de-modo">Cerrar Sesi&oacute;n</span>
                    <div class="d-flex justify-content-center">
                      <span>
                        <i class="material-icons">exit_to_app</i>
                      </span>
                    </div>
                  </div>
                </li>
              </ul>
              
            </div>
            
          </div>
          <!-- Iconos encabezado - Mobile -->
          
          <!-- Iconos encabezado - Escritorio-->
          <div class="user-desktop-icons">
            
            <div class="user-accesibility user-button dropdown dropdown-toggle" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
              
              <i class="material-icons " >accessibility</i>
              
              <div class="dropdown-menu dropdown-menu-right">
                <p class="dropdown-title">Accesibilidad</p> 
                <ul class="accesibility-menu">
                  <li class="accesibility-menu__item">
                    <div class="d-flex align-items-center justify-content-between" onClick="darkMode();">
                      <span class="tipo-de-modo">Modo Oscuro</span>
                      <div class="d-flex justify-content-center">
                        <span>
                          <i class="material-icons"> brightness_medium</i>
                        </span>
                      </div>
                    </div>
                  </li>
                  <li class="accesibility-menu__item">
                    <div class="d-flex align-items-center justify-content-between">
                      <span>Tama&ntilde;o de letra</span>
                      <div class="d-flex justify-content-center">
                        <span tabindex="0" role="button" class="btnDecrease btn btn-round btn-fonts btn-secondary d-flex align-items-center justify-content-center waves-effect waves-light"><i class="material-icons">remove</i></span>
                        <span tabindex="0" role="button" class="btnEnlarge btn btn-round btn-fonts btn-default d-flex align-items-center justify-content-center ml-2 mr-0 waves-effect waves-light"><i class="material-icons">add</i></span>
                      </div>
                    </div>
                  </li>
                </ul>
              </div>
              
            </div>
            
            <div class="user-logout user-button">
              <a href="#"><i class="material-icons">exit_to_app</i></a>
            </div>
            
          </div>
          <!-- Iconos encabezado - Escritorio-->
          
        </div>        
      </div>
      
    </header>

    <nav class="navbar toggle-navbar-lg">
      <div class="container">
          <button class="d-lg-none d-flex align-items-center pl-0" id="toggle-navbar" aria-label="Ver Menu" onclick="openNav()">
              <i class="material-icons icon-lg mr-1">menu</i>
              <span>Menu</span>
          </button>

          <ul id="offCanvas" class="nav navbar-nav navbar-offcanvas navbar-expand-lg justify-content-lg-around">
              <a href="javascript:void(0)" class="d-lg-none closebtn" onclick="closeNav()">
                  <span role="button" class="material-icons">close</span>
              </a>
              <%= cadenaMenu %>
          </ul>
      </div>
    </nav>

    <main id="main" class="container">
        <section class="row breadcrumb">
            <span class="breadcrumb-item active">Men&uacute; Definiciones</span>
        </section>

        <section>
            <div class="row">
                <%= cadenaSubMenu %>
            </div>

        </section>
    </main>    
   
    <!-- ============================================================================ -->

    <!--
    <main id="main" class="container">
      <section class="row breadcrumb">
        <span class="breadcrumb-item active">Men&uacute; de Definiciones</span>
      </section>

      <section>
        <div class="row">

          <div class="col mb-4">

            <div class="card card-home">

              <div class="card-header">
                <h3 class="h3-responsive">Etapa 1</h3>
              </div>
              
              <div class="card-body">
                <ul class="list-card" class="mb-4">
  
                  <li><a href="../DEF_PVIGENCIA_TFL/DEF_PVIGENCIA_TFL.aspx">Período de Vigencia del Diseño de TFL ></a></li>
                  <li><a href="../DEF_DIR_SEC_VRA/DEF_DIR_SEC_VRA.aspx">Definición Dirección Sectorial VRA ></a></li>
                  <li><a href="../DEF_AREA/DEF_AREA.aspx">Definición Área ></a></li>
                  <li><a href="../DEF_TFL/DEF_TFL.aspx">Definición TFL ></a></li>
                  <li><a href="../DEF_PPROF/DEF_PPROF.aspx">Definición del Perfil Profesional de la TFL ></a></li>
                  <li><a href="../DEF_LVC_ET/DEF_LVC_ET.aspx">Definición LVC Etapas ></a></li>
                  <li><a href="../DEF_LVC/DEF_LVC.aspx">Definición Lista de Verificación (LVC) ></a></li>
                  <li><a href="../DEF_ADEMANDA/DEF_ADEMANDA.aspx">Definición Análisis de Demanda ></a></li>
                </ul>
              </div>
            
            </div>

          </div>
          
          <div class="col mb-4">

            <div class="card card-home">

              <div class="card-header">
                <h3 class="h3-responsive">Etapa 2</h3>
              </div>
              
              <div class="card-body">
              
              </div>
              

            </div>

          </div>
          
          <div class="col mb-4">

            <div class="card card-home">

              <div class="card-header">
                <h3 class="h3-responsive">Etapa 3</h3>
              </div>

              
              <div class="card-body">
              
              </div>
              

            </div>

          </div>

          <div class="col mb-4">

            <div class="card card-home">

              <div class="card-header">
                <h3 class="h3-responsive">Etapa 4</h3>
              </div>

              
              <div class="card-body">
              
              </div>
              

            </div>

          </div>

          <div class="col mb-4">

            <div class="card card-home">

              <div class="card-header">
                <h3 class="h3-responsive">Etapa 5</h3>
              </div>

              
              <div class="card-body">
              
              </div>
              

            </div>

          </div>

        </div>
      
      </section>
    </main>
    -->

    <!-- ============================================================================ -->

  </div>
  
  
  <footer class="page-footer">
    <div class="container py-3">
      <div class="d-flex align-items-center justify-content-center justify-content-md-start">
        <div class="logo-gst"><img src="http://www.inacap.cl/web/template-aplicaciones/img/logo-gst.png" alt=""></div>
        <span class="pl-2 pl-md-4">Gerencia de Sistemas y Tecnolog&iacute;as</span>
      </div>
    </div>
  </footer>

<%--    <div class="modal fade" id="modalAvisoCierreSesion" tabindex="-1" role="dialog"  aria-hidden="true">
      <div class="modal-dialog modal-notify modal-success modal-sm" role="document">
          <div class="modal-content">
            <div class="modal-header">
              <p class="heading lead">Alerta</p>
              <button type="button" onclick="cerrarModalAvisoCierreSesion()" class="close" aria-label="Close">
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
            <div class="modal-body">
              <div class="text-center">
                <p name="mensaje"></p>
                <p name="tiempoRestante"></p>
              </div>
            </div>
            <div class="modal-footer d-flex justify-content-center">
              <button onclick="finalizarSesion()" class="btn btn-secondary waves-effect waves-light" data-dismiss="modal">No</button>
              <button onclick="renovarSesion()" class="btn btn-default waves-effect waves-light" data-dismiss="modal">Sí</button>
            </div>
          </div>
        </div>
    </div>--%>

    <div class="modal fade" id="ModalError" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-md modal-notify modal-danger" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <p class="heading lead">Se encontró un error. Por favor contacte a la mesa de ayuda</p>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="red-text">
                                <p name="mensaje-error"></p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer justify-content-rigth">
                    <a data-dismiss="modal" class="btn btn-default waves-effect waves-light alerta">Aceptar</a>
                </div>
            </div>
        </div>
    </div>
  
  
  <!-- Loading Overlay -->
  <div class="loading-backdrop" id="loadingOverlay">
    <div class="loading-circle">
      <div class="preloader-wrapper big active"><div class="spinner-layer spinner-white-only"><div class="circle-clipper left"><div class="circle"></div></div><div class="gap-patch"><div class="circle"></div></div><div class="circle-clipper right"><div class="circle"></div></div></div></div>
    </div>
  </div>
    
    
    <script src="../MCTP_Disenio/jquery-3.3.1.min.js"></script>
    <script src="../MCTP_Disenio/popper.min.js"></script>
    <script src="../MCTP_Disenio/bootstrap.min.js"></script>
    <script src="../MCTP_Disenio/mdb.min.js"></script>
    <script src="../MCTP_Disenio/chosen.jquery.js"></script>
    <script src="../MCTP_Disenio/datepicker.js"></script>
    <script src="../MCTP_Disenio/datatables.min.js"></script>
    <script src="../MCTP_Disenio/xlsx.full.min.js"></script>
    <script src="../MCTP_Disenio/configuraciones.js"></script>

    <script>
        const errorCarga = `<%= errorCarga %>`;

        $(document).ready(function ()
        {
            if (errorCarga != "") 
                mostrarErroresRespuestaBackend({ objeto: null, errores: [errorCarga], status: 500 });
        });

        function tflProcesarAplicacion(evento, apli_caplicacion)
        {
            evento.preventDefault();
            // console.log("href", evento.target.href);

            $.ajax({
                method: "POST",
                url: "Menu_Definiciones.aspx/TFL_PROCESAR_APLICACION",
                data: JSON.stringify({ apli_caplicacion }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: () => showLoading(),
                success: (res) =>
                {
                    if (res.status == 200)
                    {
                        setTimeout(() =>
                        {
                            hideLoading();
                            window.open(evento.target.href);
                        }, 500);
                    }
                    else {
                        hideLoading();
                        mostrarErroresRespuestaBackend(res);
                    }
                },
                error: (XMLHttpRequest, textStatus, errorThrown) => {
                    hideLoading();
                    toastr.error("Ocurrió un error al llamar a TFL_PROCESAR_APLICACION");
                }
            });
        }

        function showLoading() {
            $('#loadingOverlay').show();
        }

        function hideLoading() {
            $('#loadingOverlay').hide();
        }

    </script>
    
</body>
</html>