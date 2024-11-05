<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Guardar_Borrador_y_Publicar.aspx.cs" Inherits="TFL_x_WEB.Guardar_Borrador_y_Publicar.Guardar_Borrador_y_Publicar" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <title>Sistema de definici&oacute;n de Programas Formativos: M&oacute;dulos de Poblamiento del MCTP</title>
    <link rel="shortcut icon" href="https://www.inacap.cl/web/template-aplicaciones/img/favicon.ico" />
    <link rel="stylesheet" href="https://www.inacap.cl/web/template-aplicaciones/css/mdb.min.css">
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
        .table button{
          border: none;
          background: transparent;
          cursor: pointer;
        }

        label.count {
          right: 0;
          left: auto;
          top: -20px;
          font-size: 85%;
          transform: none !important;
        }
        table.table-bordered th:last-child, 
        table.table-bordered td:last-child {
          width: 200px;
        }
        table.table td a{
          color: #1565c0;
          text-decoration: underline;
        }
        .btn-add{
          border: none;
          color: #666;
          cursor: pointer;
          border-bottom: 1px solid #666;
          background-color: transparent;
        }

        .tooltip .tooltip-inner{
            max-width: 1000px !important;
            text-align: left !important;
        }
		
		p { word-break: break-word }   /* Impedir textos enormes que desbordan la página */ 

    </style>
</head>
<body id="appBody" class="d-flex flex-column">

    <div class="page-content">

        <div class="navbar-overlay"></div>

        <div id="menuContent">
            
            <!-- Loading Overlay -->
            <div class="loading-backdrop" id="loadingOverlay">
                <div class="loading-circle">
                    <div class="preloader-wrapper big active"><div class="spinner-layer spinner-white-only"><div class="circle-clipper left"><div class="circle"></div></div><div class="gap-patch"><div class="circle"></div></div><div class="circle-clipper right"><div class="circle"></div></div></div></div>
                </div>
            </div>
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
                            <a href="#">Título</a>
                        </h1>
                    </div>

                    <!--Datos del Usuario-->
                    <div class="user-info">

                        <div class="user-name mr-4">
                            <p> Ejemplo Nombre </p>
                        </div>

                        <!-- Iconos encabezado - Mobile -->
                        <div class="user-mobile-menu user-button dropdown dropdown-toggle" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            <i class="material-icons">more_vert</i>

                            <div class="dropdown-menu dropdown-menu-right">
                                <p class="dropdown-user-name">Colaborador X</p>
                                <p class="dropdown-title dropdown-title--border">Accesibilidad</p>
                                <ul class="accesibility-menu">
                                    <li class="accesibility-menu__item">
                                        <div class="d-flex align-items-center justify-content-between" onclick="darkMode();">
                                            <span class="tipo-de-modo">Modo Oscuro</span>
                                            <div class="d-flex justify-content-center">
                                                <span>
                                                    <i class="material-icons">brightness_medium</i>
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

                                <i class="material-icons ">accessibility</i>

                                <div class="dropdown-menu dropdown-menu-right">
                                    <p class="dropdown-title">Accesibilidad</p>
                                    <ul class="accesibility-menu">
                                        <li class="accesibility-menu__item">
                                            <div class="d-flex align-items-center justify-content-between" onclick="darkMode();">
                                                <span class="tipo-de-modo">Modo Oscuro</span>
                                                <div class="d-flex justify-content-center">
                                                    <span>
                                                        <i class="material-icons">brightness_medium</i>
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
        </div>
        <main class="container">
            <section>

                <div class="card">

                    <div class="card-header">
                        <div class="row">
                            <div class="col-12 d-flex align-items-end justify-content-between">
                                <h4 class="float-left heading h4-responsive mb-0">Guardar Borrador y Publicar</h4>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row mb-4">

                            <div class="col-12">
                                <br/>

                                <table id="TablaPrincipal" class="datatables table table-hover table-striped table-bordered m-0" width="100%">
                                    <thead>
                                      <tr>
                                        <th>Prelación</th>
                                        <th>Nombre</th>
                                        <th>Descripción</th>
                                        <th>Fecha</th>
                                        <th>Estado</th>
                                        <th>Acciones</th>
                                      </tr>
                                    </thead>
                                    <tbody>
                  
                                    </tbody>
                                </table> 
                            </div>

                        </div>
                        <div class="row mb-4">
                            <div class="col col-md-6 d-flex justify-content-start">
                                <button type="button" onclick="agregarRegistroAutomatico()" class="btn-add p-2">
                                    + Agregar Nueva Fila automática
                                </button>

                                <button type="button" onclick="abrirModalEditar(null)" class="btn-add p-2" style="margin-left: 2em">
                                    + Agregar Fila
                                </button>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <div class="row py-2 flex-row">
                      
                          <div class="col col-md-6  d-flex justify-content-start">

                            <button type="button" 
                            onclick="window.location.href = '../Menu_Principal/Menu_Principal.aspx';" 
                            class="btn btn-outline waves-effect waves-light">
                                <span>Volver al Menú</span>
                            </button>

                          </div>
                          <div class="col col-md-6 d-flex justify-content-end">
        
                            <button type="button" data-target="#ModalGuardarBorrador" data-toggle="modal"
                            class="btn btn-outline waves-effect waves-light">
                                <span>Guardar Borrador</span>
                            </button>

                            <button type="button" onclick="abrirModalGuardarPublicar()" class="btn btn-default waves-effect waves-light">
                              <span>Guardar y Publicar</span>
                            </button>
                        
                          </div>
                        </div>
                    </div>

                </div>

            </section>
        </main>
    </div>

    <!-- ========================================================= -->

    <div class="modal fade" id="ModalEditar" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
          <div class="modal-content">

              <div class="modal-header">
                  <p name="titulo" class="heading lead">-</p>
                  <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                      <span aria-hidden="true">&times;</span>
                  </button>
              </div>
              <div class="modal-body px-0">

                  <div class="row mb-4">

                    <div class="col-12 col-md-6">
                        <div class="md-form">
                            <p name="posicion" class="form-control" style="border: none"></p>
                            <label class="active">Posición</label>
                        </div>
                    </div> 
                    <div class="col-12 col-md-6">
                        <div class="md-form">
                            <p name="fecha" class="form-control" style="border: none"></p>
                            <label class="active">Fecha</label>
                        </div>
                    </div> 

                    <div class="col-12 col-md-6">
                        <div class="md-form">
                            <select name="prelacion" class="chosen-select">
                                <option value="" selected>Seleccione</option>
                            </select>
                            <label class="active">Prelación</label>
                        </div>
                    </div>

                    <div class="col-12 col-md-6">
                        <div class="md-form mb-3">
                            <input type="text" name="nombre" class="form-control mb-0" placeholder="Ingrese nombre">
                            <label class="active">Nombre</label>
                        </div>
                    </div>

                  </div>
                  <div class="row mb-4">

                      <div class="col-12 col-md-12">
                          <div class="md-form mb-3">
                                <textarea name="descripcion" rows="4" placeholder="Ingrese descripción" class="form-control md-textarea"></textarea>

                                <!-- ESTO ES PARA IMPEDIR POR COMPLETO LOS SALTOS DE LÍNEA TANTO AL ESCRIBIR COMO AL PEGAR TEXTO -->
                                <!--  
                                <textarea name="descripcion" rows="4" placeholder="Ingrese descripción" 
                                oninput="let p = this.selectionStart; this.value = this.value.replace(/\n/g, ' ');this.setSelectionRange(p, p);"
                                onkeydown="if (event.keyCode === 13) { event.preventDefault(); }"
                                class="form-control md-textarea"></textarea>
                                -->

                                <label class="active">Descripción</label>
                          </div>
                      </div>

                  </div>

              </div>
              <div class="modal-footer px-0 justify-content-between">

                  <button type="button" class="btn mx-2 btn-outline waves-effect waves-light" data-dismiss="modal">Cerrar</button>
                  <div>
                      <button type="button" name="btn-guardar" onclick="guardarDatosModalEditar()" 
                      class="btn mx-2 btn-default waves-effect waves-light">Guardar</button>

                      <button type="button" name="btn-guardar-y-continuar" onclick="guardarDatosModalEditar(true)" 
                      class="btn mx-2 btn-default waves-effect waves-light">Guardar y Continuar</button>
                  </div>
              </div>

          </div>
      </div>
    </div>

    <div class="modal fade" id="ModalFiltrarTablaPrincipal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-notify" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <p class="heading lead">Filtros</p>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">

                    <div class="row mb-4">

                        <div class="col-12 col-md-6">
                            <div class="md-form">
                                <select name="estado" class="chosen-select">
                                    <option value="" selected>Seleccione</option>
                                    <option value="Activo">Activo</option>
                                    <option value="Inactivo">Inactivo</option>
                                </select>
                                <label class="active">Estado</label>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="modal-footer justify-content-between">
                    <button onclick="limpiarFormulario('#ModalFiltrarTablaPrincipal')" type="button" class="btn btn-link px-0 d-flex align-items-center waves-effect waves-light">
                        <span class="material-icons mr-1">close
                        </span>
                        Limpiar Filtros
                    </button>
                    <div class="d-flex flex-wrap">
                        <button class="btn mx-2 btn-secondary waves-effect waves-light" data-dismiss="modal">Cerrar</button>

                        <button onclick="resetearPaginacion(); construirGrilla()"
                        class="btn mx-2 btn-default waves-effect waves-light alerta" data-dismiss="modal">Filtrar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="ModalEliminarDetalle" tabindex="-1" role="dialog" aria-labelledby="Danger" aria-hidden="true">
        <div class="modal-dialog modal-notify modal-sm modal-danger" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <p class="heading lead">Eliminar</p>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="text-center">
                        <i class="material-icons icon-3x mb-3 animated rotateIn">delete</i>
                        <p>El detalle será eliminado</p>
                    </div>
                </div>
                <div class="modal-footer justify-content-center">
                    <button class="btn btn-secondary waves-effect px-3 px-3" data-dismiss="modal">Cancelar</button>
                    <button onclick="eliminarDetalle()" class="btn btn-default px-3 px-3">Aceptar</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="ModalGuardarBorrador" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm modal-notify modal-warning" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <p class="heading lead">Guardar Borrador</p>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">

                    <div class="row mb-4">

                        <div class="col-md-12">
                            <p class="text-center"><i class="material-icons icon-3x mb-3 animated rotateIn">warning</i></p>
                            <p class="text-center m-0">
                                <%= infoUsuario.msjGuardarBorrador %>
                            </p>
                        </div>

                    </div>

                </div>
                <div class="modal-footer justify-content-between">
                    <div class="col-12 justify-content-center d-flex flex-wrap">
                        <button type="button" class="btn mx-2 btn-secondary waves-effect waves-light" data-dismiss="modal">Cancelar</button>

                        <button type="button" onclick="guardarBorrador()"
                        class="btn mx-2 btn-default waves-effect waves-light alerta" data-dismiss="modal">Aceptar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="ModalGuardarPublicar" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-md modal-notify modal-warning" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <p class="heading lead">Guardar y Publicar</p>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">

                    <div class="row mb-4">

                        <div class="col-md-12">
                            <p class="text-center"><i class="material-icons icon-3x mb-3 animated rotateIn">warning</i></p>
                            <p class="text-center m-0">
                                <%= infoUsuario.msjPublicar %>
                            </p>
                        </div>

                    </div>
                    <div class="row mb-4">

                        <div class="col-12 col-md-4">
                            <div class="md-form mb-3">

                                <input name="fechaPublicacion" type="text"
                                class="datepicker-input form-control" placeholder="DD/MM/AAAA"
                                data-position="bottom center" autocomplete="off" readonly="readonly" />

                                <label class="active">Seleccione la fecha de Publicación</label>
                            </div>
                        </div>

                        <!-- =================================================================== -->

                        <div class="col-12 col-md-8">

                            <div name="infoCamposIncompletos" class="md-form">
                                <label class="active" style="font-size: 1em; color: #ffa000; font-weight: bold;">
                                    Atención. Este es un mensaje de prueba
                                </label>

                                <div style="padding-top: 0.1em">
                                    <ul class="list-group z-depth-1 md-form">

                                        <li class="list-group-item" style="padding: 0.5rem 0 0">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td style="vertical-align: top">
                                                            <i class="material-icons" style="font-size: 1.5em; margin-left: 0.5em">warning</i>
                                                        </td>
                                                        <td>
                                                            <p style="word-break: break-word; margin-left: 0.5em">Probando 1</p>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </li>
                                        <li class="list-group-item" style="padding: 0.5rem 0 0">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td style="vertical-align: top">
                                                            <i class="material-icons" style="font-size: 1.5em; margin-left: 0.5em">warning</i>
                                                        </td>
                                                        <td>
                                                            <p style="word-break: break-word; margin-left: 0.5em">Probando 2</p>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </li>
                                    </ul>
                                </div>
                            </div>

                        </div>

                        <!-- =================================================================== -->

                    </div>

                </div>
                <div class="modal-footer justify-content-end">
                    <div class="d-flex flex-wrap">
                        <button type="button" class="btn mx-2 btn-secondary waves-effect waves-light"
                        data-dismiss="modal">Cancelar</button>

                        <button type="button" onclick="guardar_y_publicar()"
                        class="btn mx-2 btn-default waves-effect waves-light alerta" data-dismiss="modal">Aceptar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <footer class="page-footer">
        <div class="container py-3">
            <div class="d-flex align-items-center justify-content-center justify-content-md-start">
                <div class="logo-gst">
                    <img src="http://www.inacap.cl/web/template-aplicaciones/img/logo-gst.png" alt="">
                </div>
                <span class="pl-2 pl-md-4">Gerencia de Sistemas y Tecnolog&iacute;as</span>
            </div>
        </div>
    </footer>

  <script src="../MCTP_Disenio/jquery-3.3.1.min.js"></script>
  <script src="../MCTP_Disenio/popper.min.js"></script>
  <script src="../MCTP_Disenio/bootstrap.min.js"></script>
  <script src="../MCTP_Disenio/mdb.min.js"></script>
  <script src="../MCTP_Disenio/chosen.jquery.js"></script>
  <script src="../MCTP_Disenio/datepicker.js"></script>
  <script src="../MCTP_Disenio/datatables.min.js"></script>
  <script src="../MCTP_Disenio/xlsx.full.min.js"></script>
  <script src="../MCTP_Disenio/configuraciones.js"></script>


<%--    <script src="https://www.inacap.cl/web/template-aplicaciones/js/jquery-3.3.1.min.js"></script>
    <script src="https://www.inacap.cl/web/template-aplicaciones/js/popper.min.js"></script>
    <script src="https://www.inacap.cl/web/template-aplicaciones/js/bootstrap.min.js"></script>
    <script src="https://www.inacap.cl/web/template-aplicaciones/js/mdb.min.js"></script>
    <script src="http://www.inacap.cl/web/template-aplicaciones/js/chosen.jquery.js"></script>
    <script src="https://www.inacap.cl/web/template-aplicaciones/js/datepicker.js"></script>  
    <script src="http://www.inacap.cl/web/template-aplicaciones/js/datatables.min.js"></script>--%>

    <script src="Guardar_Borrador_y_Publicar.js"></script>

</body>
</html>