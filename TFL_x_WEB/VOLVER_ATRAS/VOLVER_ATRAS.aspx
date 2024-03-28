<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VOLVER_ATRAS.aspx.cs" Inherits="TFL_x_WEB.VOLVER_ATRAS.VOLVER_ATRAS" %>
<%@ Import Namespace="MCTP_c_Modelos_de_Datos.Entity" %>

<!DOCTYPE html>
<html lang="en">

<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <meta http-equiv="x-ua-compatible" content="ie=edge">
  <title>Administración de TFL</title>
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
    /* Impedir textos enormes que desbordan la página */
    p { word-break: break-word }   

    /*==============================================================*/

         .separator {
            height: 1px;
            background-color: #d8d8d8;
            padding-left: 0;
            padding-right: 0;
            width: 100%;
         }
         .btn {
            min-width: 90px;
         }
         .btn-add {
            border: none;
            color: #666;
            cursor: pointer;
            border-bottom: 1px solid #666;
            background-color: transparent;
         }
         .btn-round {
            min-width: initial;
         }
         .table button {
            border: none;
            background: transparent;
            cursor: pointer;
         }

         .c-requerido {
            color: #c00;
            font-size: 0.75rem;
            opacity: 0;
            visibility: hidden;
            transition: all 0.3s;
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
            max-width: 200px !important;
         }
         .editar .md-form .form-control.focus,
         .editar .select-wrapper.focus-select input.select-dropdown {
            color: #cdcdcd;
         }
         .overflow-table {
            overflow-x: scroll;
            padding-bottom: 20px;
         }
         .list-card {
            padding: 0;
            list-style: none;
            font-weight: 700;
         }

         .list-card li {
            margin-bottom: 1rem;
            font-size: 14px;
         }

    /*==============================================================*/

  .bs-tooltip-top .arrow::before, .bs-tooltip-auto[x-placement^="top"] .arrow::before{
    border-top-color: #1565C0;  /* Flecha que une al tooltip con el elemento sobre el cual pasa el mouse */
  }

  .tooltip.bs-tooltip-top .tooltip-inner {
    background-color: #1565C0; /* Tooltip hacia arriba */
  }

  .tooltip.bs-tooltip-bottom .tooltip-inner {
    background-color: #1565C0;  /* (Agregado) Tooltip hacia abajo */
  }

  .tooltip .tooltip-inner{
    max-width: 1000px !important;
    /* width: 900px !important; */
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
            <a href="#">Administración de TFL</a>
          </h1>
        </div>

        <!--Datos del Usuario-->
        <div class="user-info">

          <!--Nombre-->
          <div class="user-name mr-4">
            <p><%= usuario.nombre %></p>
          </div>


          <!-- Iconos encabezado - Mobile -->
          <div class="user-mobile-menu user-button dropdown dropdown-toggle" role="button" data-toggle="dropdown"
            aria-haspopup="true" aria-expanded="false">
            <i class="material-icons">more_vert</i>

            <div class="dropdown-menu dropdown-menu-right">
              <p class="dropdown-user-name">Colaborador X</p>
              <p class="dropdown-title dropdown-title--border">Accesibilidad</p>
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
                      <span tabindex="0" role="button"
                        class="btnDecrease btn btn-round btn-fonts btn-secondary d-flex align-items-center justify-content-center waves-effect waves-light">
                        <i class="material-icons">remove</i>
                      </span>
                      <span tabindex="0" role="button"
                        class="btnEnlarge btn btn-round btn-fonts btn-default d-flex align-items-center justify-content-center ml-2 mr-0 waves-effect waves-light">
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

            <div class="user-accesibility user-button dropdown dropdown-toggle" role="button" data-toggle="dropdown"
              aria-haspopup="true" aria-expanded="false">

              <i class="material-icons ">accessibility</i>

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
                        <span tabindex="0" role="button"
                          class="btnDecrease btn btn-round btn-fonts btn-secondary d-flex align-items-center justify-content-center waves-effect waves-light"><i
                            class="material-icons">remove</i></span>
                        <span tabindex="0" role="button"
                          class="btnEnlarge btn btn-round btn-fonts btn-default d-flex align-items-center justify-content-center ml-2 mr-0 waves-effect waves-light"><i
                            class="material-icons">add</i></span>
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
        <button class="d-lg-none d-flex align-items-center pl-0" id="toggle-navbar" aria-label="Ver Menu"
          onclick="openNav()">
          <i class="material-icons icon-lg mr-1">menu</i>
          <span>Menu</span>
        </button>

        <ul id="offCanvas" class="nav navbar-nav navbar-offcanvas navbar-expand-lg justify-content-lg-around">
          <a href="javascript:void(0)" class="d-lg-none closebtn" onclick="closeNav()">
            <span role="button" class="material-icons">
              close
            </span>
          </a>
          <li class="nav-item">
              <a href="#" class="nav-link waves-effect active">Menú Operacional</a>
          </li>
          <li class="nav-item">
            <a href="#" style="visibility: hidden" class="nav-link waves-effect">Menú de Definiciones</a>
          </li>
        </ul>
      </div>
    </nav>

    <!-- Loading Overlay -->
    <div class="loading-backdrop" id="loadingOverlay">
      <div class="loading-circle">
          <div class="preloader-wrapper big active"><div class="spinner-layer spinner-white-only"><div class="circle-clipper left"><div class="circle"></div></div><div class="gap-patch"><div class="circle"></div></div><div class="circle-clipper right"><div class="circle"></div></div></div></div>
      </div>
    </div>

    <main id="main" class="container">

<%--      <section class="row breadcrumb">
        <span class="breadcrumb-item">Menú Operacional</span>
        <span class="breadcrumb-item">
            <a href="../Menu_Operacional/Menu_Operacional.aspx">Etapa 1</a>
        </span>
        <span class="breadcrumb-item active">Buscar TFL para Modificación</span>
      </section>--%>

      <section class="row breadcrumb">
        <span class="breadcrumb-item active">Buscar TFL para Modificación</span>
      </section>

      <section>

        <div class="card" id="buscar">

          <div class="card-header">
            <h4 class="float-left heading h4-responsive mb-0">Buscar TFL para modificación</h4>
          </div>

          <div class="card-body">

            <div class="row mb-4">

                <div class="col-12 col-md-4">
                <div class="md-form">
                    <select name="periodoVigencia" class="chosen-select" searchable="Buscar">
                    <option value="" selected>Seleccione Período</option>
                        <% foreach(COMBOBOX_ENT x in periodosVigenciaTFL) { %>
                        <option value="<%= x.codigo %>"><%= x.descripcion %></option>
                        <% } %>
                    </select>
                    <label class="active">Período Vigencia Diseño de TFL</label>
                </div>
                </div>

                <div class="col-12 col-md-4">
                <div class="md-form">
                    <select name="direccionSectorial" class="chosen-select" searchable="Buscar">
                    <option value="" selected>Seleccione direcci&oacute;n</option>
                    <% foreach (COMBOBOX_ENT x in dir_sec_vra) { %>
                        <option value="<%= x.codigo %>"><%= x.descripcion %></option>
                    <% } %>
                    </select>
                    <label class="active">Dirección Sectorial</label>
                </div>
                </div>

                <div class="col-12 col-md-4">
                <div class="md-form">
                    <select name="area" class="chosen-select" searchable="Buscar">
                    <option value="" selected>Seleccione Área</option>
                    </select>
                    <label class="active">Área</label>
                </div>
                </div>

            </div>

            <div class="row mb-4">

                <div class="col-12 col-md-4">
                <div class="md-form">
                    <select name="tfl" class="chosen-select" searchable="Buscar">
                    <option value="" selected>Seleccione TFL</option>
                    </select>
                    <label class="active">TFL</label>
                </div>
                </div>

                <div name="col-fecha-efectiva" class="col-12 col-md-4" style="visibility: hidden">
                    <div class="md-form">
                        <p name="fechaEfectiva" class="form-control" style="border: none"></p>
                        <label class="active">Fecha Efectiva</label>
                    </div>
                </div>

            </div>
                    
            <div class="row mb-4">

                <div class="col p-0 d-flex justify-content-end">

                <button onclick="buscar()" class="btn btn-default waves-effect waves-light">
                    <span>Buscar TFL</span>
                </button>

                </div>

            </div>
                    
          </div>

          <%--  
          <div class="card-footer">

            <div class="row flex-row my-4">

              <div class="col col-md-6  d-flex justify-content-start">
                <a class="btn btn-outline waves-effect waves-light" href="../Menu_Operacional/Menu_Operacional.aspx">
                  <span>Volver</span>
                </a>
              </div>

            </div>

          </div>
          --%>

        </div>

        <%-- ========================================================================================================================= --%>

        <div id="resultados" style="display: none;">

            <div class="card">
                <div class="card-header">
                <div class="row">
                    <div class="col-12 d-flex align-items-end justify-content-between">
                        <h4 class="float-left heading h4-responsive mb-0">Buscar TFL para modificación</h4>
                        <button onclick="realizarNuevaBusqueda()" class="btn btn-outline waves-effect waves-light">
                            <span>Hacer Nueva Búsqueda</span>
                        </button>
                    </div>
                </div>
                </div>

                <div class="card-body">
                <div class="row mb-4">
                    <div class="col-12 col-md-4">
                        <p>
                            <span class="font-weight-bold">Periodo Diseño TFL:</span> 
                            <span name="periodoVigencia">-</span>
                        </p>
                    </div>
                    <div class="col-12 col-md-4">
                        <p>
                            <span class="font-weight-bold">Dirección Sectorial:</span> 
                            <span name="direccionSectorial">-</span>
                        </p>
                    </div>
                    <div class="col-12 col-md-4">
                        <p>
                            <span class="font-weight-bold">Área:</span> 
                            <span name="area">-</span>
                        </p>
                    </div>

                 </div>
                 <div class="row mb-4">

                    <div class="col-12 col-md-4">
                        <p>
                            <span class="font-weight-bold">TFL:</span> 
                            <span name="nombre_tfl">-</span>
                        </p>
                    </div>
                    <div class="col-12 col-md-4">
                        <p>
                            <span class="font-weight-bold">Estado:</span> 
                            <span name="estado_tfl">-</span>
                        </p>
                    </div>
                    <div class="col-12 col-md-4">
                        <p>
                            <span class="font-weight-bold">Fecha Efectiva:</span> 
                            <span name="fecha_efectiva_tfl">-</span>
                        </p>
                    </div>

                </div>
                </div>
            </div>

            <div id="contenidoCards" class="row mt-3">
                <%--
                <div class="col-md-3 mb-4">
                    <div class="card card-home">
                        <div class="card-header">
                            <h3 class="h3-responsive">Etapa 1</h3>
                        </div>

                        <div class="card-body">
                            <ul id="stage1" class="list-card">
                                <li class="mr-0 card-header d-flex align-items-baseline justify-content-between" id="iconos-hechos">
                                <div>
                                    <i class="material-icons icon-lg mr-1 success-text">done</i>
                                    <span class="d-md-inline">Seleccionar Cualificaciones para una TFL </span>
                                </div>
                                <a href="#" data-toggle="modal" data-target="#ModalVolverAtras" class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                </li>
                                <li class="mr-0 card-header d-flex align-items-baseline justify-content-between" id="iconos-hechos">
                                <div>
                                    <i class="material-icons icon-lg mr-1 success-text">done</i>
                                    <span class="d-md-inline">Seleccionar Cualificaciones para una TFL</span>
                                </div>
                                <a href="#" data-toggle="modal" data-target="#ModalVolverAtras" class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                </li>
                                <li class="mr-0 card-header d-flex align-items-baseline justify-content-between">
                                <div>
                                    <i class="material-icons icon-lg mr-1 success-text">done</i>
                                    <span class="d-md-inline">Análisis de Demanda de la TFL</span>
                                </div>
                                <a href="#" data-toggle="modal" data-target="#ModalVolverAtras" class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                </li>
                                <li class="mr-0 card-header d-flex align-items-baseline justify-content-between">
                                <div>
                                    <i class="material-icons icon-lg mr-1 success-text">done</i>
                                    <span class="d-md-inline">Análisis de Factibilidad de la TFL</span>
                                </div>
                                <a href="#" data-toggle="modal" data-target="#ModalVolverAtras" class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                </li>
                                <li class="mr-0 card-header d-flex align-items-baseline justify-content-between">
                                <div>
                                    <i class="material-icons icon-lg mr-1 success-text">done</i>
                                    <span class="d-md-inline">Resumen de Demanda y Factibilidad TFL</span>
                                </div>
                                <a href="#" data-toggle="modal" data-target="#ModalVolverAtras" class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                </li>
                                <li class="mr-0 card-header d-flex align-items-baseline justify-content-between">
                                <div class="d-flex">
                                    <i class="material-icons icon-lg mr-1 success-text">done</i>
                                    <span class="d-md-inline">Selección Final de Cualificaciones de una TFL</span>
                                </div>
                                <a href="#" data-toggle="modal" data-target="#ModalVolverAtras" class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                </li>
                                <li class="mr-0 card-header d-flex align-items-baseline justify-content-between">
                                <div class="d-flex">
                                    <i class="material-icons icon-lg mr-1 success-text">done</i>
                                    <span class="d-md-inline">Descripción del Perfil Profesional de una TFL</span>
                                </div>
                                <a href="#" data-toggle="modal" data-target="#ModalVolverAtras" class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                </li>
                                <li class="mr-0 card-header d-flex align-items-baseline justify-content-between">
                                <div>
                                    <i class="material-icons icon-lg mr-1 success-text">done</i>
                                    <span class="d-md-inline">Lista de Verificación (LVC) Etapa 1</span>
                                </div>
                                <a href="#" data-toggle="modal" data-target="#ModalVolverAtras" class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>

                <div class="col-md-3 mb-4">
                    <div class="card card-home">
                        <div class="card-header">
                            <h3 class="h3-responsive">Etapa 2</h3>
                        </div>

                        <div class="card-body">
                            <ul id="stage2" class="list-card">
                                <li class="mr-0 card-header d-flex align-items-baseline justify-content-between">
                                <div>
                                    <i class="material-icons icon-lg mr-1 success-text">done</i>
                                    <span class="d-md-inline">Definición de Elementos Complementarios</span>
                                </div>
                                <a href="#" data-toggle="modal" data-target="#ModalVolverAtras" class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                </li>
                                <li class="mr-0 card-header d-flex align-items-baseline justify-content-between">
                                <div class="d-flex">
                                    <i class="material-icons icon-lg mr-1 success-text">done</i>
                                    <span class="d-md-inline">Definición de Elementos Transversales (Asignatura SIGA)</span>
                                </div>
                                <a href="#" data-toggle="modal" data-target="#ModalVolverAtras" class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                </li>
                                <li class="mr-0 card-header d-flex align-items-baseline justify-content-between">
                                <div>
                                    <i class="material-icons icon-lg mr-1 success-text">done</i>
                                    <span class="d-md-inline">Definición Elementos Comunes </span>
                                </div>
                                <a href="#" data-toggle="modal" data-target="#ModalVolverAtras" class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                </li>
                                <li class="mr-0 card-header d-flex align-items-baseline justify-content-between">
                                <div class="d-flex">
                                    <i class="material-icons icon-lg mr-1 success-text">done</i>
                                    <span class="d-md-inline">Consulta Definición del Tipo de Asignatura (orientación de Matriz-Colores)</span>
                                </div>
                                <a href="#" data-toggle="modal" data-target="#ModalVolverAtras" class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                </li>
                                <li class="mr-0 card-header d-flex align-items-baseline justify-content-between">
                                <div class="d-flex">
                                    <i class="material-icons icon-lg mr-1 success-text">done</i>
                                    <span class="d-md-inline">Matriz Tipo de Asignatura y Productos Recomendados</span>
                                </div>
                                <a href="#" data-toggle="modal" data-target="#ModalVolverAtras" class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                </li>
                                <li class="mr-0 card-header d-flex align-items-baseline justify-content-between">
                                <div>
                                    <i class="material-icons icon-lg mr-1 success-text">done</i>
                                    <span class="d-md-inline">LVC Etapa 2</span>
                                </div>
                                <a href="#" data-toggle="modal" data-target="#ModalVolverAtras" class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>

                <div class="col-md-3 mb-4">
                    <div class="card card-home">
                        <div class="card-header">
                            <h3 class="h3-responsive">Etapa 3</h3>
                        </div>

                        <div class="card-body">
                            <ul id="stage3" class="list-card">
                                <li class="mr-0 card-header d-flex align-items-baseline justify-content-between">
                                <div>
                                    <i class="material-icons icon-lg mr-1 success-text">done</i>
                                    <span class="d-md-inline">Competencias Técnicas de Egreso</span>
                                </div>
                                <a href="#" data-toggle="modal" data-target="#ModalVolverAtras" class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                </li>
                                <li class="mr-0 card-header d-flex align-items-baseline justify-content-between">
                                <div class="d-flex">
                                    <i class="material-icons icon-lg mr-1 success-text">done</i>
                                    <span class="d-md-inline">Definición de Competencias Técnicas de Egreso</span>
                                </div>
                                <a href="#" data-toggle="modal" data-target="#ModalVolverAtras" class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                </li>
                                <li class="mr-0 card-header d-flex align-items-baseline justify-content-between">
                                <div>
                                    <i class="material-icons icon-lg mr-1 success-text">done</i>
                                    <span class="d-md-inline">Matriz de Tributación</span>
                                </div>
                                <a href="#" data-toggle="modal" data-target="#ModalVolverAtras" class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                </li>
                                <li class="mr-0 card-header d-flex align-items-baseline justify-content-between">
                                <div class="d-flex">
                                    <i class="material-icons icon-lg mr-1 success-text">done</i>
                                    <span class="d-md-inline">Descripción Perfil de Egreso Productos de la TFL</span>
                                </div>
                                <a href="#" data-toggle="modal" data-target="#ModalVolverAtras" class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                </li>
                                <li class="mr-0 card-header d-flex align-items-baseline justify-content-between">
                                <div>
                                    <i class="material-icons icon-lg mr-1 success-text">done</i>
                                    <span class="d-md-inline">LVC Etapa 3</span>
                                </div>
                                <a href="#" data-toggle="modal" data-target="#ModalVolverAtras" class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                </li>
                                <li class="mr-0 card-header d-flex align-items-baseline justify-content-between">
                                <div>
                                    <i class="material-icons icon-lg mr-1 success-text">done</i>
                                    <span class="d-md-inline">Technical Graduation Competencies</span>
                                </div>
                                <a href="#" data-toggle="modal" data-target="#ModalVolverAtras" class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                </li>
                                <li class="mr-0 card-header d-flex align-items-baseline justify-content-between">
                                <div>
                                    <i class="material-icons icon-lg mr-1 success-text">done</i>
                                    <span class="d-md-inline">Technical Graduation Competencies</span>
                                </div>
                                <a href="#" data-toggle="modal" data-target="#ModalVolverAtras" class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                </li>
                                <li class="mr-0 card-header d-flex align-items-baseline justify-content-between">
                                <div>
                                    <i class="material-icons icon-lg mr-1 success-text">done</i>
                                    <span class="d-md-inline">Technical Graduation Competencies</span>
                                </div>
                                <a href="#" data-toggle="modal" data-target="#ModalVolverAtras" class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>

                <div class="col-md-3 mb-4">
                    <div class="card card-home">
                        <div class="card-header">
                            <h3 class="h3-responsive">Etapa 4</h3>
                        </div>

                        <div class="card-body">
                            <ul id="stage4" class="list-card">
                                <li class="mr-0 card-header d-flex align-items-baseline justify-content-between">
                                <div>
                                    <i class="material-icons icon-lg mr-1 success-text">done</i>
                                    <span class="d-md-inline">Consulta Estructura de Programas Formativos de una Institución</span>
                                </div>
                                <a href="#" data-toggle="modal" data-target="#ModalVolverAtras" class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                </li>
                                <li class="mr-0 card-header d-flex align-items-baseline justify-content-between">
                                <div>
                                    <i class="material-icons icon-lg mr-1 success-text">done</i>
                                    <span class="d-md-inline">Mecanismo de Articulación y Reconocimiento</span>
                                </div>
                                <a href="#" data-toggle="modal" data-target="#ModalVolverAtras" class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                </li>
                                <li class="mr-0 card-header d-flex align-items-baseline justify-content-between">
                                <div>
                                    <i class="material-icons icon-lg mr-1 success-text">done</i>
                                    <span class="d-md-inline">Estructura Malla del Programa Académico</span>
                                </div>
                                <a href="#" data-toggle="modal" data-target="#ModalVolverAtras" class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                </li>
                                <li class="mr-0 card-header d-flex align-items-baseline justify-content-between">
                                <div>
                                    <i class="material-icons icon-lg mr-1 success-text">done</i>
                                    <span class="d-md-inline">Perfil Asignatura TFL</span>
                                </div>
                                <a href="#" data-toggle="modal" data-target="#ModalVolverAtras" class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                </li>
                                <li class="mr-0 card-header d-flex align-items-baseline justify-content-between">
                                <div>
                                    <i class="material-icons icon-lg mr-1 success-text">done</i>
                                    <span class="d-md-inline">Estructura Malla con Información de Asignaturas</span>
                                </div>
                                <a href="#" data-toggle="modal" data-target="#ModalVolverAtras" class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                </li>
                                <li class="mr-0 card-header d-flex align-items-baseline justify-content-between">
                                <div>
                                    <i class="material-icons icon-lg mr-1 success-text">done</i>
                                    <span class="d-md-inline">LVC Etapa 4</span>
                                </div>
                                <a href="#" data-toggle="modal" data-target="#ModalVolverAtras" class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                --%>
            </div>
        
        </div>




        <div id="PROBANDO">
            <div class="row mt-3">
                <div class="col-md-3 mb-4">
                    <div class="card card-home">
                        <div class="card-header">
                            <h3 class="h3-responsive">QUITAR ESTO</h3>
                        </div>

                        <div class="card-body">
                            <ul class="list-card">

                                <li class="mr-0 card-header d-flex align-items-baseline justify-content-between">
                                    <div>
                                        <table>
                                            <tr>
                                                <td style="font-size: 1em">
                                                    <i style="visibility: visible" class="material-icons icon-lg mr-1 success-text">done</i>
                                                </td>
                                                <td>
                                                    <p style="font-size: 14px" class="d-md-inline">
                                                        Descripción del Perfil Profesional de una TFL AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
                                                    </p>
                                                </td>
                                                <td style="font-size: 1em">
                                                    <a href="#" onclick="abrirModalVolverAtras(event)" style="visibility: visible" class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                                </td>
                                            </tr>
                                        </table>

                                    </div>
                
                                </li>

                            </ul>
                        </div>
                    </div>
                </div>
             </div>
        </div>






      </section>
    </main>

  </div>


  <footer class="page-footer">
    <div class="container py-3">
      <div class="d-flex align-items-center justify-content-center justify-content-md-start">
        <div class="logo-gst"><img src="http://www.inacap.cl/web/template-aplicaciones/img/logo-gst.png" alt=""></div>
        <span class="pl-2 pl-md-4">Gerencia de Sistemas y Tecnolog&iacute;as</span>
      </div>
    </div>
  </footer>

  <div class="modal fade" id="ModalVolverAtras" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-sm modal-notify" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <p class="heading lead">Volver Atr&aacute;s</p>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <p class="text-center m-0">
                                Est&aacute;s a punto de volver atr&aacute;s en el proceso. Recuerda que al volver atr&aacute;s, el proceso se reiniciar&aacute; desde fue seleccionado para volver atr&aacute;s, y el resto de los datos se
                                guardar&aacute;n en modo borrador, as&iacute; que podr&aacute;s retomar justo donde lo dejaste en cualquier momento.
                       
                            </p>
                            <p class="text-center m-0">
                                &iquest;Est&aacute;s seguro de que deseas continuar con esta acci&oacute;n?
                            </p>
                        </div>
                    </div>
                </div>

                <div class="modal-footer justify-content-center">
                    <button type="button" class="btn mx-2 btn-secondary waves-effect waves-light" data-dismiss="modal">No, cancelar</button>
                    <button type="button" onclick="volverAtras()" class="btn mx-2 btn-default waves-effect waves-light alerta">S&iacute;, volver atr&aacute;s</button>
                </div>
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

  <script src="../MCTP_Diseño/jquery-3.3.1.min.js"></script>
  <script src="../MCTP_Diseño/popper.min.js"></script>
  <script src="../MCTP_Diseño/bootstrap.min.js"></script>
  <script src="../MCTP_Diseño/mdb.min.js"></script>
  <script src="../MCTP_Diseño/chosen.jquery.js"></script>
  <script src="../MCTP_Diseño/datepicker.js"></script>
  <script src="../MCTP_Diseño/datatables.min.js"></script>
  <script src="../MCTP_Diseño/xlsx.full.min.js"></script>
  <script src="../MCTP_Diseño/configuraciones.js"></script>
  
  <script src="VOLVER_ATRAS.js"></script>

</body>

</html>
