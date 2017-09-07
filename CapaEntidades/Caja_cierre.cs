using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Caja_Cierre
    {
        public int id { get; set; }
        public DateTime fecha_desde { get; set; }
        public DateTime fecha_hasta { get; set; }
        public int id_usuario { get; set; }
        public Caja_Movimientos movimiento { get; set; }

        public Caja_Cierre() { }

    }
}
