<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PROBANDO.aspx.cs" Inherits="TFL_x_WEB.PROBANDO.PROBANDO" %>

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
<%--                    <div class="row">
                        <div class="col-12 col-md-12">
                            <h5>Última versión realizada el 17-6-2024</h5>
                        </div>
                    </div>--%>

                    <div class="row">
                        <div class="col-12 col-md-2">
                            <button onclick="obtenerUsuario()">Obtener Usuario</button>
                        </div>

                        <div class="col-12 col-md-2">
                            <button onclick="prueba()">Prueba</button>
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
        
        });

        function obtenerUsuario()
        {
            $.ajax({
                method: "POST",
                url: "PROBANDO.aspx/ObtenerUsuario",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: () => showLoading(),
                success: (res) => {
                    if (res.status == 200) {
                        alert(JSON.stringify(res.objeto));
                    }
                    else {
                        alert(JSON.stringify(res));
                    }
                },
                error: (XMLHttpRequest, textStatus, errorThrown) => alert("Ocurrió un error al obtener la información"),
                complete: (res) => hideLoading()
            });
        }

        function prueba()
        {
            $.ajax({
                method: "POST",
                url: "PROBANDO.aspx/Prueba",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: () => showLoading(),
                success: (res) => {
                    if (res.status == 200) {
                        alert(JSON.stringify(res.objeto));
                    }
                    else {
                        alert(JSON.stringify(res));
                    }
                },
                error: (XMLHttpRequest, textStatus, errorThrown) => alert("Ocurrió un error al obtener la información"),
                complete: (res) => hideLoading()
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