using System.Collections.Generic;

namespace TFL_x_WEB.Dto
{
    public class MenuAcceso   // Menu
    {
        public string nombre { get; set; }
        public string url { get; set; }
        public bool esPaginaActual { get; set; } = false;
        public List<SubMenu> listaSubMenu { get; set; } = new List<SubMenu>();
    }

    public class SubMenu
    {
        public string nombre { get; set; }
        public string codigoSistema { get; set; }
        public List<ItemSubMenu> listaItemSubMenu { get; set; } = new List<ItemSubMenu>();
    }

    public class ItemSubMenu
    {
        public string nombre { get; set; }
        public string url { get; set; }
        public bool autorizado { get; set; } = false;
        public bool esOtraAplicacion { get; set; }
    }
}