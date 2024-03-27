
namespace MCTP_c_Modelos_de_Datos
{
    public class RespuestaSP
    {
        public int swt { get; set; }        // Es un switch. Resultados:  0 (Encontró el registro) | 1 (No encontró el registro) | 2 (Encontró un error)
        public string msg { get; set; }     // Mensaje
        public string sts { get; set; }     // Status
        public string tbl { get; set; }     // Trae el nombre de la tabla
        public string pkgp { get; set; }    // Trae el PKG que se usó para leer esa tabla

        public string informacionExtra { get; set; }

    }
}
