<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="INTERNAL_SERVER_ERROR.aspx.cs" Inherits="TFL_x_WEB.INTERNAL_SERVER_ERROR.INTERNAL_SERVER_ERROR" %>

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
  
</head>
<body id="appBody" class="d-flex flex-column">
  
  <div class="page-content">
    
    <div class="navbar-overlay"></div>
    
    <main id="main" class="container">

        <section>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-12 col-md-12">
                            <p id="mensaje-error" style="display: none; color:#c00 ">---</p>
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

        $(document).ready(() =>
        {
            const parrafo = document.querySelector("#mensaje-error");
            const urlParams = new URLSearchParams(window.location.search);
            const errorCarga = urlParams.get('errorCarga');
            limpiarParametrosGet();

            if (errorCarga) {
                parrafo.style.display = "block";
                parrafo.innerHTML = errorCarga;
            }
            else {
                console.log('El parámetro errorCarga no está presente en la URL.');
            }
        });

        function limpiarParametrosGet()
        {
            const currentUrl = window.location.href;
            const url = new URL(currentUrl);
            url.search = "";
            history.replaceState(null, '', url);
        }

    </script>

</body>
</html>