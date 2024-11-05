<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu_Principal.aspx.cs" Inherits="TFL_x_WEB.Menu_Principal.Menu_Principal" %>

<!DOCTYPE html>
<html lang="en"> 
  <head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <title>Administraci&oacute;n de TFL</title>
    <link rel="shortcut icon" href="https://digital.inacap.cl/template-aplicaciones/img/favicon.ico" />
    <link rel="stylesheet" href="https://digital.inacap.cl/template-aplicaciones/css/mdb.min.css">
    <link rel="stylesheet" href="estiloApp.css">

    <style>
        .card-home {
            height: 100%;
            min-height: 50vh;
        }

        .list-card {
            padding: 0;
            list-style: none;
            font-weight: 700;
        }
        .list-card li {
            margin-bottom: 1rem;
            font-size: 14px;
            cursor: pointer;
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
      
      <main id="main" class="container main">

        <section id='brad' class="row breadcrumb">

        </section>

        <div id="data">
          <section id="seccion-menu" >

            <div id="menu-tfl">

              <div class="row mb-4">

                <div class="col-4 mx-auto">

                  <div class="card card-home">

                    <div class="card-header">
                      <div class="row">
                        <div class="col-12">
                          <h4 class="float-left heading h4-responsive mb-0">Menú</h4>
                        </div>
                      </div>
                    </div>
                    
                    <div class="card-body">

                      <div class="row mb-4">

                        <div class="col-12">
                          <ul class="list-card">
                            <li>
                                <a href="../Guardar_Borrador_y_Publicar/Guardar_Borrador_y_Publicar.aspx">
                                    Guardar Borrador y Publicar
                                </a>
                            </li>
                            <li>
                                <a href="../Ejemplo_Mantenedor/Ejemplo_Mantenedor.aspx">
                                    Ejemplo Mantenedor
                                </a>
                            </li>
                            <%--<li>
                                <a href="../CLONAR/CLONAR.aspx">
                                    2. Clonar TFL(Generar una nueva TFL) >
                                </a>
                            </li>--%>
                          </ul>
                        </div>

                      </div>
                      
                    </div>

                  </div>

                </div>
                
              </div>

            </div>

            
            
          </section>

          <section id="seccion-inicio" style="display: none;">

            <div id="buscar-tfl" style="display: block;">

              <div class="row mb-4">

                <div class="col-12">

                  <div class="card">

                    <div class="card-header">
                      <div class="row">
                        <div class="col-12">
                          <h4 class="float-left heading h4-responsive mb-0">Buscar TFL para Clonar</h4>
                        </div>
                      </div>
                    </div>
                    
                    <div class="card-body">

                      <div class="row mb-4">

                        <div class="col-12 col-md-4">
                          <div class="md-form">
                            <select class="mdb-select" id="buscar-tfl-campo-01" searchable="Buscar">
                              <option value="" disabled selected>Seleccione per&iacute;odo</option>
                              <option value="1">Instituci&oacute;n 1</option>
                              <option value="2">Instituci&oacute;n 2</option>
                            </select>
                            <label id="buscar-tfl-campo-01">Per&iacute;odo de Vigencia Diseño de TFL</label>
                          </div>
                        </div>
            
                        <div class="col-12 col-md-4">
                          <div class="md-form">
                            <select class="mdb-select" id="buscar-tfl-campo-02" searchable="Buscar">
                              <option value="" disabled selected>Seleccione Direcci&oacute;n</option>
                              <option value="1">Direcci&oacute;n Sectorial 1</option>
                              <option value="2">Direcci&oacute;n Sectorial 2</option>
                            </select>
                            <label id="buscar-tfl-campo-02">Direcci&oacute;n Sectorial</label>
                          </div>
                        </div>
            
                        <div class="col-12 col-md-4">
                          <div class="md-form">
                            <select class="mdb-select" id="buscar-tfl-campo-03" searchable="Buscar">
                              <option value="" disabled selected>Seleccione &Aacute;rea</option>
                              <option value="1">&Aacute;rea 1</option>
                              <option value="2">&Aacute;rea 2</option>
                            </select>
                            <label id="buscar-tfl-campo-03">&Aacute;rea</label>
                          </div>
                        </div>
                      </div>

                      <div class="row mb-4">

                        <div class="col-12 col-md-4">
                          <div class="md-form">
                            <select class="mdb-select" id="buscar-tfl-campo-01" searchable="Buscar">
                              <option value="" disabled selected>Seleccione TFL</option>
                              <option value="1">TFL 1</option>
                              <option value="2">TFL 2</option>
                            </select>
                            <label id="buscar-tfl-campo-01">TFL</label>
                          </div>
                        </div>
                      </div>
                      
                    </div>

                    <div class="card-footer">
                      <div class="row my-4">
                        <div class="col d-flex justify-content-start">
                          <button type="button" id="btn-volver-menu" class="btn btn-outline waves-effect waves-light">
                            Volver a Men&uacute; Principal
                          </button>
                        </div>
                        <div class="col d-flex justify-content-end">
                          <button type="button" id="btn-buscar-tfl" class="btn btn-default waves-effect waves-light">
                            Buscar TFL
                          </button>
                        </div>
                      </div>
                    </div>

                  </div>

                </div>
                
              </div>

            </div>

            <div id="resultados-tfl" style="display: none;">

              <div class="row mb-4">
    
                <div class="col-12">
    
                  <div class="card">
                    
                    <div class="card-header">
                      <div class="row">
                        <div class="col-md-6 col-12">
                          <h4 class="float-left heading h4-responsive mb-0">TFL para Clonar</h4>
                        </div>
                        <div class="col-md-6 col-12 d-flex justify-content-end">
                          <button type="button" id="btn-nueva-busqueda-tfl" class="btn btn-outline waves-effect waves-light">
                            Realizar Nueva B&uacute;squeda
                          </button>
                        </div>
                      </div>
                    </div>
                    
                    <div class="card-body">
      
                      <div class="row mb-4">

                        <div class="col-12 col-md-4">
                          <strong>Per&iacute;odo Vigencia Diseño TFL:</strong>
                          <span>Dise&ntilde;o Oferta 2024</span>
                        </div>

                        <div class="col-12 col-md-4">
                          <strong>Direcci&oacute;n Sectorial:</strong>
                          <span>Administraci&oacute;n y Servicios</span>
                        </div>

                        <div class="col-12 col-md-4">
                          <strong>&Aacute;rea:</strong>
                          <span>Hoteler&iacute;a, Turismo y Gastronom&iacute;a</span>
                        </div>

                      </div>
            
                      <div class="row mb-4">

                        <div class="col-12 col-md-4">
                          <strong>TFL:</strong>
                          <span>Prueba TFL 1</span>
                        </div>

                        <div class="col-12 col-md-4">
                          <strong>Estado:</strong>
                          <span>Activo</span>
                        </div>

                        <div class="col-12 col-md-4">
                          <strong>Fecha Efectiva:</strong>
                          <span>01/03/2024</span>
                        </div>
            
                      </div>
                      
                    </div>
                  </div>
    
                </div>
              
                
              </div>

              <div class="row mb-4">
    
                <div class="col-12">
    
                  <div class="card">
                    
                    <div class="card-header">
                      <div class="row">
                        <div class="col-md-6 col-12">
                          <h4 class="float-left heading h4-responsive mb-0">Modificar Informaci&oacute;n</h4>
                        </div>
                      </div>
                    </div>
                    
                    <div class="card-body">
      
                      <div class="row mb-4">

                        <div class="col-12 col-md-4">
                          <div class="md-form">
                            <select class="mdb-select" id="clonar-tfl-campo-01" searchable="Buscar">
                              <option value="" disabled selected>Seleccione per&iacute;odo</option>
                              <option value="1">Per&iacute;odo 1</option>
                              <option value="2">Per&iacute;odo 2</option>
                            </select>
                            <label id="clonar-tfl-campo-01">Per&iacute;odo de Vigencia Diseño de TFL</label>
                          </div>
                        </div>

                        <div class="col-12 col-md-4">
                          <div class="md-form mb-3">
                            <input type="text" name="clonar-tfl-campo-02" id="clonar-tfl-campo-02" class="form-control mb-0 editar-focus" placeholder="Ingrese nombre TFL" />
                            <label for="clonar-tfl-campo-02" class="active">Nombre TFL</label>
                          </div>
                        </div>

                        <div class="col-12 col-md-4">
                          <strong>Versi&oacute;n TFL:</strong>
                          <span>1</span>
                        </div>
                      </div>

                      <div class="row mb-4">


                        <div class="col-12 col-md-4">
                          <div class="md-form mb-3">
                            <input type="text" name="clonar-tfl-campo-03" id="clonar-tfl-campo-03" class="form-control mb-0 editar-focus" placeholder="Ingrese m&aacute;ximo de cualificaciones" />
                            <label for="clonar-tfl-campo-03" class="active">N&uacute;mero M&aacute;ximo de Cualificaciones</label>
                          </div>
                        </div>

                        <div class="col-12 col-md-4">
                          <div class="md-form mb-3">
                            <input type="text" name="clonar-tfl-campo-04" id="clonar-tfl-campo-04" class="form-control mb-0 editar-focus" placeholder="Ingrese m&aacute;ximo de UCLs" />
                            <label for="clonar-tfl-campo-04" class="active">N&uacute;mero M&aacute;ximo de UCLs</label>
                          </div>
                        </div>

                        <div class="col-12 col-md-4">
                          <div class="md-form">
          
                            <input id="date-picker" type='text' class="datepicker-input form-control" placeholder="DD/MM/AAAA"
                              data-position="bottom center" autocomplete="off" />
                            <label for="date-picker" class="active">Fecha Efectiva</label>
                            
                          </div>
                        </div>

                        
                      </div>

                      <div class="row mb-4">
                        <div class="col-md-6 mb-4">
                          <div class="md-form">
                              <textarea id="clonar-tfl-campo-05" name="clonar-tfl-campo-05" rows="4" class="form-control md-textarea" placeholder="Ingrese comentarios"></textarea>
                              <label for="clonar-tfl-campo-05" class="active">Descripci&oacute;n</label>
                          </div>
                        </div>
                      </div>
                      
                    </div>

                    <div class="card-footer">
                      <div class="row my-4">
                        <div class="col d-flex justify-content-end">
                          <button type="button" id="btn-cancelar-clonar" class="btn btn-secondary waves-effect waves-light align-items-center d-flex">
                            Cancelar
                          </button>
                          <button type="button" id="btn-clonar-tfl" class="btn btn-default waves-effect waves-light align-items-center d-flex">
                            Clonar TFL
                          </button>
                        </div>
                      </div>
                    </div>
                  </div>
    
                </div>
              
                
              </div>
    
            </div>

            
            
          </section>
        
        </div>

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
    
    
    
    <!-- Loading Overlay -->
    <div class="loading-backdrop" id="loadingOverlay">
      <div class="loading-circle">
        <div class="preloader-wrapper big active"><div class="spinner-layer spinner-white-only"><div class="circle-clipper left"><div class="circle"></div></div><div class="gap-patch"><div class="circle"></div></div><div class="circle-clipper right"><div class="circle"></div></div></div></div>
      </div>
    </div>


    <div id="overlay-menu" style="display: none;"></div>
      
      
      
      
  <script src="../MCTP_Disenio/jquery-3.3.1.min.js"></script>
  <script src="../MCTP_Disenio/popper.min.js"></script>
  <script src="../MCTP_Disenio/bootstrap.min.js"></script>
  <script src="../MCTP_Disenio/mdb.min.js"></script>
  <script src="../MCTP_Disenio/chosen.jquery.js"></script>
  <script src="../MCTP_Disenio/datepicker.js"></script>
  <script src="../MCTP_Disenio/datatables.min.js"></script>
  <script src="../MCTP_Disenio/xlsx.full.min.js"></script>
  <script src="../MCTP_Disenio/configuraciones.js"></script>
      
  </body>

</html>
