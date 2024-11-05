<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ejemplo_Mantenedor.aspx.cs" Inherits="TFL_x_WEB.Ejemplo_Mantenedor.Ejemplo_Mantenedor" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <title>Sistema de definici&oacute;n de Programas Formativos: M&oacute;dulos de Poblamiento del MCTP</title>
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
        table.table td .table-link {
            text-decoration: underline;
            color: blue;
        }

        .sub-tooltip {
            font-size: .75rem;
            margin-bottom: 0.5rem;
        }

        .btn {
            font-size: 1rem;
        }
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
                <a href="#">Sistema de definici&oacute;n de Programas Formativos: M&oacute;dulos de Poblamiento del MCTP</a>
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
                    <li class="nav-item">
                        <a href="#" class="nav-link waves-effect">Men&uacute; Operacional</a>
                    </li>
                    <li class="nav-item">
                        <a href="#" class="nav-link waves-effect active">Men&uacute; de Definiciones</a>
                    </li>
                </ul>
            </div>
        </nav>

        <main class="container">

            <section class="row breadcrumb">
                <span class="breadcrumb-item">Men&uacute; Definiciones</span>
                <span class="breadcrumb-item">
                    <a href="#">Cualificaciones</a>
                </span>
                <span class="breadcrumb-item active">Probando</span>
            </section>

            <section id="seccionInsertar">

                <div class="card" id="buscar">

                    <div class="card-header">
                        <h4 class="float-left heading h4-responsive mb-0">Probando</h4>
                    </div>


                    <div class="card-body">

                        <div class="accordion md-accordion" id="accordion1" role="tablist" aria-multiselectable="true">


                            <div class="accordion-item">

                                <div class="accordion-header grey lighten-3" role="tab" id="heading1">
                                    <a data-toggle="collapse" data-parent="#accordion1" href="#collapse1" aria-expanded="false" aria-controls="collapse1">
                                        <h5 class="mb-0 pl-3 pr-3">1. A&ntilde;adir
                     
                                            <span class="accordion-arrow"><i class="material-icons rotate-icon float-right">keyboard_arrow_down</i></span>
                                        </h5>
                                    </a>
                                </div>

                                <div id="collapse1" class="collapse show" role="tabpanel" aria-labelledby="heading1" data-parent="#accordion1">

                                    <div class="accordion-body p-3 pt-3 pb-2">

                                        <form id="formCrear">

                                            <div class="row mb-4">

                                                <div class="col-12 col-md-4">
                                                    <div class="md-form mb-3">

                                                        <input type="text" name="nombre" class="form-control mb-0"
                                                            maxlength="100"
                                                            oninput="this.value = this.value.toUpperCase()"
                                                            placeholder="Ingrese nombre" />

                                                        <label class="active">Nombre</label>
                                                    </div>
                                                </div>
                                                <div class="col-12 col-md-4">
                                                    <div class="md-form mb-3">

                                                        <input name="fechaEfectiva" type='text'
                                                            class="datepicker-input form-control" placeholder="DD/MM/AAAA"
                                                            data-position="bottom center" autocomplete="off" readonly="readonly" />

                                                        <label class="active">Fecha Efectiva</label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row mb-4">

                                                <div class="col-12 col-md-8">
                                                    <div class="md-form">
                                                        <textarea name="descripcion" rows="4" placeholder="Ingrese descripci&oacute;n"
                                                            maxlength="256" class="form-control md-textarea"></textarea>
                                                        <label for="campo_def_04" class="active">Descripci&oacute;n</label>
                                                    </div>

                                                </div>

                                            </div>

                                            <div class="row py-2 flex-row">

                                                <div class="col p-0 d-flex justify-content-end">

                                                    <button type="button" class="btn btn-default waves-effect waves-light" onclick="agregarNuevo()">
                                                        <span>A&ntilde;adir</span>
                                                    </button>

                                                </div>

                                            </div>

                                        </form>

                                    </div>
                                </div>

                            </div>

                            <div class="accordion-item">

                                <div onclick="obtenerFiltros()" class="accordion-header grey lighten-3" role="tab" id="heading2">
                                    <a data-toggle="collapse" data-parent="#accordion1" href="#collapseFiltrar" aria-expanded="false" aria-controls="collapseFiltrar">
                                        <h5 class="mb-0 pl-3 pr-3">2. Seleccionar
                     
                                            <span class="accordion-arrow"><i class="material-icons rotate-icon float-right">keyboard_arrow_down</i></span>
                                        </h5>
                                    </a>
                                </div>

                                <div id="collapseFiltrar" class="collapse" role="tabpanel" aria-labelledby="heading2" data-parent="#accordion1">

                                    <div class="accordion-body p-3 pt-3 pb-2">

                                        
                                        <form id="formFiltrar">

                                            <div class="row mb-4">

                                                <div class="col-12 col-md-4">
                                                    <div class="md-form">

                                                        <select name="codigo" class="chosen-select" searchable="Buscar">
                                                            <option value="" selected>Seleccione</option>
                                                            
                                                        </select>
                                                        <label class="active">Nombre</label>
                                                    </div>
                                                </div>

                                                <div class="col-12 col-md-4">
                                                    <div class="md-form">
                                                        <select name="estado" class="chosen-select">
                                                            <option value="" selected>Seleccione</option>
                                                            

                                                            <option value="A">Activo</option>
                                                            <option value="I">Inactivo</option>
                                                            <option value="Z">Todos</option>

                                                        </select>
                                                        <label class="active">Estado</label>
                                                    </div>
                                                </div>

                                                <div class="col-12 col-md-4">
                                                    <div class="md-form">

                                                        <input name="fechaEfectiva" type='text'
                                                            class="datepicker-input form-control" placeholder="DD/MM/AAAA"
                                                            data-position="bottom center" autocomplete="off" readonly="readonly" />

                                                        <label class="active">Fecha Efectiva</label>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="row">

                                                <div class="col p-0 d-flex justify-content-end">

                                                    <table>
                                                        <tr>
                                                            <td style="width: 100px;">
                                                                <div class="md-form">
                                                                    

                                                                    <select name="entradas" class="chosen-select">

                                                                            <option value="10">10</option>
                                                                        
                                                                            <option value="20">20</option>
                                                                        
                                                                            <option value="30">30</option>
                                                                        
                                                                    </select>
                                                                    <label class="active">Entradas a Leer</label>
                                                                </div>

                                                            </td>
                                                            <td>
                                                                <button type="button" onclick="construirGrilla()" class="btn btn-default waves-effect waves-light">
                                                                    <span>Buscar</span>
                                                                </button>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                </div>

                                            </div>


                                        </form>

                                    </div>
                                </div>

                            </div>

                        </div>

                    </div>

                    <div class="card-footer">

                        <div class="row py-2 flex-row">

                            <div class="col col-md-6  d-flex justify-content-start">
                                <a class="btn btn-outline waves-effect waves-light" href="../Menu_Principal/Menu_Principal.aspx">
                                    <span>Volver</span>
                                </a>
                            </div>

                        </div>

                    </div>

                </div>

            </section>

            <section id="seccionGrilla" style="display: none">

                <div class="card" id="resultados">

                    <div class="card-header">

                        <div class="row">

                            <div class="col-12 d-flex align-items-end justify-content-between">
                                <h4 class="float-left heading h4-responsive mb-0">Ejemplo Mantenedor</h4>
                            </div>

                        </div>

                    </div>

                    <div class="card-body">
                        <div class="row">
                            <div class="col-12">

                                <table id="TablaPrincipal" class="datatables table table-hover table-striped table-bordered m-0"
                                style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>Nombre</th>
                                            <th>Descripción</th>
                                            <th>Fecha Efectiva</th>
                                            <th>Estado</th>
                                            <th>Acciones</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>

                            </div>
                        </div>
                    </div>

                    <div class="card-footer">

                        <div class="row mt-4">

                            <div class="col col-md-6  d-flex justify-content-start">
                                <button id="btnBuscar" class="btn btn-outline waves-effect waves-light" onclick="volverBuscar()">
                                    <span>Volver</span>
                                </button>
                            </div>

                        </div>

                    </div>

                </div>

            </section>
        </main>


    </div>

    <div class="modal fade" id="ModalEditar" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog sm modal-notify" role="document">
            <div class="modal-content">

                <form id="formEditar">

                    <div class="modal-header">
                        <p class="heading lead">Modificar Informaci&oacute;n</p>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body px-0">

                        <div class="row mb-4">

                            <div class="col-12 col-md-6">
                                <div class="md-form">

                                    <input type="text" name="nombre" class="form-control mb-0"
                                        maxlength="100" placeholder="Ingrese nombre" disabled />

                                    <label class="active">Nombre</label>
                                </div>
                            </div>

                            <div class="col-12 col-md-4">
                                <div class="md-form">
                                    <select class="chosen-select" name="estado" disabled>
                                        <option value="" disabled selected>Seleccione</option>
                                        <option value="A">Activo</option>
                                        <option value="I">Inactivo</option>
                                    </select>
                                    <label class="active">Estado</label>
                                </div>
                            </div>

                            <div class="col-12 col-md-6">
                                <div class="md-form">

                                    <input name="fechaEfectiva" type="text"
                                        class="datepicker-input form-control" placeholder="DD/MM/AAAA"
                                        data-position="bottom center" autocomplete="off" readonly="readonly" />

                                    <label for="fechaEfectiva" class="active">Fecha Efectiva</label>
                                </div>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-12 col-md-12">
                                <div class="md-form">
                                    <textarea name="descripcion" rows="4" placeholder="Ingrese descripci&oacute;n"
                                        maxlength="256" class="form-control md-textarea"></textarea>

                                    <label class="active">Descripci&oacute;n</label>
                                </div>

                            </div>
                        </div>

                    </div>
                    <div class="modal-footer px-0">

                        <button type="button" class="btn btn-secondary waves-effect waves-light" data-dismiss="modal">
                            <span>Cancelar</span>
                        </button>

                        <button type="button" onclick="actualizar()" class="btn btn-default waves-effect waves-light">
                            <span>Guardar</span>
                        </button>

                    </div>

                </form>


            </div>
        </div>
    </div>


    <div class="modal fade" id="ModalCambiarEstado" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm modal-notify" role="document">
            <div class="modal-content">

                <form id="formCambiarEstado">

                    <div class="modal-header">
                        <p class="heading lead">Cambiar Estado</p>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body px-0">

                        <div class="row mb-4">

                            <div class="col-12">
                                <div class="md-form">
                                    <p name="nombreDesplegado"></p>
                                    <label class="active text-nowrap">Nombre</label>
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="md-form">
                                    <select class="chosen-select" name="estado">
                                        <option value="A">Activo</option>
                                        <option value="I">Inactivo</option>
                                    </select>
                                    <label class="active">Estado</label>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer px-0">

                        

                        <button type="button" id="btnCambiarEstado" class="btn btn-default waves-effect waves-light" onclick="cambiarEstado()">
                            <span>Cambiar</span>
                        </button>

                    </div>
                </form>
            </div>
        </div>
    </div>


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
    <script src="http://www.inacap.cl/web/template-aplicaciones/js/datatables.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.18.5/xlsx.full.min.js"></script>--%>
	

    <script src="Ejemplo_Mantenedor.js"></script>

</body>
</html>
