using System.Collections.Generic;

namespace TFL_x_WEB.Dto
{
    public class Grilla
    {
        public string draw { get; set; }
        public decimal recordsFiltered { get; set; }
        public decimal recordsTotal { get; set; }
        public object data { get; set; }

        public Grilla()
        {
            draw = "";
            recordsFiltered = 0;
            recordsTotal = 0;
            data = new List<object>();
        }
    }
}