
namespace TFL_x_WEB.Helpers
{
    public class InfoUsuario
    {
        public string msjGuardarBorrador { get; set; }
        public string msjPublicar { get; set; }
    }

    public static class InfoUsuarioHelper
    {
        public static InfoUsuario ObtenerInfoUsuario()
        {
            return new InfoUsuario
            {
                msjGuardarBorrador = "Se guardarán los datos sin fecha de publicación.",
                msjPublicar = "Se guardarán los datos con fecha de publicación y no podrán ser modificados:"
            };
        }
    }
}