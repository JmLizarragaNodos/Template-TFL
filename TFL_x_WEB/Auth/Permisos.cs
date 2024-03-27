using System.ComponentModel;

namespace TFL_x_WEB.Auth
{
    /*
    Cuando un rol tiene el permiso de "SELECT" puede solo consultar en la aplicación.
    Cuando un rol tiene el permiso de "UPDATE" puede hacer todo (insertar, modificar, consultar).
    */

    public enum Permisos
    {
        [Description("Consultar")]
        SELECT,

        [Description("Todo")]
        UPDATE
    }
}